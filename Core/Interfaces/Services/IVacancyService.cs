using Core.DTOS.Shared;
using Core.DTOS.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IVacancyService
    {
        Task<VacancyDto> CreateVacancy(MangeVacancyDto addVacancyDto);
        Task<VacancyDto> UpdateVacancy(int id ,MangeVacancyDto updateVacancyDto);
        Task<VacancyDto> GetVacancyyId(int id);
        Task<ListWithCountDto<VacancyDto>> GetVacancyCountAll(VacancyQuaryDto vacancyQuaryDto);
        Task ToggleVacancyStatus(int id, int employerId);
        Task DeleteVacancy(int id, int employerId);

    }
}
