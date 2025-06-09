// Add required services to container
using Order.API;
using Order.Application;
using Order.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    // App Services - MediatR
    .AddApplicationServices()
    // Infra Services - EF Core
    .AddInfrastructureServices(builder.Configuration)
    // API Service - Carter, HealthCheck
    .AddApiServices();

// Configure Http request pipeline
var app = builder.Build();

// Run application
app.Run();