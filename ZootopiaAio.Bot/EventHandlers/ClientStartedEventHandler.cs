using DSharpPlus;
using DSharpPlus.EventArgs;
using ZootopiaAio.Bot.Services;

namespace ZootopiaAio.Bot.EventHandlers;

/// <summary>
/// Event is fired early during startup -> we can be sure its code is run before commands are processed.
/// </summary>
internal sealed class ClientStartedEventHandler(CachingService cachingService) : IEventHandler<ClientStartedEventArgs>
{
    public Task HandleEventAsync(DiscordClient sender, ClientStartedEventArgs eventArgs)
    {
        cachingService.CacheBannerUrl(sender.CurrentUser.BannerUrl);
        return Task.CompletedTask;
    }
}