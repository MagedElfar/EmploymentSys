namespace API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services) {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IApplicationBuilder UseSwaggerMiddlewares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
