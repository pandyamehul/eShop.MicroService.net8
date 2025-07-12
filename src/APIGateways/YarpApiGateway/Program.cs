var builder = WebApplication.CreateBuilder(args);

// Add application services to container

var app = builder.Build();

// configuration http request pipeline

app.Run();
