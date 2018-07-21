using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Server;
using OctoBot.Custom_Library;
using OctoBot.Handeling;

namespace OctoBot.Commands
{
    public class StatsServer : ModuleBase<SocketCommandContextCustom>
    {
        [Command("topRoles")]
        [Alias("topr")]
        [Description("Top by Roles (Statistics fore Roles in the Guild)")]
        public async Task TopByRoles(int page = 1)
        {
            try
            {
                if (page < 1)
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context,
                        "Boole! Try different page <_<");
                    return;
                }

                var rolesList = Context.Guild.Roles.ToList();

                const int usersPerPage = 8;

                var lastPage = 1 + rolesList.Count / (usersPerPage + 1);
                if (page > lastPage)
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context,
                        $"Boole. Last Page is {lastPage}");
                    return;
                }

                var orderedRolesList = rolesList.OrderByDescending(acc => acc.Members.Count()).ToList();

                var embB = new EmbedBuilder()
                    .WithTitle("Top By Roles:")
                    .WithFooter(
                        $"Page {page}/{lastPage} ● Say \"topRoles 2\" to see second page (you can edit previous message)");
                page--;

                for (var i = 1; i <= usersPerPage && i + usersPerPage * page <= orderedRolesList.Count; i++)
                {
                    var num = i + usersPerPage * page - 1;
                    embB.AddField($"#{i + usersPerPage * page} {orderedRolesList[num].Name}",
                        $"**Members:** {orderedRolesList[num].Members.Count()}\n" +
                        $"**Color:** {orderedRolesList[num].Color}\n" +
                        $"**Created:** {orderedRolesList[num].CreatedAt.DateTime}\n" +
                        $"**Is Mentionable:** {orderedRolesList[num].IsMentionable}\n" +
                        $"**Position:** {orderedRolesList[num].Position}\n" +
                        $"**ID:** {orderedRolesList[num].Id}\n\n**_____**", true);
                }

                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embB);
            }
            catch
            {
             //   await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **top [page_number]**(Top By Activity)\nAlias: topl");
            }
        }


        [Command("topChannels")]
        [Alias("topChan", "top Channels", "topChannel", "top Channel")]
        [Description("Top by Roles (Statistics fore Channels in the Guild)")]
        public async Task TopByChannels(int page = 1)
        {
            try
            {
                if (page < 1)
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context,
                        "Boole! Try different page <_<");
                    return;
                }

                var guildAccount = ServerAccounts.GetServerAccount(Context.Guild);
                var allTextChannelsList = Context.Guild.TextChannels.ToList();

                var knownTextChannelsList = guildAccount.MessagesReceivedStatisctic.ToList();

                const int usersPerPage = 8;

                var lastPage = 1 + allTextChannelsList.Count / (usersPerPage + 1);
                if (page > lastPage)
                {
                    await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context,
                        $"Boole. Last Page is {lastPage}");
                    return;
                }

                guildAccount.MessagesReceivedStatisctic = new ConcurrentDictionary<string, ulong>();
                foreach (var t1 in allTextChannelsList)
                {
                    ulong num = 0;
                    foreach (var t in knownTextChannelsList)
                        if (t1.Id.ToString() == t.Key)
                            num = t.Value;


                    guildAccount.MessagesReceivedStatisctic.AddOrUpdate(t1.Id.ToString(), num, (key, value) => num);
                    ServerAccounts.SaveServerAccounts();
                }

                var orderedKnownChannels = guildAccount.MessagesReceivedStatisctic
                    .OrderByDescending(channels => channels.Value).ToList();

                var embB = new EmbedBuilder()
                    .WithTitle("Top By Activity In Text Channels:")
                    .WithFooter(
                        $"Page {page}/{lastPage} ● Say \"topChan 2\" to see second page (you can edit previous message)")
                    .WithDescription(
                        "If `Messages Count: 0` that means, I can't read the channel, or no one using it.\n");

                page--;

                for (var i = 1; i <= usersPerPage && i + usersPerPage * page <= orderedKnownChannels.Count; i++)
                {
                    var num = i - 1 + usersPerPage * page;
                    SocketTextChannel something = null;
                    foreach (var t in allTextChannelsList)
                        if (orderedKnownChannels[num].Key == t.Id.ToString())
                            something = Context.Guild.GetTextChannel(t.Id);

                    var cat = "error";
                    if (something == null)
                        continue;
                    if (something.Category != null)
                        cat = something.Category.Name;

                    embB.AddField($"#{i + usersPerPage * page} {something.Name}",
                        $"**Messages Count:** {orderedKnownChannels[num].Value}\n" +
                        $"**Average per day:** {orderedKnownChannels[num].Value / 7}\n" +
                        $"**Members:** {something.Users.Count}\n" +
                        $"**Created:** {something.CreatedAt.DateTime}\n" +
                        $"**Category:** {cat}\n" +
                        $"**IsNsfw:** {something.IsNsfw.ToString()}\n" +
                        $"**Position:** {something.Position}\n" +
                        $"**ID:** {something.Id}\n\n**_____**", true);
                }

                await CommandHandelingSendingAndUpdatingMessages.SendingMess(Context, embB);
            }
            catch
            {
             //   Console.WriteLine(e.Message);
            }
        }
    }
}