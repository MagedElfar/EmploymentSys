using AutoMapper;
using Core.DTOS.Shared;
using Core.DTOS.Vacancy;
using Core.Entities;
using Core.Exceptions;
using Core.Helper;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications.SpecificationBuilder;
using System.Text;

namespace Application.Services
{
    public class VacancyService : IVacancyService
    {

        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICacheService cacheService;
        private const string mainCashKey = "vacancy";

        public VacancyService(IMapper mapper, IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.cacheService = cacheService;
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

            var cacheKey = CacheHelper.BuildQueryCashKey(vacancyQuaryDto , "vacancies");

            var cachedVacancies = await cacheService.GetData<ListWithCountDto<VacancyDto>>(cacheKey);

            if (cachedVacancies != null)
            {
                return cachedVacancies;
            }

            var spec = vacancyQuaryDto.BuildSpecification()
                .WithLimit(vacancyQuaryDto.Limit)
                .WithPage(vacancyQuaryDto.Page)
                .Build();

            var vacancies = await unitOfWork.Repository<Vacancy>().GetAllAsync(spec);
            var count = await unitOfWork.Repository<Vacancy>().GetCountAsync(vacancyQuaryDto.BuildSpecification().Build());

            var result = new ListWithCountDto<VacancyDto>
            {
                Count = count,
                Items = mapper.Map<IEnumerable<VacancyDto>>(vacancies)
            };

            await cacheService.SetData(cacheKey, result); // Cache for 60 minutes

            return result;
        }

        public async Task<VacancyDto> GetVacancyyId(int id)
        {

            string cacheKey = CacheHelper.BuildCashKey(mainCashKey, id);

            // Try to get the cached data
            var cachedVacancy = await cacheService.GetData<VacancyDto>(cacheKey);

            if (cachedVacancy != null)
            {
                return cachedVacancy; // Return the cached data if available
            }

            var spec = new VacancySpecificationBuilder()
                .WithId(id)
                .Include(x => x.Employer)
                .Build();

            var vacancy = await this.unitOfWork.Repository<Vacancy>().GetOneAsync(spec);

            if (vacancy == null)
                throw new NotFoundException("Vacancy not exist");

            var vacancyDto = mapper.Map<VacancyDto>(vacancy);

            await cacheService.SetData(cacheKey, vacancyDto);

            return vacancyDto;
        }

        public async Task<VacancyDto> UpdateVacancy(int id, MangeVacancyDto updateVacancyDto)
        {

            string cacheKey = CacheHelper.BuildCashKey(mainCashKey, id);

            var repo = unitOfWork.Repository<Vacancy>();

            Vacancy vacancy = await CheckVacancy(id, updateVacancyDto.EmployerId);

            mapper.Map(updateVacancyDto, vacancy);

            repo.Update(vacancy);

            await unitOfWork.Compleate();

            var vacancyDto = mapper.Map<VacancyDto>(vacancy);

            // Remove the outdated cache and re-cache updated data
            await cacheService.RemoveData(cacheKey);

            // Cache the result for future use
            await cacheService.SetData(cacheKey, vacancyDto);

            return vacancyDto;

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

            string cacheKey = CacheHelper.BuildCashKey(mainCashKey, id);

            Vacancy vacancy = await CheckVacancy(id, employerId);

            unitOfWork.Repository<Vacancy>().Delete(vacancy);

            // Remove the ache 
            await cacheService.RemoveData(cacheKey);


            await unitOfWork.Compleate();


            return;
        }
    }
}
