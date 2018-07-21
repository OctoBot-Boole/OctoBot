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

        public async Task MessageReceived(SocketMessage message, DiscordSocketClient client)
        {
            try
            {
                if (message.Author.IsBot)
                    return;

                var context = new SocketCommandContextCustom(client, message as SocketUserMessage);
                var account = UserAccounts.GetAccount(context.User, context.Guild.Id);
                var guild = ServerAccounts.GetServerAccount(context.Guild);

                var myPrefix = 1;
                if (account.MyPrefix != null)
                    myPrefix = account.MyPrefix.Length;

                var rolesToGiveList = guild.Roles.ToList();

                var guildCheck = context.Message.Content.Substring(guild.Prefix.Length,
                    message.Content.Length - guild.Prefix.Length);

                if (rolesToGiveList.Any(x => x.Key == guildCheck))
                {
                    SocketRole roleToAdd = null;

                    foreach (var t in rolesToGiveList)
                        if (t.Key == guildCheck)
                            roleToAdd = context.Guild.Roles.SingleOrDefault(x => x.Name.ToString() == t.Value);


                    if (!(context.User is SocketGuildUser guildUser) || roleToAdd == null)
                        return;

                    var roleList = guildUser.Roles.ToArray();

                    if (roleList.Any(t => t.Name == roleToAdd.Name))
                    {
                        await guildUser.RemoveRoleAsync(roleToAdd);
                        await CommandHandelingSendingAndUpdatingMessages.SendingMess(context, $"-{roleToAdd.Name}");
                        return;
                    }

                    await guildUser.AddRoleAsync(roleToAdd);
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(context, $"+{roleToAdd.Name}");
                }
                else
                {
                    var userCheck = context.Message.Content.Substring(myPrefix, message.Content.Length - myPrefix);
                    if (rolesToGiveList.Any(x => x.Key == userCheck))
                    {
                        SocketRole roleToAdd = null;

                        foreach (var t in rolesToGiveList)
                            if (t.Key == userCheck)
                                roleToAdd = context.Guild.Roles.SingleOrDefault(x => x.Name.ToString() == t.Value);


                        if (!(context.User is SocketGuildUser guildUser) || roleToAdd == null)
                            return;

                        var roleList = guildUser.Roles.ToArray();

                        if (roleList.Any(t => t.Name == roleToAdd.Name))
                        {
                            await guildUser.RemoveRoleAsync(roleToAdd);
                            await CommandHandelingSendingAndUpdatingMessages.SendingMess(context, $"-{roleToAdd.Name}");
                            return;
                        }

                        await guildUser.AddRoleAsync(roleToAdd);
                        await CommandHandelingSendingAndUpdatingMessages.SendingMess(context, $"+{roleToAdd.Name}");
                    }
                }
            }
            catch
            {
                //  ignored
            }
        }

        public async Task Client_MessageReceived(SocketMessage message, DiscordSocketClient client)
        {
            MessageReceived(message, client);
        }
    }
}