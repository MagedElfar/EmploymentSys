using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Services;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services) {

            var serviceProvider = services.BuildServiceProvider();
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configretion = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);

                configretion.AbortOnConnectFail = true;

                return ConnectionMultiplexer.Connect(configretion);
            });

            services.AddScoped<ICacheService, CashService>();

            return services;
        }
    }
}
