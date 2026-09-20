using ZootopiaAio;
using ZootopiaAio.Bot;
using ZootopiaAio.Components;
using ZootopiaAio.Web;
using ZootopiaAio.Web.Components;

var builder = WebApplication.CreateBuilder(args);

var version = typeof(Program).Assembly.GetName().Version!;
builder.Services.AddSingleton(version);

builder.Services.AddAuthenticationServices(builder.Configuration);

builder.Services.AddBotServices(builder.Configuration);

builder.Services.AddWebServices(builder.Configuration);
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapAuthenticationEndpoints();
app.MapWebEndpoints();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Routes).Assembly);

app.Logger.LogInformation("Zootopia AIO, Version {v}", version.ToString(3));

app.Run();