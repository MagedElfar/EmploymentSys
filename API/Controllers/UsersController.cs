using API.Extensions;
using Core.DTOS.User;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }


        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<UserDto>> GetCurrentUsert()
        {

            var userId= HttpContext.User.GetUserId();

            var user = await usersService.GetUserByIdAsync(userId);

            var roles = HttpContext.User.GetUserRoles();

            user.Roles = roles;

            return user;
        }


        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateCurrentUsert(UpdateUserDto updateUserDto)
        {

            var userId = HttpContext.User.GetUserId();

            var user = await usersService.UpdateUserAsync(userId, updateUserDto);

            var roles = HttpContext.User.GetUserRoles();

            user.Roles = roles;

            return user;
        }

    }
}
