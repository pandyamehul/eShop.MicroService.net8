using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data;

public static class Extensions
{
    /// <summary>
    /// UseMigration : Below code added to use migration steps via code when app is started and db is not available
    /// </summary>
    /// <param name="app">Instance of Application Builder</param>
    /// <returns></returns>
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        //perform auto migration, pending migration, also create db if not exists
        dbContext.Database.MigrateAsync();
        return app;
    }
}
