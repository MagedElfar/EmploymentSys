using Application.Mappings;
using Core.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace API.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {

            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value?.Errors?.Count > 0)
                    .SelectMany(
                        e => e.Value?.Errors,
                       (e, msg) => $"{e.Key}: {msg.ErrorMessage}"

                    )
                    .ToArray();

                    var errorsResponse = new ApiValidationErrorResponse { Errors = errors };

                    return new BadRequestObjectResult(errorsResponse);

                };

            });

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin() 
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
