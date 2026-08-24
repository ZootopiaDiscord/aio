namespace ZootopiaAio.Web.Client;

/// <summary>
/// The JavaScript module backing the few behaviours that need direct access to the window, such as
/// scroll position and clicks on channel references anywhere on the page.
/// </summary>
internal static class Interop
{
    /// <summary>Import path of the module, relative to the page.</summary>
    public const string ModulePath = $"./{WebAssets.Root}/js/site.js";
}