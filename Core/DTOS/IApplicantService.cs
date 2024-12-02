using Core.DTOS.Application;
using Core.DTOS.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS
{
    public interface IApplicantService
    {
        Task<ApplicationDto> GetApplicatioAsync(int applicantId, int vacancyId);
        Task<ListWithCountDto<ApplicationDto>> GetVacancyApplicatios(VacancyApplicationQueryDto vacancyApplicationQueryDto);
        Task<ApplicationDto> ApplyForVacancyAsync(int applicantId, int vacancyId);

    }
}
