using API.Extensions;
using Core.DTOS.Shared;
using Core.DTOS.Vacancy;
using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;

namespace API.Controllers
{
    public class VacanciesController:BaseApiController
    {

        private readonly IVacancyService vacancyService;

        public VacanciesController(IVacancyService vacancyService)
        {
            this.vacancyService = vacancyService;
        }

        [HttpGet]
        [Authorize(Roles = $"{nameof(UserRole.Employer)},{nameof(UserRole.Applicant)}")]
        public async Task<ActionResult<IEnumerable<ListWithCountDto<VacancyDto>>>> GetUserOrders(
            [FromQuery] VacancyQuaryDto vacancyQuaryDto
        ){
            return Ok(await vacancyService.GetVacancyCountAll(vacancyQuaryDto));
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Employer))]
        public async Task<ActionResult<VacancyDto>> CreateVacancy(MangeVacancyDto addVacancyDto)
        {

            addVacancyDto.EmployerId = HttpContext.User.GetUserId();

            var vacancy = await vacancyService.CreateVacancy(addVacancyDto);

            return CreatedAtAction("GetVacancyById", new { id = vacancy.Id }, vacancy);

        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = nameof(UserRole.Employer))]
        public async Task<ActionResult<VacancyDto>> UpdateVacancy(int id , MangeVacancyDto updateVacancyDto)
        {

            updateVacancyDto.EmployerId = HttpContext.User.GetUserId();

            return Ok(await vacancyService.UpdateVacancy(id, updateVacancyDto));

        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = nameof(UserRole.Employer))]
        public async Task<ActionResult> ToggleVacancyStatus(int id)
        {

            var employerId = HttpContext.User.GetUserId();

            await vacancyService.ToggleVacancyStatus(id, employerId);

            return Ok(new { message = "Vacany status updated"});

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = nameof(UserRole.Employer))]
        public async Task<ActionResult> DeleteVacancy(int id)
        {

            var employerId = HttpContext.User.GetUserId();

            await vacancyService.DeleteVacancy(id, employerId);

            return Ok(new { message = "Vacany deleted" });

        }

        [HttpGet("{id:int}", Name = "GetVacancyById")]
        [Authorize(Roles = $"{nameof(UserRole.Employer)},{nameof(UserRole.Applicant)}")]
        public async Task<ActionResult<VacancyDto>> GetVacancyById(int id)
        {
            return Ok(await vacancyService.GetVacancyyId(id));
        }
    }
}
