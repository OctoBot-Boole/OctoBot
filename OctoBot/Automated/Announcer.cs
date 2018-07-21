using System.Threading.Tasks;
using Discord.WebSocket;

namespace OctoBot.Automated
{
    internal static class Announcer
    {
        internal static async Task AnnounceUserJoin(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;


            var kek = 1; // DELETE
            if (kek != 1) // DELETE
                await channel.SendMessageAsync($" {user.Mention}, Приветвсвую тебя в подводный мир осьминожек! ");
        }
    }
}