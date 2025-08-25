using CloudTasker.Api;
using CloudTasker.Api.Data;
using Microsoft.Azure.Cosmos;
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

// Configure Cosmos
var endpoint = builder.Configuration["CosmosDb"];
var key = builder.Configuration["Cosmos:Key"];
var databaseName = builder.Configuration["Cosmos:Database"];
var containerName = builder.Configuration["Cosmos:Container"];

var cosmosClient = new CosmosClient(endpoint, key);
var container = cosmosClient.GetContainer(databaseName, containerName);

// Register services
builder.Services.AddLogging();
builder.Services.AddHttpClient();
builder.Services.AddSingleton(container);
builder.Services.AddSingleton<ITaskRepository, CosmosTaskRepository>();


builder.Build().Run();