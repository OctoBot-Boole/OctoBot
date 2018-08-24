using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using OctoBot.Commands;
using OctoBot.Configs;
using OctoBot.Configs.Server;
using OctoBot.Configs.Users;

namespace OctoBot.Automated
{
    public class CheckBirthday
    {
        private static Timer _loopingTimer;

        internal static Task CheckTimer()
        {
            _loopingTimer = new Timer
            {
                AutoReset = false,
                Interval = 3000,
                Enabled = true
            };
            _loopingTimer.Elapsed += CheckAllBirthdays;
            return Task.CompletedTask;
        }

        public static async void CheckAllBirthdays(object sender, ElapsedEventArgs e)
        {
            try
            {

                var allServersWithBirthdayRole = ServerAccounts.GetFilteredServerAccounts(s => s.BirthdayRoleId != 0);
                var timeUtcNow = DateTime.UtcNow;
             

                foreach (var server in allServersWithBirthdayRole)
                {
                    var allUserAccounts = UserAccounts.GetOrAddUserAccountsForGuild(server.ServerId);
                    var removeLaterList = new List<ServerSettings.BirthdayRoleActive>();

                    var roleToGive = Global.Client.GetGuild(server.ServerId).GetRole(server.BirthdayRoleId);


                    if (roleToGive == null)
                    {
                        server.BirthdayRoleId = 0;
                        ServerAccounts.SaveServerAccounts();
                        Console.WriteLine($"Birthday Role == NULL ({server.ServerName} - {server.ServerId})");
                        return;
                    }

                    foreach (var t in allUserAccounts)
                    {
                        if (t.Birthday.ToString(CultureInfo.InvariantCulture) == "0001-01-01T00:00:00")
                            continue;

                        if(Global.Client.Guilds.All(x => x.Id != server.ServerId))
                            continue;

                        var globalAccount = Global.Client.GetUser(t.Id);

                            if(globalAccount == null)
                                continue;

                        var account = UserAccounts.GetAccount(globalAccount, server.ServerId);
                        var timezone = account.TimeZone ?? "UTC";

                        try
                        {
                            var fftz = TimeZoneInfo.FindSystemTimeZoneById($"{timezone}");
                        }
                        catch
                        {
                            account.TimeZone = "UTC";
                            UserAccounts.SaveAccounts(server.ServerId);
                            Console.WriteLine($"{account.UserName} TimeZone changed to UTC");
                            continue;
                        }

                        var tz = TimeZoneInfo.FindSystemTimeZoneById($"{timezone}");

                        var timeWhenIsBirthdayByUtc = TimeZoneInfo.ConvertTimeToUtc(account.Birthday, tz);


                        var check = 0;
                        foreach (var l in server.BirthdayRoleList)
                        {
                            if (l.UserId == account.Id)
                                check = 1;
                        }

                        if (timeUtcNow.Month == timeWhenIsBirthdayByUtc.Month &&
                            timeUtcNow.Day == timeWhenIsBirthdayByUtc.Day
                            && check == 0)
                        {
                            Console.WriteLine("here");
                            await Global.Client.GetGuild(server.ServerId).GetUser(account.Id)
                                .AddRoleAsync(roleToGive);

                            var newBirthday = new ServerSettings.BirthdayRoleActive(timeUtcNow +
                                                                                    TimeSpan.ParseExact("1d",
                                                                                        ReminderFormat.Formats,
                                                                                        CultureInfo
                                                                                            .CurrentCulture),
                                account.Id, account.UserName);

                            server.BirthdayRoleList.Add(newBirthday);
                            ServerAccounts.SaveServerAccounts();
                        }                    
                    }
                    
                    if (server.BirthdayRoleList.Count >= 1)
                    {
                        foreach (var v in server.BirthdayRoleList)
                        {
                            if (timeUtcNow > v.DateToRemoveRole)
                            {
                                await Global.Client.GetGuild(server.ServerId).GetUser(v.UserId)
                                    .RemoveRoleAsync(roleToGive);
                                removeLaterList.Add(v);
                            }
                        }
                    }

                    if (removeLaterList.Any())
                    {
                        removeLaterList.ForEach(item => server.BirthdayRoleList.Remove(item));

                        ServerAccounts.SaveServerAccounts();
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("ERROR!!! Birthday Role(Big try) Does not work: '{0}'", error);
            }
        }
    }
}