namespace ZootopiaAio.Web.Client;

/// <summary>
/// Paths to the site's static assets. The files themselves ship in ZootopiaAio.Web, which serves
/// them from <c>_content/{assembly name}</c>. That is a URL rather than a code dependency, so this
/// project does not reference the library holding them.
/// </summary>
public static class WebAssets
{
    /// <summary>Root path every asset of the site is served from.</summary>
    public const string Root = "_content/ZootopiaAio.Web";

    /// <summary>Path to an image below the asset root's <c>img</c> folder.</summary>
    public static string Image(string path)
    {
        return $"{Root}/img/{path}";
    }
}
