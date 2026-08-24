using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ZootopiaAio.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Awaited before the host is built so every component can take the content as a plain dependency,
// exactly as it does when the same component renders on the server.
var content = await SiteContentClient.FetchAsync(builder.HostEnvironment.BaseAddress);

builder.Services.AddSingleton(content);

await builder.Build().RunAsync();
