using Application.Services;
using Core.DTOS;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace API.Extensions
{
    public static class CustomServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork , UntitOfWork>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IMediaStorageService, LocalStorageService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<IApplicantService , ApplicantService>();
            services.AddScoped<IEmailSender , GmailEmailSender>();
            return services;
        }
    }
}
