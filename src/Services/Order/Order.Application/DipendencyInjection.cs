using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Order.Application;

public static class DipendencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddMediatR(config =>
        //{
        //    config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        //});
        return services;
    }
}
