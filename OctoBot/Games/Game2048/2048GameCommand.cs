using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using OctoBot.Configs;
using OctoBot.Custom_Library;

namespace OctoBot.Games.Game2048
{
    public class GameBridge : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("2048")]
        public async Task Start1024Game()
        {
            if (NewGame.UserIsPlaying(Context.User.Id))
            {
                await ReplyAsync(
                    "you are **already** playing, you need to finish what you started (well... or just use `*e2` command)");
                return;
            }

            var message = await Context.Channel.SendMessageAsync("**Please wait**");


            await message.AddReactionAsync(new Emoji("⬅"));
            await message.AddReactionAsync(new Emoji("➡"));
            await message.AddReactionAsync(new Emoji("⬆"));
            await message.AddReactionAsync(new Emoji("⬇"));
            await message.AddReactionAsync(new Emoji("🔃"));
            await message.AddReactionAsync(new Emoji("❌"));


            NewGame.CreateNewGame(Context.User.Id, message);

            var new2048Game =
                new Global.OctoGameMessAndUserTrack2048(message.Id, Context.User.Id, message, Context.User);

            Global.OctopusGameMessIdList2048.Add(new2048Game);
        }

        [Command("end2048")]
        [Alias("finish2048", "e2048", "e2")]
        public async Task End2048Gmae()
        {
            NewGame.EndGame(Context.User.Id);
            await Task.CompletedTask;
        }
    }
}