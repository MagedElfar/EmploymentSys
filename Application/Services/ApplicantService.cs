using AutoMapper;
using Core.DTOS;
using Core.DTOS.Application;
using Core.Entities;
using ApplicationEntity = Core.Entities.Application;
using Core.Interfaces.Repositories;
using Core.Specifications.SpecificationBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.DTOS.Shared;
using Core.DTOS.Vacancy;

namespace Application.Services
{
    public class ApplicantService : IApplicantService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ApplicantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApplicationDto> GetApplicatioAsync(int applicantId, int vacancyId)
        {

            Console.WriteLine(applicantId);
            Console.WriteLine(vacancyId);

            var spec = new ApplicationpecificationBuilder()
                .WithVacancyId(vacancyId)
                .WithApplicantIdId(applicantId)
                .Include(x => x.Applicant)
                .Include(x => x.Vacancy)
                .Build ();

            var application = await unitOfWork.Repository<ApplicationEntity>().GetOneAsync(spec);

            Console.WriteLine(application);

            if (application == null)
                throw new NotFoundException();

            return mapper.Map<ApplicationDto>(application);
        }

        public async Task<ApplicationDto> ApplyForVacancyAsync(int applicantId, int vacancyId)
        {

            var repo = unitOfWork.Repository<ApplicationEntity>();

            // Check if vacancy exists and is active
            var vacancy = await unitOfWork.Repository<Vacancy>().GetByIdAsync(vacancyId);

            if (vacancy == null || !vacancy.IsActive || vacancy.ExpiryDate < DateTimeOffset.Now)
                throw new BadRequestException("Vacancy is not available for application.");

            //check if user already applaird
            var exsitAppication = await repo.GetOneAsync(x => x.ApplicantId == applicantId && x.VacancyId == vacancyId);
            if (exsitAppication != null)
                throw new BadRequestException("Allicant already apply for this vacancy");

            // Check if the max number of applications has been reached
            var vacancyApplicationCount = await repo.GetCountAsync(x => x.VacancyId == vacancyId);
            if (vacancyApplicationCount >= vacancy.MaxApplications)
                throw new BadRequestException("Vacancy has reached the maximum number of applications.");

            // Check if the applicant has applied for another vacancy today
            var today = DateTimeOffset.Now.Date;
            var hasAppliedToday = await repo.GetOneAsync(
                x => x.ApplicantId == applicantId && x.CreatedDate.Date == today
                );
            if (hasAppliedToday != null)
                throw new BadRequestException("You cannot apply for more than one vacancy per day.");

            // Add the application
            var application = new ApplicationEntity
            {
                ApplicantId = applicantId,
                VacancyId = vacancyId,
            };

            await repo.AddAsync(application);
            await unitOfWork.Compleate();

            return await GetApplicatioAsync(applicantId, vacancyId);
        }

        public async Task<ListWithCountDto<ApplicationDto>> GetVacancyApplicatios(VacancyApplicationQueryDto vacancyApplicationQueryDto)
        {

            var spec = new ApplicationpecificationBuilder()
                .WithVacancyId(vacancyApplicationQueryDto.VacancyId)
                .WithEmployerId(vacancyApplicationQueryDto.EmployerId)
                .Include(x => x.Applicant)
                .Include(x => x.Vacancy)
                .WithPage(vacancyApplicationQueryDto.Page)
                .WithLimit(vacancyApplicationQueryDto.Limit)
                .Build();

            var applications = await unitOfWork.Repository<ApplicationEntity>().GetAllAsync(spec);
            var count = await unitOfWork.Repository<ApplicationEntity>().GetCountAsync(
                x => x.VacancyId == vacancyApplicationQueryDto.VacancyId && x.Vacancy.EmployerId == vacancyApplicationQueryDto.EmployerId
                );

            return new ListWithCountDto<ApplicationDto>
            {
                Count = count,
                Items = mapper.Map<IEnumerable<ApplicationDto>>(applications)
            };

        }
    }
}
