using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Server;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Custom_Library.DiscordBotsList.Api.Custom;
using OctoBot.Handeling;
using OctoBot.Helper;

namespace OctoBot.Commands
{
    public class DailyPull : ModuleBase<ShardedCommandContextCustom>
    {
        private readonly AuthDiscordBotListApi _dblApi =
            new AuthDiscordBotListApi(423593006436712458, Config.Bot.DbLtoken);

        public enum DailyPullResult
        {
            Success,
            AlreadyRecieved
        }

        public static DailyPullResult GetDailyPull(SocketUser user, ulong guilid)
        {
            var account = UserAccounts.GetAccount(user, guilid);
            var difference = DateTime.UtcNow - account.LastDailyPull;

            if (difference.TotalHours < 20) return DailyPullResult.AlreadyRecieved;

            var date = DateTime.UtcNow.DayOfWeek;
            if (date == DayOfWeek.Friday || date == DayOfWeek.Saturday || date == DayOfWeek.Sunday)
                account.DailyPullPoints += 2;
            else
                account.DailyPullPoints += 1;

            account.LastDailyPull = DateTime.UtcNow;

            UserAccounts.SaveAccounts(guilid);
            return DailyPullResult.Success;
        }


        public async Task<bool> HasVoted(ulong userId)
        {
            var url = "https://discordbots.org/api/bots/423593006436712458/check?userId=" + userId;
            var response = await _dblApi.RestClient.SetAuthorization(Config.Bot.DbLtoken).GetAsync(url);
            return response.Body.Contains('1');
        }



        [Command("serverUPD2", RunMode = RunMode.Async)]
        [RequireOwner]
        public async Task ServerUpd()
        {
            await ReplyAsync("324");
            var serverAccount = ServerAccounts.GetAllServerAccounts();

            foreach (var t in serverAccount)
            {
                var rooms = t.MessagesReceivedStatisctic.ToList();

                foreach (var room in rooms)
                {
                    if (!ulong.TryParse(room.Key, out _))
                    {
                        t.MessagesReceivedStatisctic.TryRemove($"{room.Key}", out _);
                    }
                }
            }
        }


        [Command("pull")]
        public async Task Pull()
        {
            if (Context.Channel is SocketDMChannel)
                return;

            if (Context.Guild.Id != 377879473158356992)
            {
                var userForActrivityChack = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (userForActrivityChack.Lvl < 1)
                {
                    var text = "You have to be lvl 1 or more, to use pull\n" +
                               "You may check your lvl using `stats` command";
                    await CommandHandeling.ReplyAsync(Context, text);
                    return;
                }
            }

            var account = UserAccounts.GetAccount(Context.User, 0);


            if (!await HasVoted(Context.User.Id))
            {
                await CommandHandeling.ReplyAsync(Context,
                    "Boole-Boole. To use this command, you have to vote here: <https://discordbots.org/bot/423593006436712458>\n" +
                    $"Please, wait for 2-5 minutes after the vote. Thank you boole!\n" +
                    $"{new Emoji("<:octo_hi:465374417644552192>")}");
                return;
            }

            var result = GetDailyPull(Context.User, 0);
            var difference = DateTime.UtcNow - account.LastDailyPull;
            var embed = new EmbedBuilder();
            var pointsToGive = 1;
            var date = DateTime.UtcNow.DayOfWeek;
            if (date == DayOfWeek.Friday || date == DayOfWeek.Saturday || date == DayOfWeek.Sunday)
            {
                embed.WithFooter("Weekend! Double points!");
                pointsToGive = 2;
            }

            embed.WithAuthor(Context.User);
            embed.WithColor(Color.Gold);
            switch (result)
            {
                case DailyPullResult.AlreadyRecieved:
                    embed.AddField("Pull Points",
                        $"You **already** have received a point, {Context.User.Username}. You have **{account.DailyPullPoints} points**. Try again in {20 - (int) difference.TotalHours} hours\n");
                    if (account.DailyPullPoints - 1 > OctoPicPull.OctoPicsPull.Length)
                        embed.WithImageUrl(OctoPicPull.OctoPicsPull[27]);
                    else if (account.DailyPullPoints <= 0)
                        embed.WithImageUrl(
                            "https://media.discordapp.net/attachments/436071383836000256/467134338085945344/20180712_210457.jpg?width=1247&height=702");
                    else
                        embed.WithImageUrl(OctoPicPull.OctoPicsPull[account.DailyPullPoints - 1]);
                    await CommandHandeling.ReplyAsync(Context, embed);
                    break;
                case DailyPullResult.Success:
                    if (account.DailyPullPoints == 28)
                    {
                        embed.AddField("Pull Points",
                            $"**You have all {account.DailyPullPoints} points!!** Within a minute, our turtles will send you a key in DM!\n");
                        embed.WithImageUrl(OctoPicPull.OctoPicsPull[account.DailyPullPoints - 1]);
                        await CommandHandeling.ReplyAsync(Context, embed);
                    }
                    else if (account.DailyPullPoints < 28)
                    {
                        embed.AddField("Pull Points",
                            $"**You got {pointsToGive} point!** You have now **{account.DailyPullPoints} points**. Try again in 1 day, to get another point!\n");
                        embed.WithImageUrl(OctoPicPull.OctoPicsPull[account.DailyPullPoints - 1]);
                        await CommandHandeling.ReplyAsync(Context, embed);
                    }
                    else // BACK UP
                    {
                        embed.AddField("Pull Points",
                            $"**You got {pointsToGive} point!** You have now {account.DailyPullPoints} points. Try again in 1 day, to get another point!\n");
                        embed.WithImageUrl(OctoPicPull.OctoPicsPull[27]);
                        await CommandHandeling.ReplyAsync(Context, embed);
                    }
                    break;
            }
        }

