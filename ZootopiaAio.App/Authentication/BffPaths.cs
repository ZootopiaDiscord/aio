namespace ZootopiaAio.App.Authentication;

/// <remarks>
/// These are mirrored in <c>wwwroot/service-worker.published.js</c>, which has to let them through to the network,
/// and in <c>gateway/appsettings.json</c>, which routes them.
/// </remarks>
internal static class BffPaths
{
    /// <summary>The signed-in user's claims, or 401.</summary>
    public const string User = "bff/user";

    /// <summary>Sign-out for a single-page client, POST.</summary>
    public const string Logout = "/bff/logout";

    /// <summary>Sign-in, document navigation.</summary>
    public const string Login = "/login";
}