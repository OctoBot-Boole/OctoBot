using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using OctoBot.Configs;
using OctoBot.Configs.Users;

namespace OctoBot.Automated
{
    public class CheckForMute
    {
        private static Timer _loopingTimer;

        internal static Task CheckTimer()
        {
            _loopingTimer = new Timer
            {
                AutoReset = true,
                Interval = 5000,
                Enabled = true
            };

            _loopingTimer.Elapsed += ChekAllMutes;


            return Task.CompletedTask;
        }

        public static async void ChekAllMutes(object sender, ElapsedEventArgs e)

        {
            return;
            try
            {
                var allUserAccounts = UserAccounts.GetOrAddUserAccountsForGuild(0);
                var now = DateTime.UtcNow;

                foreach (var t in allUserAccounts)
                {
                    if (Global.Client.GetUser(t.Id) == null)
                        continue;

                    var globalAccount = Global.Client.GetUser(t.Id);
                    var account = UserAccounts.GetAccount(globalAccount, 0);

                    if (account.MuteTimer <= now && account.MuteTimer != Convert.ToDateTime("0001-01-01T00:00:00"))
                    {
                        var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                            .SingleOrDefault(x => x.Name.ToString() == "Muted");
                        var wtf = Global.Client.GetGuild(338355570669256705).GetUser(account.Id);
                        await wtf.RemoveRoleAsync(roleToGive);
                        await wtf.ModifyAsync(u => u.Mute = false);
                        account.MuteTimer = Convert.ToDateTime("0001-01-01T00:00:00");
                        UserAccounts.SaveAccounts(0);

                        try
                        {
                            var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                            var embed = new EmbedBuilder();
                            embed.WithFooter("lil octo notebook");
                            embed.WithColor(Color.Red);
                            embed.WithImageUrl("https://i.imgur.com/puNz7pu.jpg");
                            embed.WithDescription($"бу-бу-бу!\nБольше так не делай, тебя размутили.");

                            await dmChannel.SendMessageAsync("", false, embed.Build());
                        }
                        catch
                        {
                            var embed = new EmbedBuilder();
                            embed.WithFooter("lil octo notebook");
                            embed.WithColor(Color.Red);
                            embed.WithImageUrl("https://i.imgur.com/puNz7pu.jpg");
                            embed.WithDescription($"бу-бу-бу!\nБольше так не делай, тебя размутили.");

                            await Global.Client.GetGuild(338355570669256705).GetTextChannel(374914059679694848)
                                .SendMessageAsync("", false, embed.Build());
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("ERROR!!! ChekAllMutes(Big try) Does not work: '{0}'", error);
            }
        }
    }
}