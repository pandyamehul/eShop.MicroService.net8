namespace Order.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        //services.Addcarter();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication application)
    {
        //application.MapCarter();
        return application;
    }
}