        [Command("AddKey")]
        public async Task JsonTask([Remainder] string mess)
        {
            try
            {
                var mylorikGlobal = Global.Client.GetUser(181514288278536193);
                var mylorik = UserAccounts.GetAccount(mylorikGlobal, 0);

                var gameAndKey = mess.Split(new[] {"&&"}, StringSplitOptions.RemoveEmptyEntries);
                if (gameAndKey.Length < 2 || gameAndKey.Length >= 3)
                    return;

                mylorik.KeyPullName += gameAndKey[0] + "|";
                mylorik.KeyPullKey += gameAndKey[1] + "|";
                UserAccounts.SaveAccounts(0);

                await CommandHandeling.ReplyAsync(Context, "Boooole~ We got the key!");


                ConsoleLogger.Log($" [ADD KEY] ({Context.User.Username}) - {mess}", ConsoleColor.DarkBlue);
            }
            catch
            {
                //  await ReplyAsync(
                //      "boooo... An error just appear >_< \nTry to use this command properly: **AddKey Gamename && key (platform)**\n");
            }
        }

        [Command("KeyDel")]
        [RequireOwner]
        public async Task JsonDel(int index)
        {
            var mylorikGlobal = Global.Client.GetUser(181514288278536193);
            var mylorik = UserAccounts.GetAccount(mylorikGlobal, 0);
            var keyName =
                mylorik.KeyPullName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            var keykey =
                mylorik.KeyPullKey.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


            mylorik.KeyPullName = null;
            mylorik.KeyPullKey = null;
            for (var i = 0; i < keyName.Length; i++)
                if (i != index)
                {
                    mylorik.KeyPullName += $"{keyName[i]}|";
                    mylorik.KeyPullKey += $"{keykey[i]}|";
                }


            await CommandHandeling.ReplyAsync(Context,
                $"ключ **{keyName[index]} {keykey[index]}** был удалён");
            UserAccounts.SaveAccounts(0);
        }

