using Core.DTOS.Authentication;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationDto>> Login(LoaginDto loaginDto)
        {
            var user = await authenticationService.Login(loaginDto);

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationDto>> Register(RegisterDto registerDto)
        {
            var user = await authenticationService.Register(registerDto);

            return Ok(user);
        }

        
    }
}
