using Carter;
using SocialNetwork.API.DependencyInjection.Extensions;
using SocialNetwork.API.Middleware;
using SocialNetwork.Application.DependencyInjection.Extensions;
using SocialNetwork.Persistence.DependencyInjection.Extensions;
using SocialNetwork.Persistence.DependencyInjection.Options;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using SocialNetwork.Infrastructure.DependencyInjection.Extensions;
using Serilog;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();
// Add Carter module => RememberMap
builder.Services.AddCarter();


// Add Swagger
builder.Services
    .AddSwaggerGenNewtonsoftSupport()
    .AddFluentValidationRulesToSwagger()
    .AddEndpointsApiExplorer()
    .AddSwagger();


builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });


builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddConfigureMediatR();
builder.Services.AddConfigureAutoMapper();


// Configure masstransit rabbitmq
builder.Services.AddMasstransitRabbitMQInfrastructure(builder.Configuration);
builder.Services.AddQuartzInfrastructure();
builder.Services.AddServicesInfrastructure();
builder.Services.AddRedisInfrastructure(builder.Configuration);
builder.Services.ConfigureServicesInfrastructure(builder.Configuration);

//// Configure Options and SQL => Remember mapcarter
builder.Services.AddInterceptorPersistence();
builder.Services.ConfigureSqlServerRetryOptionsPersistence(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddSqlServerPersistence();
builder.Services.AddRepositoryPersistence();

// Add Middleware 
builder.Services.AddTransient<ExceptionHandlingMiddleware>();



var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.


//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
// Add API Endpoint with carter module
app.MapCarter();
//app.MapControllers();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
    app.ConfigureSwagger();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

public partial class Program
{

}