// ------------------------ Configure ASP.Net request pipeline Pre build --------------------------------//
//Before building application
using BuildingBlocks.Behaviours;

var builder = WebApplication.CreateBuilder(args);

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

//Add Marten library support for DB Crud operations
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(schema => schema.UserName );
}).UseLightweightSessions();


// ------------------------ Configure ASP.Net request pipeline Post build --------------------------------//
// After building application 
var app = builder.Build();

//Configure http request pipeline
app.MapCarter();

//Run application
app.Run();
