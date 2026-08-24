namespace ZootopiaAio.Web;

/// <summary>
/// Values the site needs that are deployment specific rather than content.
/// </summary>
/// <param name="InviteUrl">Discord invite link the join button points to.</param>
public sealed record SiteOptions(string InviteUrl);