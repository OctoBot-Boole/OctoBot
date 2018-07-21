using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;
using OctoBot.Configs;

namespace OctoBot.Helper
{
    public class AwaitForUserMessage
    {
        public static async Task<SocketMessage> AwaitMessage(ulong userId, ulong channelId, int delayInMs)
        {
            SocketMessage response = null;
            var cancler = new CancellationTokenSource();
            var waiter = Task.Delay(delayInMs, cancler.Token);

            Global.Client.MessageReceived += OnMessageReceived;
            try
            {
                await waiter;
            }
            catch (TaskCanceledException)
            {
            }

            Global.Client.MessageReceived -= OnMessageReceived;

            return response;

            async Task OnMessageReceived(SocketMessage message)
            {
                if (message.Author.Id != userId || message.Channel.Id != channelId)
                    return;
                response = message;
                cancler.Cancel();
                await Task.CompletedTask;
            }
        }
    }
}