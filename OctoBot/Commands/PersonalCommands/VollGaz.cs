using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Custom_Library;
using OctoBot.Handeling;

namespace OctoBot.Commands.PersonalCommands
{
    public class VollGaz : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("Miserum")]
        public async Task Miserum()
        {
            if (Context.User.Id == 129658526460149760)
            {
                var meme = new[]
                {
                    "https://i.imgur.com/qWb1Q0x.jpg",
                    "https://i.imgur.com/H8umFpy.png",
                    "https://i.imgur.com/kBwCId7.jpg",
                    "https://i.imgur.com/OXqvzAo.jpg",
                    "https://i.imgur.com/snUQeuU.jpg",
                    "https://i.imgur.com/mzpy9wJ.jpg",
                    "https://i.imgur.com/PrEpuqH.jpg",
                    "https://i.imgur.com/ZGmIg8c.jpg"
                };
                var randMeme = new Random();
                var randomIndex = randMeme.Next(meme.Length);
                var memeToPost = meme[randomIndex];


                var color1 = new Random();
                var color2 = new Random();
                var color3 = new Random();
                var color1Index = color1.Next(256);
                var color2Index = color2.Next(256);
                var color3Index = color3.Next(256);


                var embed = new EmbedBuilder();
                embed.WithColor(color1Index, color2Index, color3Index);
                embed.WithAuthor("MISERUM!");
                embed.WithImageUrl("" + memeToPost);

                await CommandHandeling.ReplyAsync(Context, embed);
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");

                await CommandHandeling.ReplyAsync(Context, embed);
            }
        }

        [Command("Approves")]
        public async Task Approves()
        {
            if (Context.User.Id == 129658526460149760)
            {
                const string url = "https://i.imgur.com/wiEppGx.jpg";

                var color1 = new Random();
                var color2 = new Random();
                var color3 = new Random();
                var color1Index = color1.Next(256);
                var color2Index = color2.Next(256);
                var color3Index = color3.Next(256);


                var embed = new EmbedBuilder();
                embed.WithColor(color1Index, color2Index, color3Index);
                embed.WithAuthor("Фистурион Одобряет");
                embed.WithImageUrl("" + url);

                await CommandHandeling.ReplyAsync(Context, embed);
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");

                await CommandHandeling.ReplyAsync(Context, embed);
            }
        }

        [Command("Perfectus")]
        public async Task Perfectus()
        {
            if (Context.User.Id == 129658526460149760)
            {
                const string url = "https://i.imgur.com/mKxhNAY.jpg";
                var color1 = new Random();
                var color2 = new Random();
                var color3 = new Random();
                var color1Index = color1.Next(256);
                var color2Index = color2.Next(256);
                var color3Index = color3.Next(256);

                var embed = new EmbedBuilder();
                embed.WithColor(color1Index, color2Index, color3Index);

                embed.WithImageUrl("" + url);

                await CommandHandeling.ReplyAsync(Context, embed);
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");

                await CommandHandeling.ReplyAsync(Context, embed);
            }
        }

        [Command("Fist")]
        public async Task Fist()
        {
            if (Context.User.Id == 129658526460149760)
            {
                const string url = "https://i.imgur.com/uLfBWZ3.jpg";
                var color1 = new Random();
                var color2 = new Random();
                var color3 = new Random();
                var color1Index = color1.Next(256);
                var color2Index = color2.Next(256);
                var color3Index = color3.Next(256);

                var embed = new EmbedBuilder();
                embed.WithColor(color1Index, color2Index, color3Index);
                embed.WithAuthor("Oh yea");
                embed.WithImageUrl("" + url);

                await CommandHandeling.ReplyAsync(Context, embed);
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/wt8EN8R.jpg");

                await CommandHandeling.ReplyAsync(Context, embed);
            }
        }

        [Command("ETIAM")]
        public async Task Etiam()
        {
            const string url = "https://i.imgur.com/wt8EN8R.jpg";
            var color1 = new Random();
            var color2 = new Random();
            var color3 = new Random();
            var color1Index = color1.Next(256);
            var color2Index = color2.Next(256);
            var color3Index = color3.Next(256);

            var embed = new EmbedBuilder();
            embed.WithColor(color1Index, color2Index, color3Index);
            embed.WithAuthor("INCREDIBLIS");
            embed.WithImageUrl("" + url);

            await CommandHandeling.ReplyAsync(Context, embed);
        }
    }
}