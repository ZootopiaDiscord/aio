namespace ZootopiaAio.Bot.Services;

internal sealed class CachingService
{
    private string? _bannerUrl;

    public void CacheBannerUrl(string? url)
    {
        _bannerUrl = url;
    }

    public string? GetBannerUrl()
    {
        return _bannerUrl;
    }
}