        [Command("Ключи")]
        [Alias("Keys")]
        public async Task AllKeys()
        {
            try
            {
                var account = UserAccounts.GetAccount(Context.User, 0);

                var keyName = account.KeyPullName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                var keyKey = account.KeyPullKey.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                var keys = "";
                var keysExtra = "";
                var keysExtra2 = "";

                for (var i = 0; i < keyName.Length; i++)
                    if (keys.Length <= 800)
                        keys += $"index: {i} | {keyName[i]} {keyKey[i]}\n";
                    else if (keys.Length <= 1600)
                        keysExtra += $"index: {i} | {keyName[i]} {keyKey[i]} \n";
                    else
                        keysExtra2 += $"index: {i} | {keyName[i]} {keyKey[i]} \n";

                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.AddField("Ключи:", $"{keys}\n**KeyDel [index]** Чтобы удалить ");
                embed.WithFooter("lil octo notebook");
                if (keysExtra.Length > 10)
                    embed.AddField("Ключи(cont):", $"{keysExtra}\n**KeyDel [index]** Чтобы удалить ");
                if (keysExtra2.Length > 10)
                    embed.AddField("Ключи(cont):", $"{keysExtra2}\n**KeyDel [index]** Чтобы удалить ");


                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch
            {
                //   await ReplyAsync(
                //       "boo... An error just appear >_< \nTry to use this command properly: **Keys** (show all **YOUR** keys)\n");
            }
        }


        [Command("avakey")]
        [Alias("AvaKeys")]
        public async Task AvailableAllKeys()
        {
            try
            {
                var mylorikGlobal = Global.Client.GetUser(181514288278536193);
                var account = UserAccounts.GetAccount(mylorikGlobal, 0);

                var keyName = account.KeyPullName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                var keys = "";
                var keysExtra = "";
                var keysExtra2 = "";

                for (var i = 0; i < keyName.Length; i++)
                    if (keys.Length <= 800)
                        keys += $"{i + 1}) {keyName[i]}\n";
                    else if (keys.Length <= 1600)
                        keysExtra += $"{i + 1}) {keyName[i]}\n";
                    else
                        keysExtra2 += $"{i + 1}) {keyName[i]}\n";

                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.AddField("Keys:", $"{keys}\n");
                embed.WithFooter("lil octo notebook");
                if (keysExtra.Length > 10)
                    embed.AddField("Keys(cont):", $"{keysExtra}\n");
                if (keysExtra2.Length > 10)
                    embed.AddField("Keys(cont):", $"{keysExtra2} ");


                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch
            {
                //   await ReplyAsync(
                //       "boo... An error just appear >_< \nTry to use this command properly: **Keys** (show all **YOUR** keys)\n");
            }
        }


        [Command("cKey")]
        public async Task ChooseKeyFromPull(int choice)
        {
            try
            {
                if (choice < 0 || choice > 3)
                {
                    await ReplyAsync("Booo!");
                    return;
                }

                var account = UserAccounts.GetAccount(Context.User, 0);
                if (choice == 0)
                {
                    await CommandHandeling.ReplyAsync(Context, "Boole~");
                    account.PullToChoose = null;
                    UserAccounts.SaveAccounts(0);
                    return;
                }


                var keysToChooseList =
                    account.PullToChoose.Split(new[] {"%%"}, StringSplitOptions.RemoveEmptyEntries);


                int index;

                if (choice == 1)
                {
                    var something = keysToChooseList[0].Split(new[] {"%%"}, StringSplitOptions.RemoveEmptyEntries);
                    index = Convert.ToInt32(something[0]);
                }
                else if (choice == 2)
                {
                    var something = keysToChooseList[1].Split(new[] {"%%"}, StringSplitOptions.RemoveEmptyEntries);
                    index = Convert.ToInt32(something[0]);
                }
                else
                {
                    var something = keysToChooseList[2].Split(new[] {"%%"}, StringSplitOptions.RemoveEmptyEntries);
                    index = Convert.ToInt32(something[0]);
                }

                var mylorikGlobal = Global.Client.GetUser(181514288278536193);
                var mylorik = UserAccounts.GetAccount(mylorikGlobal, 0);
                var keyName =
                    mylorik.KeyPullName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                var keykey =
                    mylorik.KeyPullKey.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.WithTitle("OctoNotification");
                embed.WithDescription($"А вот и ключ!\n\n**{keyName[index]} : {keykey[index]}**\n\nБуль!");
                ConsoleLogger.Log($"DM [KEY] ({Context.User.Username}) - {keyName[index]} : {keykey[index]}",
                    ConsoleColor.DarkBlue);

                await CommandHandeling.ReplyAsync(Context, embed);


                mylorik.KeyPullName = null;
                mylorik.KeyPullKey = null;
                for (var i = 0; i < keyName.Length; i++)
                    if (i != index)
                    {
                        mylorik.KeyPullName += $"{keyName[i]}|";
                        mylorik.KeyPullKey += $"{keykey[i]}|";
                    }

                account.PullToChoose = null;
                UserAccounts.SaveAccounts(0);
            }
            catch
            {
                //    Console.WriteLine(e);
                //    await CommandHandeling.ReplyAsync(Context,
                //        "You do not have any keys, or there is an error. Please contact mylorik#2828 for more info");
            }
        }

        [Command("pullp")]
        [RequireOwner]
        public async Task GivePullPoints(SocketGuildUser user, int pullPoints)
        {
            var account = UserAccounts.GetAccount(user, 0);
            account.DailyPullPoints += pullPoints;
            UserAccounts.SaveAccounts(0);
            var embed = new EmbedBuilder();
            embed.WithColor(Color.DarkMagenta);
            embed.AddField("буууль~",
                $"Мы добавили {pullPoints} пулл Поинтов {user.Mention}. Теперь у него {account.DailyPullPoints} поинтов, буль!");
            await CommandHandeling.ReplyAsync(Context, embed);
        }
    }
}