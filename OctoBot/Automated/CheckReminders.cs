using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using OctoBot.Configs;
using OctoBot.Configs.Users;
using OctoBot.Handeling;

namespace OctoBot.Automated
{
    public class CheckReminders
    {
        private static Timer _loopingTimer;

        internal static Task CheckTimer()
        {
            _loopingTimer = new Timer
            {
                AutoReset = true,
                Interval = 20000,
                Enabled = true
            };
            _loopingTimer.Elapsed += CheckForBirthdayRole;
            return Task.CompletedTask;
        }

        public static async void CheckForBirthdayRole(object sender, ElapsedEventArgs e)
        {
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

                    var removeLaterList = new List<AccountSettings.CreateReminder>();

                    for (var j = 0; j < account.ReminderList?.Count; j++)
                    {
                        if (account.ReminderList[j].DateToPost > now || removeLaterList.Any( x => x.ReminderMessage == account.ReminderList[j].ReminderMessage))
                            continue;

                        try
                        {
                            var dmChannel = await globalAccount.GetOrCreateDMChannelAsync();
                            var embed = new EmbedBuilder();
                            embed.WithFooter("lil octo notebook");
                            embed.WithColor(Color.Teal);
                            embed.WithTitle("Pink turtle remindinds you:");
                            embed.WithDescription($"\n{account.ReminderList[j].ReminderMessage}");

                            await dmChannel.SendMessageAsync("", false, embed.Build());

                            removeLaterList.Add(account.ReminderList[j]);
                        }
                        catch (Exception closedDm)
                        {
                            try
                            {
                                ConsoleLogger.Log(
                                    $" [REMINDER] ({account.UserName}) - {account.ReminderList[j].ReminderMessage}",
                                    ConsoleColor.DarkBlue);
                                if (!closedDm.Message.Contains("404") || !closedDm.Message.Contains("403")) continue;
                                Console.WriteLine(
                                    $"ERROR DM SENING (TRY-CATCH DELETE) {account.UserName} Closed DM: '{0}'",
                                    closedDm);
                                account.ReminderList = new List<AccountSettings.CreateReminder>();
                                UserAccounts.SaveAccounts(0);
                                return;
                            }
                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"ERROR REMINDER (Catch-catch) ?????? {account.UserName}");
                                Console.ResetColor();
                            }
                        }
                    }

                    if (removeLaterList.Any())
                    {
                        removeLaterList.ForEach(item => account.ReminderList.Remove(item));
                        UserAccounts.SaveAccounts(0);
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("ERROR!!! REMINDER(Big try) Does not work: '{0}'", error);
            }
        }
    }
}