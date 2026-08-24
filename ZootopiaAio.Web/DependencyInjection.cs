using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZootopiaAio.Web.Client;
using ZootopiaAio.Web.Client.Models;
using ZootopiaAio.Web.Client.Services;
using ZootopiaAio.Web.Services;

namespace ZootopiaAio.Web;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers the site content and options the components resolve. Razor component services
        /// are set up by the host.
        /// </summary>
        public void AddWebServices(IConfiguration configuration)
        {
            var inviteUrl = configuration.GetRequiredSection(EnvironmentVariables.InviteUrl).Value!;

            services.AddSingleton(new SiteOptions(inviteUrl));
            services.AddSingleton<SiteContentLoader>();

            // Scoped, so a new request or circuit picks up an edited content file.
            services.AddScoped(provider => provider.GetRequiredService<SiteContentLoader>().Current);
        }
    }

    extension(IEndpointRouteBuilder endpoints)
    {
        /// <summary>
        /// Maps the endpoint the WebAssembly client reads the site content from. The client cannot
        /// read the content file itself, and passing it through component parameters would serialize
        /// the whole site into the page.
        /// </summary>
        public IEndpointConventionBuilder MapWebEndpoints()
        {
            return endpoints.MapGet(ContentApi.ContentPath, (SiteContentLoader loader, HttpResponse response) =>
            {
                // Must be revalidated, otherwise an edited content file would not reach clients that
                // already have a copy.
                response.Headers.CacheControl = "no-cache";

                return TypedResults.Json(loader.Current, SiteContentJsonContext.Default.SiteContent);
            });
        }
    }
}
