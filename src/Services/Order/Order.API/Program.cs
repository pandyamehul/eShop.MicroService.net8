// Add required services to container
var builder = WebApplication.CreateBuilder(args);

// Configure Http request pipeline
var app = builder.Build();

// Run application
app.Run();