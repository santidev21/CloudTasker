using CloudTasker.Api;
using CloudTasker.Api.Data;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Configure Functions
builder.ConfigureFunctionsWebApplication();

// Register services
builder.Services.AddLogging();
builder.Services.AddHttpClient(); 
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();


builder.Build().Run();