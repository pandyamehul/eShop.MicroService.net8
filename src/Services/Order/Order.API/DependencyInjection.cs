namespace Order.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();

        // Add OpenAPI/Swagger services
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Order API",
                Version = "v1",
                Description = "Order microservice API for eShop application",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "eShop Team",
                    Email = "support@eshop.com"
                }
            });
        });

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication application)
    {
        // Configure Swagger middleware
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
                c.RoutePrefix = "swagger";
            });
        }

        application.MapCarter();
        return application;
    }
}
