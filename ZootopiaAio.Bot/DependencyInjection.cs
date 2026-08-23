using DSharpPlus;
using DSharpPlus.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddHostedService<BotService>();
        }
    }
}