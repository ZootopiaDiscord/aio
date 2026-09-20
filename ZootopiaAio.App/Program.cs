using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ZootopiaAio.App;
using ZootopiaAio.App.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var version = typeof(Program).Assembly.GetName().Version!;

builder.Services.AddScoped(sp => new HttpClient(
    new RedirectToLoginHandler(sp.GetRequiredService<NavigationManager>()) { InnerHandler = new HttpClientHandler() })
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, BffAuthenticationStateProvider>();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("ZootopiaAio.App");
logger.LogInformation("Zootopia App, Version {v}", version.ToString(3));

await app.RunAsync();