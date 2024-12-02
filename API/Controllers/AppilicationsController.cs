using API.Extensions;
using Application.Services;
using Core.DTOS;
using Core.DTOS.Application;
using Core.DTOS.Vacancy;
using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AppilicationsController:BaseApiController
    {

        private readonly IApplicantService applicantService;

        public AppilicationsController(IApplicantService applicantService)
        {
            this.applicantService = applicantService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Applicant))]
        public async Task<ActionResult<VacancyDto>> CreateVacancy(ApplyForVacancyDto applyForVacancyDto)
        {

            var applicantId = HttpContext.User.GetUserId();

            var vacancy = await applicantService.ApplyForVacancyAsync(applicantId , applyForVacancyDto.VacancyId);

            return Ok(vacancy);

        }


        [HttpGet("{vacancyId:int}", Name = "GetApplicationById")]
        [Authorize(Roles = nameof(UserRole.Applicant))]
        public async Task<ActionResult<VacancyDto>> GetVacancyById([FromRoute] int vacancyId)
        {
            var applicantId = HttpContext.User.GetUserId();
            return Ok(await applicantService.GetApplicatioAsync(applicantId, vacancyId));
        }

        [HttpGet("vacancy")]
        [Authorize(Roles = nameof(UserRole.Employer))]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetVacancyApplications(
            [FromQuery] VacancyApplicationQueryDto vacancyApplicationQueryDto
            )
        {
            vacancyApplicationQueryDto.EmployerId = HttpContext.User.GetUserId();

            return Ok(await applicantService.GetVacancyApplicatios(vacancyApplicationQueryDto));
        }
    }
}
