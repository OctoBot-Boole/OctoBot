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
    public class OctopusPic : ModuleBase<ShardedCommandContextCustom>
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private readonly SecureRandom _secureRandom;

        public OctopusPic(SecureRandom secureRandom)
        {
            _secureRandom = secureRandom;
        }

        [Command("octo")]
        [Alias("окто", "octopus", "Осьминог", "Осьминожка", "Осьминога", "o", "oct", "о")]
        public async Task OctopusPicture()
        {
            try
            {
                var embed = new EmbedBuilder();
                var account = UserAccounts.GetAccount(Context.User, 0);
                var difference = DateTime.UtcNow - account.LastOctoPic;

                if (difference.TotalHours > 5)
                {
                    var randomKeyPossible = _secureRandom.Random(0, 100000);
                    if (randomKeyPossible == 1448)
                        embed.AddField("Boole!?", "What is it? Some king of a quest... I found a number - **228**, " +
                                                  "what can it mean... if you will know, say `quest ANSWER`, I think this should be one eglish word.");
                }

                account.LastOctoPic = DateTime.UtcNow;
                UserAccounts.SaveAccounts(0);

                var index = _secureRandom.Random(0, 254);
                if (index == 5 || index == 38 || index == 69)
                {
                    var lll = await Context.Channel.SendMessageAsync("boole");
                    HelperFunctions.DeleteMessOverTime(lll, 6);
                }
                else
                {
                    var octoIndex = _secureRandom.Random(0, OctoPicPull.OctoPics.Length-1);
                    var octoToPost = OctoPicPull.OctoPics[octoIndex];


                    var color1Index = _secureRandom.Random(0, 254);
                    var color2Index = _secureRandom.Random(0, 254);
                    var color3Index = _secureRandom.Random(0, 254);

                    var randomIndex = _secureRandom.Random(0, OctoNamePull.OctoNameRu.Length);
                    var randomOcto = OctoNamePull.OctoNameRu[randomIndex];


                    embed.WithDescription($"{randomOcto} found:");
                    embed.WithFooter("lil octo notebook");
                    embed.WithColor(color1Index, color2Index, color3Index);
                    embed.WithAuthor(Context.User);
                    embed.WithImageUrl("" + octoToPost);


                    await CommandHandeling.ReplyAsync(Context, embed);


                    if (octoIndex == 19)
                    {
                        var lll = await Context.Channel.SendMessageAsync("Ooooo, it was I who just passed Dark Souls!");
                        HelperFunctions.DeleteMessOverTime(lll, 6);
                    }

                    if (octoIndex == 9)
                    {
                        var lll = await Context.Channel.SendMessageAsync("I'm drawing an octopus :3");
                        HelperFunctions.DeleteMessOverTime(lll, 6);
                    }

                    if (octoIndex == 26)
                    {
                        var lll = await Context.Channel.SendMessageAsync(
                            "Oh, this is New Year! time to gift turtles!!");
                        HelperFunctions.DeleteMessOverTime(lll, 6);
                    }
                }
            }
            catch
            {
                // await ReplyAsync("boo... An error just appear >_< \nTry to use this command properly: **Octo**\n");
            }
        }

        [Command("octo")]
        [Alias("окто", "octopus", "Осьминог", "Осьминожка", "Осьминога", "o", "oct", "о")]
        public async Task OctopusPictureSelector(int selection)
        {
            try
            {
                var passCheck = UserAccounts.GetAccount(Context.User, Context.Guild.Id);


                if (passCheck.OctoPass >= 1)
                {
                    var index = _secureRandom.Random(0, 254);
                    if (index == 68 || index == 228 || index == 18)
                    {
                        var lll = await Context.Channel.SendMessageAsync("boole");
                        HelperFunctions.DeleteMessOverTime(lll, 6);
                    }
                    else
                    {
                        if (OctoPicPull.OctoPics.Length - 1 < selection)
                        {
                            await Context.Channel.SendMessageAsync(
                                $"Boole. The maximum available index is {OctoPicPull.OctoPics.Length - 1}");
                            return;
                        }

                        var octoToPost = OctoPicPull.OctoPics[selection];

                        var color1Index = _secureRandom.Random(0, 254);
                        var color2Index = _secureRandom.Random(0, 254);
                        var color3Index = _secureRandom.Random(0, 254);

                        var randomIndex = _secureRandom.Random(0, OctoNamePull.OctoNameRu.Length);
                        var randomOcto = OctoNamePull.OctoNameRu[randomIndex];

                        var embed = new EmbedBuilder();
                        embed.WithColor(color1Index, color2Index, color3Index);
                        embed.WithDescription($"{randomOcto} found:");
                        embed.WithFooter("lil octo notebook");
                        embed.WithAuthor(Context.User);
                        embed.WithImageUrl("" + octoToPost);


                        await CommandHandeling.ReplyAsync(Context, embed);
                      
                    }
                }
                else
                {
                    await CommandHandeling.ReplyAsync(Context, "Boole! You do not have a tolerance of this level!");
                }
            }
            catch
            {
                // await ReplyAsync(
                //       "boo... An error just appear >_< \nTry to use this command properly: **Octo [Octo_index]**\n");
            }
        }
    }
}