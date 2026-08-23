using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZootopiaAio.Bot.Commands;
using ZootopiaAio.Bot.Services;

namespace ZootopiaAio.Bot;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddBotServices(IConfiguration configuration)
        {
            var token = configuration.GetRequiredSection(EnvironmentVariables.BotToken).Value!;

            services.AddDiscordClient(token, DiscordIntents.None).Configure<DiscordConfiguration>(x =>
            {
                x.LogUnknownAuditlogs = false;
                x.LogUnknownEvents = false;
            });

            services.AddCommandsExtension((_, x) =>
            {
                x.AddProcessor<SlashCommandProcessor>();

                x.AddCommands<GeneralCommands>();
            });

            services.AddCommandHandlers();

            services.AddHostedService<BotService>();
        }

        private void AddCommandHandlers()
        {
            var types = typeof(DependencyInjection).Assembly.GetTypes()
                .Where(x => x is { IsClass: true, IsAbstract: false, ContainsGenericParameters: false });

            foreach (var type in types)
            {
                foreach (var handlerInterface in type.GetInterfaces().Where(IsCommandHandler))
                {
                    services.AddTransient(handlerInterface, type);
                }
            }

            return;

            static bool IsCommandHandler(Type type)
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICommandHandler<>);
            }
        }
    }
}