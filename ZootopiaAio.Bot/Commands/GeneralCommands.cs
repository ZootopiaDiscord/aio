using System.ComponentModel;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands;
using ZootopiaAio.Bot.Commands.General;

namespace ZootopiaAio.Bot.Commands;

internal sealed class GeneralCommands
{
    [Command("about")]
    [Description("Info about the app.")]
    public Task AboutAsync(SlashCommandContext ctx,
        [Parameter("private")] [Description(Messages.Commands.Parameters.Private)]
        bool isPrivate = false)
    {
        return ctx.HandleAsync(new AboutCommand(ctx, isPrivate));
    }
}