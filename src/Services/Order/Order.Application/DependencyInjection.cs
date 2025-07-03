using BuildingBlocks.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Order.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });
        return services;
    }
}
