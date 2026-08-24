namespace ZootopiaAio.Web.Client;

/// <summary>
/// The contract between the server that serves the site content and the WebAssembly client that
/// fetches it. Shared so the route cannot drift between the two.
/// </summary>
public static class ContentApi
{
    /// <summary>Route the site content is served from.</summary>
    public const string ContentPath = "/api/content";
}
