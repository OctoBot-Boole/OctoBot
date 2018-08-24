using System;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using OctoBot.Configs;
using OctoBot.Configs.Users;
using OctoBot.Helper;

namespace OctoBot.Automated
{
    public class CheckForPull
    {
        private static Timer _loopingTimerForPull;

        internal static Task CheckTimerForPull()
        {
            _loopingTimerForPull = new Timer
            {
                AutoReset = true,
                Interval = 60000,
                Enabled = true
            };
            _loopingTimerForPull.Elapsed += CheckPulls;


            return Task.CompletedTask;
        }

        public static async void CheckPulls(object sender, ElapsedEventArgs e)
        {
            try
            {
                ulong zeroGuildId = 0;
                var allUserAccounts = UserAccounts.GetOrAddUserAccountsForGuild(zeroGuildId);

                foreach (var t in allUserAccounts)
                    if (Global.Client.GetUser(t.Id) != null)
                    {
                        var globalAccount = Global.Client.GetUser(t.Id);
                        var account = UserAccounts.GetAccount(globalAccount, zeroGuildId);
                        var difference = DateTime.UtcNow - account.LastDailyPull;


                        if (difference.TotalHours > 39 && account.DailyPullPoints >= 1)
                            try
                            {
                                var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                var embed = new EmbedBuilder();
                                embed.WithFooter("lil octo notebook");
                                embed.WithTitle("OctoNotification");
                                embed.WithDescription("You have lost all the ponts ;c");
                                await dmChannel.SendMessageAsync("", false, embed.Build());
                                account.DailyPullPoints = 0;
                                UserAccounts.SaveAccounts(zeroGuildId);
                            }
                            catch
                            {
                                Console.WriteLine($"ERROR DM SENING {account.UserName} Closed DM");
                                account.DailyPullPoints = 0;
                                UserAccounts.SaveAccounts(zeroGuildId);
                                return;
                            }

                        if (account.DailyPullPoints >= 28)
                        {
                            var mylorikGlobal = Global.Client.GetUser(181514288278536193);
                            var mylorik = UserAccounts.GetAccount(mylorikGlobal, zeroGuildId);

                            if (mylorik.KeyPullName != null)
                            {
                                var fullKeysNameList =
                                    mylorik.KeyPullName.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                                var fullKeysKeyList =
                                    mylorik.KeyPullKey.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);


                                if (fullKeysNameList.Length == 1)
                                {
                                    Array.Resize(ref fullKeysNameList, fullKeysNameList.Length + 2);
                                    Array.Resize(ref fullKeysKeyList, fullKeysKeyList.Length + 2);

                                    fullKeysNameList[fullKeysNameList.Length - 2] = "Nothing";
                                    fullKeysNameList[fullKeysNameList.Length - 1] = "Nothing";


                                    fullKeysKeyList[fullKeysKeyList.Length - 2] = "boole?";
                                    fullKeysKeyList[fullKeysKeyList.Length - 1] = "boole?";
                                }
                                else if (fullKeysNameList.Length == 2)
                                {
                                    Array.Resize(ref fullKeysNameList, fullKeysNameList.Length + 1);
                                    Array.Resize(ref fullKeysKeyList, fullKeysKeyList.Length + 1);

                                    fullKeysNameList[fullKeysNameList.Length - 1] = "Nothing";
                                    fullKeysKeyList[fullKeysKeyList.Length - 1] = "boole?";
                                }

                                int randonKey1;
                                int randonKey2;
                                int randonKey3;

                                do
                                {
                                    randonKey1 = SecureRandomStatic.Random(0, fullKeysNameList.Length);
                                    randonKey2 = SecureRandomStatic.Random(0, fullKeysNameList.Length);
                                    randonKey3 = SecureRandomStatic.Random(0, fullKeysNameList.Length);
                                } while (randonKey1 == randonKey2 || randonKey2 == randonKey3 ||
                                         randonKey1 == randonKey3);


                                UserAccounts.SaveAccounts(zeroGuildId);


                                var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                var embed = new EmbedBuilder();

                                var text =
                                    "**IMPORTANT, pelase read BEFORE cKey command:**\nIf you are NOT going to PLAY this game - please say `cKey 0`, and we will use this key to the one who WILL play the game.\n\n" +
                                    $"Choose one or zero:\nSay `cKey number`\n\n**1. {fullKeysNameList[randonKey1]}**\n**2. {fullKeysNameList[randonKey2]}**\n**3. {fullKeysNameList[randonKey3]}**\n\n**0. Nothing**\n\n" +
                                    $"Boole.\n{new Emoji("<:octo_hi:465374417644552192>")}";
                                embed.WithFooter("lil octo notebook");
                                embed.WithColor(Color.Green);
                                embed.WithTitle("You can choose a game now!");
                                embed.WithDescription(text);
                                await dmChannel.SendMessageAsync("", false, embed.Build());

                                account.PullToChoose = null;
                                account.PullToChoose += $"{randonKey1}%%";
                                account.PullToChoose += $"{randonKey2}%%";
                                account.PullToChoose += $"{randonKey3}%%";

                                account.DailyPullPoints = 0;
                                UserAccounts.SaveAccounts(zeroGuildId);
                            }
                            else
                            {
                                var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                                var embed = new EmbedBuilder();
                                embed.WithFooter("lil octo notebook");
                                embed.WithColor(Color.Green);
                                embed.WithTitle("OctoNotification");
                                embed.WithDescription("Booole... There is no more keys.... ;c");
                                await dmChannel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                    }
            }
            catch
            {
                Console.WriteLine("Failed To ReadFile(CheckPulls). Will ty in 5 sec.");
            }
        }
    }
}