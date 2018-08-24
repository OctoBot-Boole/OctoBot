using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Handeling;
using OctoBot.Helper;

namespace OctoBot.Commands
{
    public class Fun : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("pick")]
        public async Task Pick([Remainder] string message)
        {
            try
            {
                var option = message.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

                var rand = new Random();
                var selection = option[rand.Next(0, option.Length)];

                var embed = new EmbedBuilder();
                embed.WithTitle("I chose instead " + Context.User.Username);
                embed.WithDescription(selection);

                embed.WithColor(new Color(255, 0, 94));
                embed.WithThumbnailUrl("https://i.imgur.com/I3o0bm4.jpg");


                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch
            {
             //   await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **pick option1|option2|option3**(output random option (can be as many as you want))\n");
            }
        }

        [Command("ping")]
        [Alias("пинг")]
        public async Task DefaultPing()
        {
            await CommandHandeling.ReplyAsync(Context, $"{Context.User.Mention} pong!");
        }


        [Command("DM")]
        public async Task DmMess()
        {
            try
            {
                var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync("Boole.");
            }
            catch
            {
             //   await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **DM**(sends you a DM)\n");
            }
        }

        [Command("guess", RunMode = RunMode.Async)]
        [Alias("Рулетка", "угадайка")]
        public async Task GuessGame(ulong enter)
        {
            try
            {
                var amount = (int) enter;

                var userAccount = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                var octoAcccount = UserAccounts.GetAccount(Context.Guild.CurrentUser, Context.Guild.Id);

                if (amount > userAccount.Points || amount <= 0)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "You do not have enough OktoPoints! Or you just entered something wrong.");

                    return;
                }


                var randSlot = new Random();
                var slots = randSlot.Next(72);


                await CommandHandeling.ReplyAsync(Context,
                    $"Number of slots **{slots}**. What is your choice?");

                var response = await AwaitForUserMessage.AwaitMessage(Context.User.Id, Context.Channel.Id, 10000);

                var result = int.TryParse(response.Content, out _);
                if (result)
                {
                    var choise = Convert.ToInt32(response.Content);
                    var bank = Math.Abs(amount * slots / 5);


                    var rand = new Random();
                    var random = rand.Next(slots);

                    if (choise == random)
                    {
                        userAccount.Points += bank;
                        UserAccounts.SaveAccounts(Context.Guild.Id);

                        await CommandHandeling.ReplyAsync(Context,
                            $"You won **{bank}** OctoPoints!\nNow you have **{userAccount.Points}** OctoPoints!");

                        userAccount.Points += bank;
                        UserAccounts.SaveAccounts(Context.Guild.Id);
                    }
                    else
                    {
                        await CommandHandeling.ReplyAsync(Context,
                            $"booole. Yuor **{amount}** OctoPoints stayed with us. Btw, number was **{random}**");


                        userAccount.Points -= amount;
                        octoAcccount.Points += amount;
                        UserAccounts.SaveAccounts(Context.Guild.Id);
                    }
                }
                else


                {
                    await CommandHandeling.ReplyAsync(Context,
                        $"The choice should be between 0 and {slots}, answer only with a number.");
                }
            }
            catch
            {
            //    await ReplyAsync(
            //        "boo... An error just appear >_< \nTry to use this command properly: **guess [rate_num]**(like cassino, ty it!)\n" +
            //        "Alias: Рулетка, угадайка");
            }
        }


        [Command("myPrefix")]
        public async Task SetMyPrefix([Remainder] string prefix = null)
        {
            var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);

            if (prefix == null)
            {
                await CommandHandeling.ReplyAsync(Context,
                    $"Your prefix: **{account.MyPrefix}**");
                return;
            }

            if (prefix.Length < 100)
            {
                account.MyPrefix = prefix;
                if (prefix.Contains("everyone") || prefix.Contains("here"))
                {
                    await CommandHandeling.ReplyAsync(Context,
                        $"Boooooo! no `here` or `everyone` prefix!");
                    return;
                }

                UserAccounts.SaveAccounts(Context.Guild.Id);
                await CommandHandeling.ReplyAsync(Context,
                    $"Booole~, your own prefix is now **{prefix}**");
            }
            else
            {
                await CommandHandeling.ReplyAsync(Context,
                    "Booooo! Prefix have to be less than 100 characters");
            }
        }
    }
}