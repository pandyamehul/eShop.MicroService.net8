
var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddValidatorsFromAssembly(assembly);

//Add Services to container
builder.Services.AddCarter();

//Add Marten library support for DB Crud operations
builder.Services.AddMarten( options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

//Pupulate default products if env. is dev
if(builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

//Add Custom and Generic Exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Initiate and Run application pipeline
var app = builder.Build();

//Configure HTTP request pipeline
app.MapCarter();

#region 20250217: Start - Commented below block to replace with generic implementation
// 20250217: Start - Commented below block to replace with generic implementation
//ASP.net core global exception handling
//app.UseExceptionHandler( exceptionHandlerApp =>
//{
//    exceptionHandlerApp.Run(async context =>
//    {
//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if(exception == null)
//        {
//            return;
//        }
//        var exceptionDetails = new ProblemDetails
//        {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace
//        };
//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";

//        await context.Response.WriteAsJsonAsync(exceptionDetails);
//    });
//});
// 20250217: End - Commented below block to replace with generic implementation
#endregion 20250217: End - Commented below block to replace with generic implementation

//Configure app to use custom exception handling, empty { } indicates pipeline is 
app.UseExceptionHandler(optios => { });

app.Run();