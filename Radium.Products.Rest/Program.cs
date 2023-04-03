using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Radium.Products.Infrastructure.Middlewares;
using Radium.Products.Rest.Extensions;
using Serilog;
using Radium.Products.Infrastructure.Extensions;
using Radium.Products.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.ConfigureApplicationServices();
    builder.Services.ConfigureDatabase(builder.Configuration);
    builder.Services.ConfigureVersionedSwagger();

    var app = builder.Build();

    var hostingEnvironment = app.Services.GetRequiredService<IHostEnvironment>();

    app.Services.MigrateDatabase();

    if (hostingEnvironment.IsDevelopment())
    {
        app.Services.SeedDatabase();
    }

    app.UseSerilogRequestLogging();
    app.UseSwagger();

    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(c =>
    {
        foreach (var group in provider.ApiVersionDescriptions.Select(x => x.GroupName))
        {
            c.SwaggerEndpoint($"/swagger/{group}/swagger.json", $"Radium Products Rest API Version {group.ToUpperInvariant()}");
        }

        c.OAuthAppName("Radium Products Rest API - Swagger");
    });

    app.ConfigureCustomExceptionMiddleware();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.Run();

}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception while bootstrapping application");
}
finally
{
    Log.Information("Shutting down...");
    Log.CloseAndFlush();
}
public partial class Program { }