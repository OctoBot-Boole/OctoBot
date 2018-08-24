using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using OctoBot.Automated;
using OctoBot.Commands;
using OctoBot.Configs;
using OctoBot.Handeling;
using OctoBot.Helper;

namespace OctoBot
{
    public class ProgramOctoBot
    {
        private DiscordShardedClient _client;
        private IServiceProvider _services;
        private readonly int[] _shardIds = { 0,1,2};

        private static void Main()
        {
            new ProgramOctoBot().RunBotAsync().GetAwaiter().GetResult();
        }

        public async Task RunBotAsync()
        {
            if (string.IsNullOrEmpty(Config.Bot.Token)) return;
            _client = new DiscordShardedClient(_shardIds, new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                DefaultRetryMode = RetryMode.AlwaysRetry,
                MessageCacheSize = 100,
                TotalShards = 3
            });

            _services = ConfigureServices();
            _services.GetRequiredService<DiscordEventHandler>().InitDiscordEvents();
            await _services.GetRequiredService<CommandHandeling>().InitializeAsync();

            var botToken = Config.Bot.Token;
            await _client.SetGameAsync("Boole! | *help");

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
                .AddSingleton<CommandHandeling>()
                .AddSingleton<DiscordEventHandler>()
                .AddSingleton<CheckIfCommandGiveRole>()
                .AddSingleton<ReactionsHandelingForBlogAndArt>()
                .AddSingleton<GiveRoleOnJoin>()
                .AddSingleton<LvLing>()
                .AddSingleton<CheckForVoiceChannelStateForVoiceCommand>()
                .AddSingleton<UserSkatisticsCounter>()
                .AddSingleton<SecureRandom>()
                .AddSingleton<TimerForChangingAvatar>()
                .AddSingleton<OctopusPic>()
                .AddSingleton<Reminder>()
                .AddScoped<ServerActivityLogger>()
                .BuildServiceProvider();

        }
    }
}