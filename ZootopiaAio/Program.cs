using ZootopiaAio.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBotServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();