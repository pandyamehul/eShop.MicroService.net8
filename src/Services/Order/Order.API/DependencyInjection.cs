using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Order.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        services.AddCarter();

        // Add Generic Exception Handlers
        services.AddExceptionHandler<CustomExceptionHandler>();

        // Added Halth check probes
        services
            .AddHealthChecks() // Application Configuration checks 
            // dependent SQL Server health check
            .AddSqlServer(configuration.GetConnectionString("Database")!);

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
        // Mapping carter 
        application.MapCarter();

        // Configure to use generic exception handler
        application.UseExceptionHandler(options => { });

        // Configure health check status
        application
            .UseHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
        );

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

        return application;
    }
}
