using Discord.Commands;
using Discord.WebSocket;

namespace OctoBot.Custom_Library
{
    public class ShardedCommandContextCustom : ShardedCommandContext
    {
        public string MessageContentForEdit { get; }

        public ShardedCommandContextCustom(DiscordShardedClient client, SocketUserMessage msg, string edit = null) :
            base(client, msg)
        {
            MessageContentForEdit = edit;
        }
    }
}