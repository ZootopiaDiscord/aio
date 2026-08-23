using ZootopiaAio.Bot;

var builder = WebApplication.CreateBuilder(args);

var version = typeof(Program).Assembly.GetName().Version!;
builder.Services.AddSingleton(version);

builder.Services.AddBotServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Logger.LogInformation("Winddancer, Version {v}", version.ToString(3));

app.Run();