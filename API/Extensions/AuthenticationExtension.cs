using Core.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services ) {

            var serviceProvider = services.BuildServiceProvider();
            var configration = serviceProvider.GetRequiredService<IConfiguration>();
            var jwtOptions = configration.GetSection("JWT").Get<JwtOptions>();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options => {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidIssuer = jwtOptions.Issuer,
                     ValidateAudience = false,
                     //ValidAudience = jwtOptions.Aduience,
                     ValidateIssuerSigningKey = true,
                     ValidateLifetime = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                 };
             });

            return services;
        }
    }
}
