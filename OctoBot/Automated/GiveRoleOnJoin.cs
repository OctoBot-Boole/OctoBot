using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using OctoBot.Configs.Server;

namespace OctoBot.Automated
{
    public class GiveRoleOnJoin
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        public async Task UserJoined_ForRoleOnJoin(SocketGuildUser arg)
        {
            var guid = ServerAccounts.GetServerAccount(arg.Guild);

            if (guid.RoleOnJoin == null)
                return;

            var roleToGive = arg.Guild.Roles
                .SingleOrDefault(x => x.Name.ToString().ToLower() == $"{guid.RoleOnJoin.ToLower()}");

            await arg.AddRoleAsync(roleToGive);
        }

        public async Task Client_UserJoined_ForRoleOnJoin(SocketGuildUser arg)
        {
            UserJoined_ForRoleOnJoin(arg);
        }
    }
}