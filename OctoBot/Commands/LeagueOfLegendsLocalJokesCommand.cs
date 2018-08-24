using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Handeling;

namespace OctoBot.Commands
{
    public class LoL : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("я проиграл")]
        [Alias("я проиграла", "я проиграл.", "я проиграла.", "я проиграл...", "я проиграл..", "я проиграла...")]
        public async Task Lost()
        {
            var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            account.Lost += 1;
            UserAccounts.SaveAccounts(Context.Guild.Id);

            if (account.Lost == 1)
            {
                var embed = new EmbedBuilder();
                embed.WithColor(Color.Green);
                embed.WithAuthor(Context.User);
                embed.WithTimestamp(Context.Message.Timestamp);
                embed.WithTitle("Оппа, первопроходец!");
                embed.WithDescription($"Ты впервые проиграл!");


                await CommandHandeling.ReplyAsync(Context, embed);
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithColor(Color.DarkOrange);
                embed.WithAuthor(Context.User);
                embed.WithTimestamp(Context.Message.Timestamp);
                embed.WithTitle("Опять?");
                if (account.Lost == 2)
                    embed.WithDescription($"Это уже во {account.Lost}й раз...");
                else
                    embed.WithDescription($"Это уже в {account.Lost}й раз...");

                await CommandHandeling.ReplyAsync(Context, embed);
            }
        }


        public static string[] ThamKench =
        {
            "https://i.imgur.com/hPekXqU.jpg",
            "https://i.imgur.com/jUtSMaI.png",
            "https://cdna.artstation.com/p/assets/images/images/008/269/292/large/joao-vagner-joao-vagner-tham-kench-esboco-final-2.jpg?1511640303",
            "https://media.esportsedition.com/wp-content/uploads/2015/07/Tahm-Kench-for-the-Worst-1068x601.jpg",
            "https://static.lolwallpapers.net/2016/01/Tahm-Kench-Jinx-Fan-Art-By-Ben-Lo-1.jpg"
        };


        [Command("кто там?")]
        [Alias("кто там", "кто ... там", "кто .. там", "кто .... там", "кто там?!", "Таам", "кто там...", "кто там..",
            "кто там....", "кто ... там?", "кто ... там?!", "кто там.", "кто там.", "кто там,")]
        public async Task WhoAreThere()
        {
            var rand = new Random();
            var randomIndex = rand.Next(ThamKench.Length);
            var thamKenchToPost = ThamKench[randomIndex];

            await Context.Channel.SendMessageAsync("ТАМ....");
            await Task.Delay(3000);
            await Context.Channel.SendMessageAsync("КЕНЧ!");
            var embed = new EmbedBuilder();
            embed.WithImageUrl($"{thamKenchToPost}");

            await CommandHandeling.ReplyAsync(Context, embed);
        }

        [Command("А там")]
        [Alias("А там", "А там...", "А там..", "А там....")]
        public async Task WhoAreThereA()
        {
            var rand = new Random();
            var randomIndex = rand.Next(ThamKench.Length);
            var thamKenchToPost = ThamKench[randomIndex];


            await Context.Channel.SendMessageAsync("КЕНЧ!");
            var embed = new EmbedBuilder();
            embed.WithImageUrl($"{thamKenchToPost}");

            await CommandHandeling.ReplyAsync(Context, embed);
        }


        [Command("Заповедь")]
        [Alias("Заповеди", "Заповеди Бога Лола", "10 заповедей бога лола")]
        public async Task Commandment()
        {
            var embed = new EmbedBuilder();
            embed.WithImageUrl(
                "https://media.discordapp.net/attachments/238416197337481217/436790640861511691/--2oOzEe8RI.png");
            embed.WithTitle("10 заповедей бога лола");

            await CommandHandeling.ReplyAsync(Context, embed);
        }
    }
}