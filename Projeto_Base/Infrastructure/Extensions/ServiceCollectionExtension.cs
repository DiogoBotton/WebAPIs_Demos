using Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static async Task RunDatabaseMigrations<T>(this WebApplication app, IServiceProvider serviceProvider, ILogger logger)
    {
        if (app is null)
            throw new ArgumentNullException(nameof(app));

        logger.LogInformation("Program start, applying migrations");

        try
        {
            var db = serviceProvider.GetRequiredService<ApiDbContext>();

            await db.Database.MigrateAsync();

            logger.LogInformation("Migrations applied");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Migrations error");
        }
    }
}
