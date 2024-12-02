using AutoMapper;
using Core.DTOS.Shared;
using Core.DTOS.Vacancy;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications.SpecificationBuilder;
using Microsoft.AspNetCore.Identity;
using Stripe;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VacancyService : IVacancyService
    {

        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public VacancyService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<VacancyDto> CreateVacancy(MangeVacancyDto addVacancyDto)
        {
            var vacancy = mapper.Map<Vacancy>(addVacancyDto);

            await unitOfWork.Repository<Vacancy>().AddAsync(vacancy);

            await unitOfWork.Compleate();

            return mapper.Map<VacancyDto>(vacancy);
        }

        public async Task<ListWithCountDto<VacancyDto>> GetVacancyCountAll(VacancyQuaryDto vacancyQuaryDto)
        {
            var spec = vacancyQuaryDto.BuildSpecification()
                .WithLimit(vacancyQuaryDto.Limit)
                .WithPage(vacancyQuaryDto.Page)
                .Build();

            var vacancies = await unitOfWork.Repository<Vacancy>().GetAllAsync(spec);
            var count = await unitOfWork.Repository<Vacancy>().GetCountAsync(vacancyQuaryDto.BuildSpecification().Build());

            return new ListWithCountDto<VacancyDto>
            {
                Count = count,
                Items = mapper.Map<IEnumerable<VacancyDto>>(vacancies)
            };
        }

        public async Task<VacancyDto> GetVacancyyId(int id)
        {
            var spec = new VacancySpecificationBuilder()
                .WithId(id)
                .Include(x => x.Employer)
                .Build();

            var vacancy = await this.unitOfWork.Repository<Vacancy>().GetOneAsync(spec);

            if (vacancy == null)
                throw new NotFoundException("Vacancy not exist");

            return mapper.Map<VacancyDto>(vacancy);
        }

        public async Task<VacancyDto> UpdateVacancy(int id, MangeVacancyDto updateVacancyDto)
        {
            var repo = unitOfWork.Repository<Vacancy>();

            Vacancy vacancy = await CheckVacancy(id, updateVacancyDto.EmployerId);

            mapper.Map(updateVacancyDto, vacancy);

            repo.Update(vacancy);

            await unitOfWork.Compleate();


            return await GetVacancyyId(id);
        }

      

        public async Task ToggleVacancyStatus(int id, int employerId)
        {

            Vacancy vacancy = await CheckVacancy(id, employerId);

            vacancy.IsActive = !vacancy.IsActive;

            unitOfWork.Repository<Vacancy>().Update(vacancy);

            await unitOfWork.Compleate();

            return;
        }

        private async Task<Vacancy> CheckVacancy(int id, int employerId)
        {
            var repo = unitOfWork.Repository<Vacancy>();

            var vacancy = await repo.GetOneAsync(x => x.EmployerId == employerId && x.Id == id);

            if (vacancy == null)
                throw new NotFoundException("Vacancy not exist");

            return vacancy;
        }

        public async Task DeleteVacancy(int id, int employerId)
        {
            Vacancy vacancy = await CheckVacancy(id, employerId);

            unitOfWork.Repository<Vacancy>().Delete(vacancy);

            await unitOfWork.Compleate();

            return;
        }
    }
}
