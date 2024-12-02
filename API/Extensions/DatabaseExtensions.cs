using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseConections(this IServiceCollection services) {

            var serviceProvider = services.BuildServiceProvider();
            var configration = serviceProvider.GetRequiredService<IConfiguration>();

            services.AddDbContext<AdbContext>(opthions =>
            {
                opthions.UseSqlServer(configration.GetConnectionString("Default"));
            });

            services
               .AddIdentity<ApplicationUser, ApplicationRole>(options =>
               {
                   options.User.RequireUniqueEmail = true;
                   options.Password.RequireDigit = false;            // Require at least one digit (can disable)
                   options.Password.RequireLowercase = false;        // Require at least one lowercase character (can disable)
                   options.Password.RequireUppercase = false;        // Require at least one uppercase character (can disable)
                   options.Password.RequireNonAlphanumeric = false;
               })
               .AddSignInManager<SignInManager<ApplicationUser>>()
               .AddEntityFrameworkStores<AdbContext>()
               .AddDefaultTokenProviders();


            return services;
        }


        public static async Task MigrateAndSeedDatabaseAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AdbContext>();
                var userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManger = services.GetRequiredService<RoleManager<ApplicationRole>>();

                try
                {
                    // Apply migrations
                    await context.Database.MigrateAsync();
                    await AdbContextSeed.SeedRolesAsync(roleManger);

                    // Seed the database
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during migration or seeding.");
                }
            }
        }
    }
}
