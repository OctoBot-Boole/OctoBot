using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using OctoBot.Configs.Server;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Handeling;

namespace OctoBot.Automated
{
    public class CheckIfCommandGiveRole
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        public async Task MessageReceived(SocketMessage message, DiscordShardedClient client)
        {
            try
            {
                if (message.Author.IsBot)
                    return;

                var context = new ShardedCommandContextCustom(client, message as SocketUserMessage);
                var account = UserAccounts.GetAccount(context.User, context.Guild.Id);
                var guild = ServerAccounts.GetServerAccount(context.Guild);

                var rolesToGiveList = guild.Roles.ToList();

                var roleToGive = "2gagwerweghsxbd";


                var userCheck = "ju";
                if (account.MyPrefix !=null)
                    userCheck = context.Message.Content.Substring(0, account.MyPrefix.Length);
                var serverCheck = context.Message.Content.Substring(0, guild.Prefix.Length);

                if (serverCheck == guild.Prefix)
                {
                    roleToGive = context.Message.Content.Substring(guild.Prefix.Length,
                        message.Content.Length - guild.Prefix.Length);
                }

                if (userCheck == account.MyPrefix)
                {
                    roleToGive = context.Message.Content.Substring(account.MyPrefix.Length,
                        message.Content.Length - account.MyPrefix.Length);
                }
                if(userCheck != account.MyPrefix && serverCheck != guild.Prefix)
                    return;
                if (rolesToGiveList.Any(x => string.Equals(x.Key, roleToGive, StringComparison.CurrentCultureIgnoreCase)))
                {
                    SocketRole roleToAdd = null;

                    foreach (var t in rolesToGiveList)
                        if (string.Equals(t.Key, roleToGive, StringComparison.CurrentCultureIgnoreCase) )
                            roleToAdd = context.Guild.Roles.SingleOrDefault(x => x.Name.ToString().ToLower() == t.Value.ToLower());


                    if (!(context.User is SocketGuildUser guildUser) || roleToAdd == null)
                        return;

                    var roleList = guildUser.Roles.ToArray();

                    if (roleList.Any(t => string.Equals(t.Name, roleToAdd.Name, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        await guildUser.RemoveRoleAsync(roleToAdd);
                        await CommandHandeling.ReplyAsync(context, $"-{roleToAdd.Name}");
                        return;
                    }

                    await guildUser.AddRoleAsync(roleToAdd);
                    await CommandHandeling.ReplyAsync(context, $"+{roleToAdd.Name}");
                }
            }
            catch
            {
                //  ignored
            }
        }

        public async Task Client_MessageReceived(SocketMessage message, DiscordShardedClient client)
        {
            MessageReceived(message, client);
        }
    }
}