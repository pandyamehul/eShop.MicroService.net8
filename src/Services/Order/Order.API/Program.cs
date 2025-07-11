// Add required services to container
using Order.API;
using Order.Application;
using Order.Infrastructure;
using Order.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    // App Services - MediatR, MassTransit
    .AddApplicationServices(builder.Configuration)
    // Infra Services - EF Core
    .AddInfrastructureServices(builder.Configuration)
    // API Service - Carter, HealthCheck
    .AddApiServices(builder.Configuration);

// Configure Http request pipeline
var app = builder.Build();

// Use API Services
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

// Run application
app.Run();