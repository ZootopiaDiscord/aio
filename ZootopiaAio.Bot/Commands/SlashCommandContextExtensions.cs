using DSharpPlus.Commands.Processors.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using ZootopiaAio.Bot.Commands.General;

namespace ZootopiaAio.Bot.Commands;

internal static class SlashCommandContextExtensions
{
    extension(SlashCommandContext ctx)
    {
        public Task HandleAsync<T>(T command) where T : SlashCommand
        {
            return ctx.ServiceProvider.GetRequiredService<ICommandHandler<T>>().HandleAsync(command);
        }
    }
}