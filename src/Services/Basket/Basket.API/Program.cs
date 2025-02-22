//Before building application
var builder = WebApplication.CreateBuilder(args);

//Add services to container - dependency injection

//Configure http request pipeline



// After building application 
var app = builder.Build();

//Run application
app.Run();
