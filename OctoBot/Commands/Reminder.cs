﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Handeling;
using OctoBot.Helper;
using static OctoBot.Configs.Users.AccountSettings;

namespace OctoBot.Commands
{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

    public class ReminderFormat
    {
        public static string[] Formats =
        {
            // Used to parse stuff like 1d14h2m11s and 1d 14h 2m 11s could add/remove more if needed

            "d'd'",
            "d'd'm'm'", "d'd 'm'm'",
            "d'd'h'h'", "d'd 'h'h'",
            "d'd'h'h's's'", "d'd 'h'h 's's'",
            "d'd'm'm's's'", "d'd 'm'm 's's'",
            "d'd'h'h'm'm'", "d'd 'h'h 'm'm'",
            "d'd'h'h'm'm's's'", "d'd 'h'h 'm'm 's's'",

            "h'h'",
            "h'h'm'm'", "h'h m'm'",
            "h'h'm'm's's'", "h'h 'm'm 's's'",
            "h'h's's'", "h'h s's'",
            "h'h'm'm'", "h'h 'm'm'",
            "h'h's's'", "h'h 's's'",

            "m'm'",
            "m'm's's'", "m'm 's's'",

            "s's'"
        };
    }


    public class Reminder : ModuleBase<ShardedCommandContextCustom>
    {
        private readonly SecureRandom _secureRandom;

        public Reminder(SecureRandom secureRandom)
        {
            _secureRandom = secureRandom;
        }

        [Command("Remind", RunMode = RunMode.Async)]
        [Priority(1)]
        [Alias("Напомнить", "напомни мне", "напиши мне", "напомни", "алярм", " Напомнить", " напомни мне",
            " напиши мне", " напомни", " алярм", " Remind")]
        public async Task AddReminder([Remainder] string args)
        {
            try
            {    
                string[] splittedArgs = { };
                if (args.ToLower().Contains("  in ")) splittedArgs = args.ToLower().Split(new[] {"  in "}, StringSplitOptions.None);
                else if (args.ToLower().Contains(" in  ")) splittedArgs = args.ToLower().Split(new[] {" in  "}, StringSplitOptions.None);
                else if (args.ToLower().Contains("  in  ")) splittedArgs = args.ToLower().Split(new[] {"  in  "}, StringSplitOptions.None);
                else if (args.ToLower().Contains(" in ")) splittedArgs = args.ToLower().Split(new[] {" in "}, StringSplitOptions.None);

                
                if (splittedArgs == null)
                {
                    const string bigmess = "boole-boole... you are using this command incorrectly!!\n" +
                                           "Right way: `Remind [text] in [time]`\n" +
                                           "Between message and time **HAVE TO BE** written `in` part" +
                                           "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                                           "I'm a loving order octopus!";
                    await CommandHandeling.ReplyAsync(Context, bigmess);
                    return;
                }
                var account = UserAccounts.GetAccount(Context.User, 0);
                var accountForTimeZone = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                
                var timezone = accountForTimeZone.TimeZone ?? "UTC";

                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById($"{timezone}");


                var timeString = splittedArgs[splittedArgs.Length-1];
                if (timeString == "24h")
                    timeString = "1d";
                splittedArgs[splittedArgs.Length-1] = "";
                var reminderString = string.Join(" in ", splittedArgs, 0, splittedArgs.Length-1);

                var timeDateTime = DateTime.UtcNow +
                                   TimeSpan.ParseExact(timeString, ReminderFormat.Formats, CultureInfo.CurrentCulture);
                var randomIndex = _secureRandom.Random(0, OctoNamePull.OctoNameRu.Length-1);
                var randomOcto = OctoNamePull.OctoNameRu[randomIndex];

                var extra = randomOcto.Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
                var name = extra[0].Remove(0, 1);
                var url = extra[1].Remove(extra[1].Length - 1, 1);


                var localTime = TimeZoneInfo.ConvertTimeFromUtc(timeDateTime, tz);

                var bigmess2 =
                    $"{reminderString}\n\n" +
                    $"We will send you a DM in  __**{localTime}**__ `by {timezone}`\n";
                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithCurrentTimestamp();
                embed.WithColor(_secureRandom.Random(0, 254), _secureRandom.Random(0, 254),
                    _secureRandom.Random(0, 254));
                embed.AddField($"**____**", $"{bigmess2}");
                embed.WithTitle($"{name} напомнит тебе:");
                embed.WithUrl(url);

                
                var newReminder = new CreateReminder(timeDateTime, reminderString);

                account.ReminderList.Add(newReminder);
                UserAccounts.SaveAccounts(0);


                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch (Exception e)
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \n" +
                    "Say `HelpRemind`");
                 HelperFunctions.DeleteMessOverTime(botMess, 5);
                ConsoleLogger.Log($" [REMINDER][Exception] ({Context.User.Username}) - {e.Message}",
                    ConsoleColor.DarkBlue);
                Console.WriteLine(e.Message);
            }
        }

