using Core.Options;

namespace API.Extensions
{
    public static class OptionsExtensions
    {
        public static IServiceCollection AddApplicationOptions(this IServiceCollection services)
        {

            var serviceProvider = services.BuildServiceProvider();
            var configration = serviceProvider.GetRequiredService<IConfiguration>();

            services.Configure<JwtOptions>(configration.GetSection("JWT"));
            services.Configure<GmailSmtpSettings>(configration.GetSection("GmailSmtpSettings"));

            return services;
        }
    }
}
