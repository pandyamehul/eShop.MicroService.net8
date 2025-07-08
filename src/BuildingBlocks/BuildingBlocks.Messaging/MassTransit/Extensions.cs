using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTransit;
public static class Extentions
{
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services, 
        IConfiguration configuration, 
        // null is for publisher
        Assembly? assembly = null
    )
    {
        // Implement RabbitMQ - MassTransit Configurations
        services.AddMassTransit(config =>
        {
            // set name convension for configration
            config.SetKebabCaseEndpointNameFormatter();

            // Check for consumers to register automatically and consumer messages
            if (assembly != null)
                config.AddConsumers(assembly);

            // Configure RabbitMQ connection
            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:UserName"]!);
                    host.Password(configuration["MessageBroker:Password"]!);
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}