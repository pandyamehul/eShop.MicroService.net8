using BuildingBlocks.Behaviours;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Order.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });

        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}
