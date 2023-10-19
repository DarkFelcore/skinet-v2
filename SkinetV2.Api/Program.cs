using Microsoft.EntityFrameworkCore;
using SkinetV2.Api;
using SkinetV2.Application;
using SkinetV2.Infrastructure;
using SkinetV2.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.MapControllers();

    // Apply automatic migrations and seed data into the database
    using (var scope = app.Services.CreateScope())
    {
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        try
        {
            var storeContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
            await storeContext.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(storeContext, loggerFactory);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occured during migration");
        }
    }

    app.Run();
}

