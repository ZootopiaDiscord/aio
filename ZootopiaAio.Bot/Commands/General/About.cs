using System.Diagnostics;
using DSharpPlus;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Entities;
using ZootopiaAio.Bot.Services;

namespace ZootopiaAio.Bot.Commands.General;

internal record AboutCommand(SlashCommandContext Ctx, bool IsPrivate) : SlashCommand(Ctx);

internal class AboutCommandHandler(CachingService cachingService, Version version) : ICommandHandler<AboutCommand>
{
    public async Task HandleAsync(AboutCommand command, CancellationToken cancellationToken = default)
    {
        var builder = new DiscordInteractionResponseBuilder().EnableV2Components();

        List<DiscordComponent> components = [];

        var currentProcess = Process.GetCurrentProcess();
        var startedAt = currentProcess.StartTime.ToUniversalTime();

        components.Add(new DiscordSectionComponent(
            [
                new DiscordTextDisplayComponent($"# {command.Ctx.Client.CurrentUser.Username}"),
                new DiscordTextDisplayComponent("""
                                                You say justice is dead. I say, "Neigh!"
                                                """)
            ],
            new DiscordThumbnailComponent(command.Ctx.Client.CurrentUser.AvatarUrl)
        ));

        components.Add(new DiscordSeparatorComponent(true, DiscordSeparatorSpacing.Large));

        components.Add(new DiscordSectionComponent(
            new DiscordTextDisplayComponent($"### Version **{version.ToString(3)}**"),
            new DiscordLinkButtonComponent("https://github.com/ZootopiaDiscord/aio/releases", "Release Notes")
        ));

        components.Add(new DiscordTextDisplayComponent($"""
                                                        Started {Formatter.Timestamp(startedAt)}
                                                        {Formatter.Timestamp(startedAt, TimestampFormat.ShortDateTime)}
                                                        """));

        components.Add(new DiscordSeparatorComponent(true, DiscordSeparatorSpacing.Large));

        components.Add(new DiscordTextDisplayComponent(
            $"""
             ## Materials
             -# This app is not directly affiliated with, endorsed by, maintained, authorized, or sponsored by Disney.
             -# It is provided free for non-commercial entertainment. This app is for non-profit/educational use only.
             -# All copyrights, trademarks, and logos are owned by their respective owners.
             ## Libraries
             -# {Formatter.MaskedUrl("DSharpPlus", new Uri("https://github.com/DSharpPlus/DSharpPlus"))} • MIT License

             Built on top of .NET and many Microsoft libraries
             -# All licensed under the MIT License
             """));

        if (cachingService.GetBannerUrl() is { } bannerUrl)
        {
            components.Add(new DiscordSeparatorComponent());
            var desc = $"Banner for {command.Ctx.Client.CurrentUser.Username}";
            components.Add(new DiscordMediaGalleryComponent(new DiscordMediaGalleryItem(bannerUrl, desc, false)));
        }
        else
        {
            components.Add(new DiscordSeparatorComponent(true, DiscordSeparatorSpacing.Large));
        }

        components.Add(new DiscordTextDisplayComponent(
            $"-# Created by {Formatter.MaskedUrl("Tawmy", new Uri("https://tawmy.dev"))}"));

        builder.AddContainerComponent(new DiscordContainerComponent(components));
        builder.AsEphemeral(command.IsPrivate);

        await command.Ctx.RespondAsync(builder);
    }
}