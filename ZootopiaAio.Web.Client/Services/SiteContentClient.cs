using System.Net.Http.Json;
using ZootopiaAio.Web.Client.Models;

namespace ZootopiaAio.Web.Client.Services;

/// <summary>
/// Fetches the site content from the server's content endpoint.
/// </summary>
public static class SiteContentClient
{
    /// <summary>
    /// Reads the content from <see cref="ContentApi.ContentPath" />.
    /// </summary>
    /// <remarks>
    /// Called during startup, before the host is built, so the content is already in the container by
    /// the time a component renders. That keeps the components synchronous and avoids blanking
    /// content that the server already prerendered.
    /// </remarks>
    /// <exception cref="InvalidOperationException">The endpoint returned no content.</exception>
    public static async Task<SiteContent> FetchAsync(string baseAddress)
    {
        using var http = new HttpClient();
        http.BaseAddress = new Uri(baseAddress);

        var content = await http.GetFromJsonAsync(ContentApi.ContentPath.TrimStart('/'),
            SiteContentJsonContext.Default.SiteContent);

        return content ?? throw new InvalidOperationException(
            $"'{ContentApi.ContentPath}' returned no site content.");
    }
}