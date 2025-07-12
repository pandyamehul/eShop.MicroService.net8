var builder = WebApplication.CreateBuilder(args);

// Add application services to container

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// configuration http request pipeline
app.MapReverseProxy();

app.Run();
