using BuildingBlocks.Messaging.MassTransit;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

// ------------------------ Configure ASP.Net request pipeline Pre build --------------------------------//
//Before building application

var builder = WebApplication.CreateBuilder(args);

// -- Application Services --//
#region Application Services
//Add services to container - dependency injection
builder.Services.AddCarter();

//Configure http request pipeline
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    //adding validator behavior in pipeline
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    //add logging behaviour for general logging in pipeline for every mediatr request
    //centralize logging
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
#endregion

// -- Database Services --//
#region Database Services
//Add Marten library support for DB Crud operations
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(schema => schema.UserName);
}).UseLightweightSessions();

//Add repository to container
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

//Add suport for Redis Cahce
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.InstanceName = "Basket_";
});
#endregion

// -- Grpc Services --//
// TODO: Add Grps service dependencies
#region Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUri"]!);
})
// Below code by pass trusted certificate validation at server end.
// This setup is only recommended in Dev env. and not cloud and prod env.
// below code will accept any cert provided by client
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return handler;
});
#endregion

// -- Async Communication Services (publish to RabbitMq using MassTransit) --//
#region Async Communication Services
// Note* - No Assembly parameters passed here, as Basket API is publisher here
builder.Services.AddMessageBroker(builder.Configuration);
#endregion

// -- Common, cross cuting and health check Services --//
#region Common, cross cutting and health services
//Add Custom and Generic Exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Add support for health checks
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
#endregion


// ------------------------ Configure ASP.Net request pipeline Post build --------------------------------//

// After building application 
var app = builder.Build();

//Configure http request pipeline
app.MapCarter();

//Configure app to use custom exception handling, empty { } indicates pipeline is 
app.UseExceptionHandler(optios => { });

//Configure health checks
app.UseHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);

//Run application
app.Run();
