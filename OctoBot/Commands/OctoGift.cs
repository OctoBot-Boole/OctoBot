using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Handeling;

namespace OctoBot.Commands
{
    public class OctoGift : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("GiftCooki")]
        [Alias("Gift Cooki", "подаритьКуки", "Подарить Куки")]
        public async Task GiftCooki(IGuildUser user)
        {
            try
            {
                var contextUser = UserAccounts.GetAccount(Context.User, Context.Guild.Id);

                var account = UserAccounts.GetAccount((SocketUser) user, Context.Guild.Id);
                if (account.Cooki >= 1)
                {
                    await Context.Channel.SendMessageAsync(
                        $"{user.Mention} Already have Cooki, you should choose another Octopus or Turtle!");
                    return;
                }

                if (contextUser.Points >= 1488)
                {
                    contextUser.Points -= 1488;
                    UserAccounts.SaveAccounts(Context.Guild.Id);

                    account.Cooki += 1;
                    account.Octopuses += "Cooki|";
                    UserAccounts.SaveAccounts(Context.Guild.Id);

                    var embed = new EmbedBuilder();
                    embed.WithColor(244, 66, 107);
                    embed.WithTitle($"You gave {user} Cooki!!");
                    embed.WithFooter("lil octo notebook");
                    embed.AddField("Fees was applied (1488 OctoPoints)", $"{contextUser.Points} Octo Points left");
                    embed.WithImageUrl("https://i.imgur.com/dCJwloV.jpg");


                    await CommandHandeling.ReplyAsync(Context, embed);
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"You do not have enough OktoPoints to give **Cooki**!");
                }
            }
            catch
            {
             //   await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **GiftCooki**\n" +
             //                    "Alias: ПодаритьКуки");
            }
        }

        [Command("GiftPinki")]
        [Alias("Gift Pinki", "Подарить Пинки", "ПодаритьПинки")]
        public async Task GiftPinki(IGuildUser user)
        {
            try
            {
                var contextUser = UserAccounts.GetAccount(Context.User, Context.Guild.Id);

                var account = UserAccounts.GetAccount((SocketUser) user, Context.Guild.Id);
                if (account.Pinki >= 1)
                {
                    await Context.Channel.SendMessageAsync(
                        $"{user.Mention} Already have Pinki, you should choose another Octopus or Turtle!");
                    return;
                }

                if (contextUser.Points >= 1488)
                {
                    contextUser.Points -= 1488;
                    UserAccounts.SaveAccounts(Context.Guild.Id);

                    account.Pinki += 1;
                    account.Octopuses += "Pinki|";
                    UserAccounts.SaveAccounts(Context.Guild.Id);

                    var embed = new EmbedBuilder();
                    embed.WithColor(244, 66, 107);
                    embed.WithTitle($"You gave {user} Pinki!!");
                    embed.WithFooter("lil octo notebook");
                    embed.AddField("Fees was applied (1488 OctoPoints)", $"{contextUser.Points} Octo Points left");
                    embed.WithImageUrl("https://i.imgur.com/xxE7EeX.jpg");


                    await CommandHandeling.ReplyAsync(Context, embed);
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"You do not have enough OktoPoints to give **Pinki**!");
                }
            }
            catch
            {
            //    await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **GiftPinki**\n" +
            //                     "Alias: ПодаритьПинки");
            }
        }


        [Command("GiftRainbow")]
        [Alias("Gift Rainbow", "Подарить рудужного", "Подарить радужный", "ПодаритьРадужный", "ПодаритьРадужного")]
        public async Task GiftRainbow(IGuildUser user)
        {
            try
            {
                var contextUser = UserAccounts.GetAccount(Context.User, Context.Guild.Id);

                var account = UserAccounts.GetAccount((SocketUser) user, Context.Guild.Id);
                if (account.Raqinbow >= 1)
                {
                    await Context.Channel.SendMessageAsync(
                        $"{user.Mention} Already have Rainbow, you should choose another Octopus or Turtle!");
                    return;
                }

                if (contextUser.Points >= 1488)
                {
                    contextUser.Points -= 1488;
                    UserAccounts.SaveAccounts(Context.Guild.Id);

                    account.Raqinbow += 1;
                    account.Octopuses += "Rainbow|";
                    UserAccounts.SaveAccounts(Context.Guild.Id);

                    var embed = new EmbedBuilder();
                    embed.WithColor(244, 66, 107);
                    embed.WithFooter("lil octo notebook");
                    embed.WithTitle($"You gave {user} Rainbow!!");
                    embed.AddField("Fees was applied (1488 OctoPoints)", $"{contextUser.Points} Octo Points left");
                    embed.WithImageUrl("https://i.imgur.com/Ufky6UB.jpg");


                    await CommandHandeling.ReplyAsync(Context, embed);
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"You do not have enough OktoPoints to give **Rainbow**!");
                }
            }
            catch
            {
             //   await ReplyAsync(
              //      "boo... An error just appear >_< \nTry to use this command properly: **GiftRainbow**\n" +
             //       "Alias: ПодаритьРадужного");
            }
        }

        [Command("AllOcto")]
        [Alias("All Octo", "ВсеОкто", "Все Окто")]
        public async Task AllOcto()
        {
            try
            {
                var embed = new EmbedBuilder();
                embed.WithColor(Color.Blue);
                embed.WithFooter("lil octo notebook");
                embed.WithTitle("Available Octopus:");
                embed.AddField("**Cooki!**", "`GiftCooki [user]` cost: **1488** ОктоПоинтов");
                embed.AddField("**Pinki~**", "`GiftPinki [user]` cost: **1488** ОктоПоинтов");
                embed.AddField("**Rainbow** :gay_pride_flag:", "`GiftRainbow [user]` cost: **1488** Octo Points");

                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch
            {
             //   await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **AllOcto**\n" +
             //                    "Alias: ВсеОкто");
            }
        }
    }
}