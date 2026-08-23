using ZootopiaAio.Bot.Commands.General;

namespace ZootopiaAio.Bot.Commands;

internal interface ICommandHandler<in T> where T : SlashCommand
{
    Task HandleAsync(T request, CancellationToken cancellationToken = default);
}