        ///REMINDER FOR MINUTES!
        [Command("Re")]
        public async Task AddReminderMinute(uint minute = 0, [Remainder] string reminderString = null)
        {
            try
            {
                if (minute > 1439)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Booole. [time] have to be in range 0-1439 (in minutes)");


                    return;
                }

                var hour = 0;
                var timeFormat = $"{minute}m";

                if (minute >= 60)
                    for (var i = 0; minute >= 59; i++)
                    {
                        minute = minute - 59;
                        hour++;

                        timeFormat = $"{hour}h {minute}m";
                    }

                var timeString = timeFormat; //// MAde t ominutes

                var timeDateTime = DateTime.UtcNow +
                                   TimeSpan.ParseExact(timeString, ReminderFormat.Formats, CultureInfo.CurrentCulture);

                var randomIndex = _secureRandom.Random(0, OctoNamePull.OctoNameRu.Length-1);
                var randomOcto = OctoNamePull.OctoNameRu[randomIndex];
                var extra = randomOcto.Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
                var name = extra[0].Remove(0, 1);
                var url = extra[1].Remove(extra[1].Length - 1, 1);

                var bigmess =
                    $"{reminderString}\n\n" +
                    $"We will send you a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                    $"**Time Now:                               {DateTime.UtcNow}** `by UTC`";

                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithCurrentTimestamp();
                embed.WithColor(_secureRandom.Random(0, 255), _secureRandom.Random(0, 255),
                    _secureRandom.Random(0, 255));
                embed.AddField($"**____**", $"{bigmess}");
                embed.WithTitle($"{name} напомнит тебе:");
                embed.WithUrl(url);

                await CommandHandeling.ReplyAsync(Context, embed);


                var account = UserAccounts.GetAccount(Context.User, 0);
                //account.SocketUser = SocketGuildUser(Context.User);
                var newReminder = new CreateReminder(timeDateTime, reminderString);

                account.ReminderList.Add(newReminder);
                UserAccounts.SaveAccounts(0);
            }
            catch
            {
                var botMess =
                    await ReplyAsync(
                        "boo... An error just appear >_< \n" +
                        "Say `HelpRemind`");
                HelperFunctions.DeleteMessOverTime(botMess, 5);
            }
        }

        //REminder To A User
        [Command("RemTo")]
        [Alias("RemindTo", "RemindTo")]
        public async Task AddReminderToSomeOne(ulong userId, [Remainder] string args)
        {
            try
            {

                string[] splittedArgs = null;
                 if (args.ToLower().Contains("  in ")) splittedArgs = args.ToLower().Split(new[] {"  in "}, StringSplitOptions.None);
                else if (args.ToLower().Contains(" in  ")) splittedArgs = args.ToLower().Split(new[] {" in  "}, StringSplitOptions.None);
                else if (args.ToLower().Contains("  in  ")) splittedArgs = args.ToLower().Split(new[] {"  in  "}, StringSplitOptions.None);
                else if (args.ToLower().Contains(" in ")) splittedArgs = args.ToLower().Split(new[] {" in "}, StringSplitOptions.None);


                if (splittedArgs == null)
                {
                    var bigmess = "boole-boole... you are using this command incorrectly!!\n" +
                                  "Right way: `Remind [text] in [time]`\n" +
                                  "Between message and time **HAVE TO BE** written `in` part" +
                                  "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                                  "I'm a loving order octopus!";

                    await CommandHandeling.ReplyAsync(Context, bigmess);

                    return;
                }

                var timeString = splittedArgs[splittedArgs.Length-1];
                if (timeString == "24h")
                    timeString = "1d";
                splittedArgs[splittedArgs.Length-1] = "";
                var reminderString = string.Join(" in ", splittedArgs, 0, splittedArgs.Length-1);

                var timeDateTime =
                    DateTime.UtcNow +
                    TimeSpan.ParseExact(timeString, ReminderFormat.Formats, CultureInfo.CurrentCulture);

                var user = Global.Client.GetUser(userId);


                var randomIndex = _secureRandom.Random(0, OctoNamePull.OctoNameRu.Length-1);
                var randomOcto = OctoNamePull.OctoNameRu[randomIndex];
                var extra = randomOcto.Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
                var name = extra[0].Remove(0, 1);
                var url = extra[1].Remove(extra[1].Length - 1, 1);

                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithCurrentTimestamp();
                embed.WithColor(_secureRandom.Random(0, 255), _secureRandom.Random(0, 255),
                    _secureRandom.Random(0, 255));

                var bigmess2 =
                    $"{reminderString}\n\n" +
                    $"We will send you a DM in  __**{timeDateTime}**__ `by UTC`\n" +
                    $"**Time Now:                               {DateTime.UtcNow}** `by UTC`";


                embed.AddField($"**____**", $"{bigmess2}");
                embed.WithTitle($"{name} напомнит {user.Username}:");
                embed.WithUrl(url);


                var account = UserAccounts.GetAccount(user, 0);
                var newReminder = new CreateReminder(timeDateTime, $"From {Context.User.Username}: " + reminderString);

                account.ReminderList.Add(newReminder);
                UserAccounts.SaveAccounts(0);


                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \n" +
                    "Say `HelpRemind`");
               HelperFunctions.DeleteMessOverTime(botMess, 5);
            }
        }

        [Command("List")]
        [Alias("Напоминания", "Мои Напоминания", "список")]
        public async Task ShowReminders()
        {
            try
            {
                var account = UserAccounts.GetAccount(Context.User, 0);
                if (account.ReminderList.Count == 0)
                {
                    var bigmess =
                        "Booole... You have no reminders! You can create one by using the command `Remind [text] in [time]`\n" +
                        "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                        "I'm a loving order octopus!";

                    await CommandHandeling.ReplyAsync(Context, bigmess);


                    return;
                }

                var reminders = account.ReminderList;
                var embed = new EmbedBuilder();
                embed.WithTitle("Your Reminders:");
                embed.WithDescription($"**Your current time by UTC: {DateTime.UtcNow}**\n" +
                                      "To delete one of them, type the command `*Delete [index]`");
                embed.WithFooter("lil octo notebook");

                for (var i = 0; i < reminders.Count; i++)
                    embed.AddField($"[{i + 1}] {reminders[i].DateToPost:f}", reminders[i].ReminderMessage, true);

                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \n" +
                    "Say `HelpRemind`");

                HelperFunctions.DeleteMessOverTime(botMess, 5);
            }
        }


        [Command("List")]
        [Alias("Напоминания", "Мои Напоминания", "список")]
        public async Task ShowUserReminders(SocketUser user)
        {
            try
            {
                var commander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (commander.OctoPass >= 10)
                {
                    var account = UserAccounts.GetAccount(user, 0);
                    if (account.ReminderList.Count == 0)
                    {
                        var bigmess =
                            "Booole... You have no reminders! You can create one by using the command `Remind [text] in [time]`\n" +
                            "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                            "I'm a loving order octopus!";

                        await CommandHandeling.ReplyAsync(Context, bigmess);

                        return;
                    }

                    var reminders = account.ReminderList;
                    var embed = new EmbedBuilder();
                    embed.WithTitle("Your Reminders:");
                    embed.WithDescription($"**Your current time by UTC: {DateTime.UtcNow}**\n" +
                                          "To delete one of them, type the command `*del [index]`");
                    embed.WithFooter("lil octo notebook");

                    for (var i = 0; i < reminders.Count; i++)
                        embed.AddField($"[{i + 1}] {reminders[i].DateToPost:f}", reminders[i].ReminderMessage, true);


                    await CommandHandeling.ReplyAsync(Context, embed);
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Boole! You do not have a tolerance of this level!");
                }
            }
            catch
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \n" +
                    "Say `HelpRemind`");
                 HelperFunctions.DeleteMessOverTime(botMess, 5);
            }
        }

        [Command("Delete")]
        [Alias("Удалить Напоминания", "Удалить", "Удалить Напоминание", "del")]
        public async Task DeleteReminder(int index)
        {
            try
            {
                var account = UserAccounts.GetAccount(Context.User, 0);

                var reminders = account.ReminderList;

                if (index > 0 && index <= reminders.Count)
                {
                    reminders.RemoveAt(index - 1);
                    UserAccounts.SaveAccounts(0);
                    var embed = new EmbedBuilder();
                    // embed.WithImageUrl("");
                    embed.WithTitle("Boole.");
                    embed.WithDescription($"Message by index **{index}** was removed!");
                    embed.WithFooter("lil octo notebook");


                    await CommandHandeling.ReplyAsync(Context, embed);

                    return;
                }

                var bigmess =
                    $"Booole...We could not find this reminder, could there be an error?\n" +
                    $"Try to see all of your reminders through the command `list`";

                await CommandHandeling.ReplyAsync(Context, bigmess);
            }
            catch
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \n" +
                    "Say `HelpRemind`");
                HelperFunctions.DeleteMessOverTime(botMess, 5);
            }
        }

        [Command("Время")]
        [Alias("time", "date")]
        public async Task CheckTime()
        {
            try
            {
                var bigmess = $"**UTC Current Time: {DateTime.UtcNow}**";

                await CommandHandeling.ReplyAsync(Context, bigmess);
            }
            catch
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \nTry to use this command properly: **time**(see current time by UTC)\n" +
                    "Alias: Удалить, Delete");
                 HelperFunctions.DeleteMessOverTime(botMess, 5);
            }
        }



        [Command("RemindOn", RunMode = RunMode.Async)]
        [Alias("Remind On")]
        public async Task AddReminderOn(string timeOn, [Remainder] string args)
        {
            try
            {    
                string[] splittedArgs = { };
                if (args.ToLower().Contains("  at ")) splittedArgs = args.ToLower().Split(new[] {"  at "}, StringSplitOptions.None);
                else if (args.ToLower().Contains(" at  ")) splittedArgs = args.ToLower().Split(new[] {" at  "}, StringSplitOptions.None);
                else if (args.ToLower().Contains("  at  ")) splittedArgs = args.ToLower().Split(new[] {"  at  "}, StringSplitOptions.None);
                else if (args.ToLower().Contains(" at ")) splittedArgs = args.ToLower().Split(new[] {" at "}, StringSplitOptions.None);
               
                if (!DateTime.TryParse(timeOn, out var myDate) ) //|| myDate < DateTime.Now
                {
                    await CommandHandeling.ReplyAsync(Context, "Date input is not correct, you can try this `yyyy-mm-dd`");
                    return;
                }          
                if (splittedArgs == null)
                {
                    const string bigmess = "boole-boole... you are using this command incorrectly!!\n" +
                                           "Right way: `Remind [text] in [time]`\n" +
                                           "Between message and time **HAVE TO BE** written `in` part" +
                                           "(Time can be different, but follow the rules! **day-hour-minute-second**. You can skip any of those parts, but they have to be in the same order. One space or without it between each of the parts\n" +
                                           "I'm a loving order octopus!";
                    await CommandHandeling.ReplyAsync(Context, bigmess);
                    return;
                }
                var account = UserAccounts.GetAccount(Context.User, 0);
                var accountForTimeZone = UserAccounts.GetAccount(Context.User, Context.Guild.Id);

                
                var timezone = accountForTimeZone.TimeZone ?? "UTC";

                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById($"{timezone}");
                            
                var timeString = splittedArgs[splittedArgs.Length-1];
                
                splittedArgs[splittedArgs.Length-1] = "";
                var reminderString = string.Join(" at ", splittedArgs, 0, splittedArgs.Length-1);
                var hourTime = TimeSpan.ParseExact(timeString, "h\\:mm", CultureInfo.CurrentCulture);

                var timeDateTime = TimeZoneInfo.ConvertTimeToUtc(myDate + hourTime, tz);


                var randomIndex = _secureRandom.Random(0, OctoNamePull.OctoNameRu.Length-1);
                var randomOcto = OctoNamePull.OctoNameRu[randomIndex];

                var extra = randomOcto.Split(new[] {"]("}, StringSplitOptions.RemoveEmptyEntries);
                var name = extra[0].Remove(0, 1);
                var url = extra[1].Remove(extra[1].Length - 1, 1);

                var bigmess2 =
                    $"{reminderString}\n\n" +
                    $"We will send you a DM in  __**{myDate + hourTime}**__ `by {timezone}`\n";
                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithCurrentTimestamp();
                embed.WithColor(_secureRandom.Random(0, 255), _secureRandom.Random(0, 255),
                    _secureRandom.Random(0, 255));
                embed.AddField($"**____**", $"{bigmess2}");
                embed.WithTitle($"{name} напомнит тебе:");
                embed.WithUrl(url);

                
                var newReminder = new CreateReminder(timeDateTime, reminderString);

                account.ReminderList.Add(newReminder);
                UserAccounts.SaveAccounts(0);


                await CommandHandeling.ReplyAsync(Context, embed);
            }
            catch (Exception e)
            {
                var botMess = await ReplyAsync(
                    "boo... An error just appear >_< \n" +
                    "Say `HelpRemind`");
                 HelperFunctions.DeleteMessOverTime(botMess, 5);
                ConsoleLogger.Log($" [REMINDER][Exception] ({Context.User.Username}) - {e.Message}",
                    ConsoleColor.DarkBlue);
                Console.WriteLine(e.Message);
            }
        }

    }
}