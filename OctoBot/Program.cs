using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using OctoBot.Automated;
using OctoBot.Configs;
using OctoBot.Handeling;

namespace OctoBot
{
    public class ProgramOctoBot
    {
        private DiscordSocketClient _client;
        private IServiceProvider _services;

        private static void Main()
        {
            new ProgramOctoBot().RunBotAsync().GetAwaiter().GetResult();
        }

        public async Task RunBotAsync()
        {
            if (string.IsNullOrEmpty(Config.Bot.Token)) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                DefaultRetryMode = RetryMode.AlwaysRetry,
                MessageCacheSize = 2000
            });

            _services = ConfigureServices();
            _services.GetRequiredService<DiscordEventHandler>().InitDiscordEvents();
            await _services.GetRequiredService<CommandHandelingSendingAndUpdatingMessages>().InitializeAsync();

            var botToken = Config.Bot.Token;
            await _client.SetGameAsync("Осьминожек! | *help");

            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();
            Global.Client = _client;

            SendMessagesUsingConsole.ConsoleInput(_client);
            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandelingSendingAndUpdatingMessages>()
                .AddSingleton<DiscordEventHandler>()
                .AddSingleton<CheckIfCommandGiveRole>()
                .AddSingleton<ReactionsHandelingForBlogAndArt>()
                .AddSingleton<GiveRoleOnJoin>()
                .AddSingleton<LvLing>()
                .AddSingleton<CheckForVoiceChannelStateForVoiceCommand>()
                .AddSingleton<UserSkatisticsCounter>()
                .AddScoped<ServerActivityLogger>()
                .BuildServiceProvider();
        }
    }
}