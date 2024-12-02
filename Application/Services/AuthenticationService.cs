using AutoMapper;
using Core.DTOS.Authentication;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Stripe.Forwarding;

namespace Application.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJwtService tokenService;
        private readonly IMapper mapper;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IJwtService tokenService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        public async Task<AuthenticationDto> Login(LoaginDto loaginDto)
        {
            var user = await userManager.FindByEmailAsync(loaginDto.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, loaginDto.Password))
                throw new UserUnauthorizedException("Invalid Credentials");

            var roles = await userManager.GetRolesAsync(user);

            var token = await tokenService.GenerateToken(user , roles);

            var authenticationDto = mapper.Map<AuthenticationDto>(user);

            // Set token and roles
            authenticationDto.Token = token;
            authenticationDto.Roles = roles;
            authenticationDto.Id = user.Id;
            return authenticationDto;
        }


        public async Task<AuthenticationDto> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            ResultCheck(result);

            await userManager.AddToRoleAsync(user, registerDto.UserType);

            var token = await tokenService.GenerateToken(user);

            var authenticationDto = mapper.Map<AuthenticationDto>(user);

            // Set token and roles
            authenticationDto.Token = token;
            authenticationDto.Id = user.Id;
            authenticationDto.Roles = new [] {registerDto.UserType};
            return authenticationDto;

        }

       

        private void ResultCheck(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                throw new BadRequestException(err: errors);
            }
        }
    }
}
