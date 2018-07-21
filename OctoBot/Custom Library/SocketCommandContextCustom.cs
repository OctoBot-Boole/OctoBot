using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace OctoBot.Custom_Library
{
    public class SocketCommandContextCustom : ICommandContext
    {
        public DiscordSocketClient Client { get; }

        public SocketGuild Guild { get; }

        public ISocketMessageChannel Channel { get; }

        public SocketUser User { get; }

        public SocketUserMessage Message { get; }

        public string MessageContentForEdit { get; }

        public bool IsPrivate => Channel is IPrivateChannel;

        public SocketCommandContextCustom(DiscordSocketClient client, SocketUserMessage msg, string edit = null)
        {
            Client = client;
            Guild = (msg.Channel as SocketGuildChannel)?.Guild;
            Channel = msg.Channel;
            User = msg.Author;
            Message = msg;
            MessageContentForEdit = edit;
        }

        IDiscordClient ICommandContext.Client => Client;

        IGuild ICommandContext.Guild => Guild;

        IMessageChannel ICommandContext.Channel => Channel;

        IUser ICommandContext.User => User;

        IUserMessage ICommandContext.Message => Message;
    }
}