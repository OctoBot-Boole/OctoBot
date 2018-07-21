using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs.Users;

namespace OctoBot.Automated
{
    public class UserSkatisticsCounter
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.


        public async Task MessageReceived(SocketMessage msg)
        {
            var channel = msg.Channel as IGuildChannel;
            var guild = channel?.Guild;
            if (guild == null) return;
            var account = UserAccounts.GetAccount(msg.Author, guild.Id);
            account.UserStatistics.AddOrUpdate("all", 1, (key, value) => value + 1);
            account.UserStatistics.AddOrUpdate($"{msg.Channel.Id}", 1, (key, value) => value + 1);
            UserAccounts.SaveAccounts(guild.Id);
        }

        public async Task Clien_MessageReceived(SocketMessage msg)
        {
            MessageReceived(msg);
        }

        public async Task MessageDeleted(Cacheable<IMessage, ulong> cacheMessage, ISocketMessageChannel socketChannel)
        {
            var channel = socketChannel as IGuildChannel;
            var guild = channel?.Guild;
            if (guild == null) return;
            var account = UserAccounts.GetAccount(cacheMessage.Value.Author, guild.Id);
            account.UserStatistics.AddOrUpdate("deleted", 1, (key, value) => value + 1);
            UserAccounts.SaveAccounts(guild.Id);
        }

        public async Task Client_MessageDeleted(Cacheable<IMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
            MessageDeleted(cacheMessage, channel);
        }


        public async Task MessageUpdated(Cacheable<IMessage, ulong> cacheMessageBefore, SocketMessage messageAfter,
            ISocketMessageChannel socketChannel)
        {
            var channel = socketChannel as IGuildChannel;
            var guild = channel?.Guild;
            if (guild == null || messageAfter == null) return;
            var account = UserAccounts.GetAccount(cacheMessageBefore.Value.Author, guild.Id);
            account.UserStatistics.AddOrUpdate("updated", 1, (key, value) => value + 1);
            UserAccounts.SaveAccounts(guild.Id);
        }

        public async Task Client_MessageUpdated(Cacheable<IMessage, ulong> cacheMessageBefore,
            SocketMessage messageAfter,
            ISocketMessageChannel channel)
        {
            MessageUpdated(cacheMessageBefore, messageAfter, channel);
        }
    }
}