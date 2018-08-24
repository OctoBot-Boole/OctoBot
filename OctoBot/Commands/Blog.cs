using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Handeling;
using static OctoBot.Configs.Global;

namespace OctoBot.Commands
{
    public class Blog : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("подписчики")]
        [Alias("MySubc", "subscribers", "Subc")]
        public async Task CheckMySu()
        {
            try
            {
                var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (account.SubedToYou == null)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        $"На тебя никто не подписан, буль!");


                    return;
                }

                var accountSubs = account.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                var mess = "";

                for (var i = 0; i < accountSubs.Length; i++)
                {
                    var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                    mess += $"{i + 1}. {globalAccount.Username}\n";
                }

                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle("Твои подписчики:");
                embed.WithDescription($"{mess}");

                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch
            {
            //    await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **Subc**(showing all of yuor followers)\n" +
            //        "Alias: MySubc, subscribers, подписчики");
            }
        }


        [Command("подписки")]
        [Alias("MySubs", "Subscriptions", "subs")]
        public async Task CheckMySubscriptions()
        {
            try
            {
                var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (account.SubToPeople == null)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        $"Ты и так ни на кого не подписан, буль!");


                    return;
                }

                var accountSubs = account.SubToPeople.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


                var mess = "";
                for (var i = 0; i < accountSubs.Length; i++)
                {
                    var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                    mess += $"{i + 1}. {globalAccount.Username}\n";
                }

                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle("Твои подписки:");
                embed.WithDescription($"{mess}");

                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch
            {
           //     await ReplyAsync(
           //         "boo... An error just appear >_< \nTry to use this command properly: **subs**(showing all people you follow)\n" +
           //         "Alias: MySubs, Subscriptions, подписки");
            }
        }


        [Command("Sub")]
        [Alias("follow")]
        public async Task Sub(SocketUser user)
        {
            try
            {
                if (user.Id == 423593006436712458)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Нельзя подписываться на осьминожку!");


                    return;
                }

                if (user.IsBot)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Нельзя подписываться на бота.");


                    return;
                }

                var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                var el = UserAccounts.GetAccount(user, Context.Guild.Id);


                if (account.SubToPeople != null)
                {
                    var accountSubs = account.SubToPeople.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                    if (accountSubs.Any(t => t == user.Id.ToString()))
                    {
                        await CommandHandeling.ReplyAsync(Context,
                            $"Ты уже подписан на {user.Username}.");


                        return;
                    }
                }


                account.SubToPeople += user.Id + "|";
                el.SubedToYou += Context.User.Id + "|";
                UserAccounts.SaveAccounts(Context.Guild.Id);


                await CommandHandeling.ReplyAsync(Context,
                    $"Ты подписался на {user.Username}!\nЕсли хочешь отписаться введи команду ***unsub [user]**");
            }
            catch
            {
            //    await ReplyAsync(
            //        "boo... An error just appear >_< \nTry to use this command properly: **Sub [user_ping (or user ID)**(follow someone's blog)\n" +
            //        "Alias: follow");
            }
        }


        [Command("unsub")]
        [Alias("unfollow")]
        public async Task Unsub(SocketUser user)
        {
            try
            {
                var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (account.SubToPeople == null)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Ты и так ни на кого не подписан, буль!");


                    return;
                }

                var el = UserAccounts.GetAccount(user, Context.Guild.Id);
                var accountSubs = account.SubToPeople.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


                var check = 0;
                foreach (var t in accountSubs)
                {
                    if (Convert.ToUInt64(t) != user.Id) check++;

                    if (check >= accountSubs.Length && Convert.ToUInt64(t) != user.Id)
                    {
                        await CommandHandeling.ReplyAsync(Context,
                            $"Ты **не был** подписан на {user.Username}.");


                        return;
                    }
                }

                account.SubToPeople = null;
                foreach (var t in accountSubs)
                    if (Convert.ToUInt64(t) != user.Id)
                        account.SubToPeople += $"{t}|";

                UserAccounts.SaveAccounts(Context.Guild.Id);

                var elSubs = el.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                el.SubedToYou = null;
                foreach (var t in elSubs)
                    if (Convert.ToUInt64(t) != Context.User.Id)
                        el.SubedToYou += $"{t}|";

                UserAccounts.SaveAccounts(Context.Guild.Id);


                await CommandHandeling.ReplyAsync(Context,
                    $"Ты был успешно ансабнут от {user.Username}");
            }
            catch
            {
            //    await ReplyAsync(
            //        "boo... An error just appear >_< \nTry to use this command properly: **unSub [user_ping (or user ID)]**(unfollow someone's blog)\n" +
            //        "Alias: unfollow");
            }
        }


        [Command("Blog")]
        [Alias("блог", "пост", "пинг", "BlogPost", "Blog Post")]
        public async Task BlogPost([Remainder] string mess)
        {
            try
            {
                var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (account.SubedToYou == null)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "На тебя никто не подписан ещё буль... Попробуй чем то завлечь людей сначала!");


                    return;
                }

                var accountSubs = account.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                var messList = new List<IUserMessage>();


                for (var i = 0; i < accountSubs.Length; i++)
                    try
                    {
                        var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                        var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();


                        var embed = new EmbedBuilder();
                        embed.WithFooter("lil octo notebook");
                        embed.WithTitle("OctoNotification");
                        embed.WithDescription($"**{Context.User.Mention}:** {mess}");
                        // embed.WithUrl("https://puu.sh/AqC2d/715e9eb16e.mp3");
                        embed.AddField("Оценить творчество",
                            "Если тебе нравится блоги этой/этого няши, почему бы не оценить?\n" +
                            "Можешь оценить от 1 до zazz, оценка анонимная, но общую оценку можно посомтрет чрезе команду **topr**\n" +
                            "https://puu.sh/AqC2d/715e9eb16e.mp3");
                        var message = await dmChannel.SendMessageAsync("", false, embed.Build());
                        //await dmChannel.SendFileAsync("https://puu.sh/AqC2d/715e9eb16e.mp3");


                        messList.Add(message);

                        try
                        {
                            var voteMessToTrack = new BlogVotes(Context.User, message, globalAccount, 1);
                            BlogVotesMessIdList.Add(voteMessToTrack);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    catch
                    {
                        account.SubedToYou = null;
                        for (var j = 0; j < accountSubs.Length; j++)
                            if (j != i)
                                account.SubedToYou += $"{accountSubs[j]}|";

                        UserAccounts.SaveAccounts(Context.Guild.Id);

                        var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                        var errorGuyAcc = UserAccounts.GetAccount(globalAccount, Context.Guild.Id);
                        var errorGuy =
                            errorGuyAcc.SubToPeople.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                        errorGuyAcc.SubToPeople = null;
                        foreach (var t in errorGuy)
                            if (Convert.ToUInt64(t) != Context.User.Id)
                                errorGuyAcc.SubToPeople += $"{t}|";

                        UserAccounts.SaveAccounts(Context.Guild.Id);
                        var dmCommander = await Context.User.GetOrCreateDMChannelAsync();

                        await dmCommander.SendMessageAsync(
                            $"Возникла проблема с {globalAccount.Username}, мы его удалили из списка.");
                    }

                await AddReactionForBlogMessages(messList, Context.Channel);
                await ReplyAsync($"{Context.User.Mention}, твой блог был отправлен!");
            }
            catch
            {
              //  await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **Blog [any text you want]**(send a blog to all of your followers)\n");
            }
        }


        [Command("IBlog")]
        public async Task BlogPostWithUrl(string url, [Remainder] string mess)
        {
            try
            {
                var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (account.SubedToYou == null)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "На тебя никто не подписан ещё буль... Попробуй чем то завлечь людей сначала!");


                    return;
                }

                if (url.Length < 5)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "буууу! Ссылка не в правильном формате!");


                    return;
                }

                var httpsCheck = $"{url[0]}{url[1]}{url[2]}{url[3]}{url[4]}";
                if (httpsCheck != "https")
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "буууу! Ссылка не в правильном формате!");


                    return;
                }

                var accountSubs = account.SubedToYou.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                var messList = new List<IUserMessage>();

                for (var i = 0; i < accountSubs.Length; i++)
                    try
                    {
                        var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                        var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();

                        var embed = new EmbedBuilder();
                        embed.WithFooter("lil octo notebook");
                        embed.WithTitle("OctoNotification");
                        embed.WithImageUrl($"{url}");
                        embed.WithDescription($"**{Context.User.Mention}:** {mess}\n");
                        //https://puu.sh/AqCnq/8ac50e7232.mp3
                        //https://puu.sh/AqCse/1e0dbed1d5.mp3
                        //embed.WithUrl("https://puu.sh/AqC2d/715e9eb16e.mp3");
                        embed.AddField("Оценить творчество",
                            "Если тебе нравится блоги этой/этого няши, почему бы не оценить?\n" +
                            "Можешь оценить от 1 до zazz, оценка анонимная, но общую оценку можно посомтрет чрезе команду **topr**\n" +
                            "https://puu.sh/AqC2d/715e9eb16e.mp3");

                        var message = await dmChannel.SendMessageAsync("", false, embed.Build());
                        //  await dmChannel.SendFileAsync("https://puu.sh/AqC2d/715e9eb16e.mp3");
                        messList.Add(message);
                        try
                        {
                            var voteMessToTrack = new BlogVotes(Context.User, message, Context.User, 1);
                            BlogVotesMessIdList.Add(voteMessToTrack);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    catch
                    {
                        account.SubedToYou = null;
                        for (var j = 0; j < accountSubs.Length; j++)
                            if (j != i)
                                account.SubedToYou += $"{accountSubs[j]}|";
                        UserAccounts.SaveAccounts(Context.Guild.Id);

                        var globalAccount = Client.GetUser(Convert.ToUInt64(accountSubs[i]));
                        var errorGuyAcc = UserAccounts.GetAccount(globalAccount, Context.Guild.Id);
                        var errorGuy =
                            errorGuyAcc.SubToPeople.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                        errorGuyAcc.SubToPeople = null;
                        foreach (var t in errorGuy)
                            if (Convert.ToUInt64(t) != Context.User.Id)
                                errorGuyAcc.SubToPeople += $"{t}|";
                        UserAccounts.SaveAccounts(Context.Guild.Id);
                        var dmCommander = await Context.User.GetOrCreateDMChannelAsync();
                        await dmCommander.SendMessageAsync(
                            $"Возникла проблема с {globalAccount.Username}, мы его удалили из списка.");
                    }

                await AddReactionForBlogMessages(messList, Context.Channel);
                await ReplyAsync($"{Context.User.Mention}, твой блог был отправлен!");
            }
            catch
            {
              //  await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **IBlog [HTTPS_url] [any text you want]**(send a blog **WITH PICTURE** to all of your followers)\n");
            }
        }

        public static async Task AddReactionForBlogMessages(List<IUserMessage> messageList,
            ISocketMessageChannel contextChannel)
        {
            try
            {
                var zaaz = Emote.Parse("<:zazz:448323858492162049>");

                Parallel.For(0, messageList.Count, async i =>
                {
                    await messageList[i].AddReactionAsync(new Emoji("1⃣"));
                    await messageList[i].AddReactionAsync(new Emoji("2⃣"));
                    await messageList[i].AddReactionAsync(new Emoji("3⃣"));
                    await messageList[i].AddReactionAsync(new Emoji("4⃣"));
                    await messageList[i].AddReactionAsync(zaaz);
                });

                await Task.CompletedTask;
            }
            catch
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("AddReactionForBlogMessages CATCH");
                Console.ResetColor();
              //  await contextChannel.SendMessageAsync(
             //       $"Произошла какая-то ошибка, попробуй отправить снова");
            }
        }
    }
}