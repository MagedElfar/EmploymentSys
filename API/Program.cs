
using API.Extensions;
using API.Middlewares;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices();
            builder.Services.AddDatabaseConections();
            builder.Services.AddApplicationOptions();
            builder.Services.AddServices();
            builder.Services.AddSwaggerServices();
            builder.Services.AddAuthenticationServices();

            var app = builder.Build();

            await app.MigrateAndSeedDatabaseAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
