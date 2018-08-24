using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using OctoBot.Configs;
using OctoBot.Configs.Server;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Handeling;
using Ofl.Google.Maps.TimeZone;

namespace OctoBot.Commands
{
    public class Birthday : ModuleBase<ShardedCommandContextCustom>
    {

        [Command("SetBirthdayRole")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task SetBirthdayrole(SocketRole role = null)
        {
            var guildAccount = ServerAccounts.GetServerAccount(Context.Guild);
            if (role != null)
            {
                guildAccount.BirthdayRoleId = role.Id;
                ServerAccounts.SaveServerAccounts();
                await CommandHandeling.ReplyAsync(Context, $"Birthday role set! ({role.Mention})");
            }

            if (role == null)
            {
                var change = await Context.Guild.CreateRoleAsync($"🍰",
                    new GuildPermissions(), Color.Gold, true, RequestOptions.Default);
                await change.ModifyAsync(p => p.Position = 2);

                guildAccount.BirthdayRoleId = change.Id;
                ServerAccounts.SaveServerAccounts();
                await CommandHandeling.ReplyAsync(Context, $"We have creatyed and set Birthday role - **{change.Name}**!");
            }
        }

        
        [Command("SetBirthdayRole")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task SetBirthdayrole(string role = null)
        {
            var guildAccount = ServerAccounts.GetServerAccount(Context.Guild);
            if (role != null)
            {
                var list = Context.Guild.Roles.SingleOrDefault(x => string.Equals(x.Name, role, StringComparison.CurrentCultureIgnoreCase));
                if (list != null)
                {
                    guildAccount.BirthdayRoleId = list.Id;
                    ServerAccounts.SaveServerAccounts();
                    await CommandHandeling.ReplyAsync(Context, $"Birthday role set! ({list.Name})");
                }
                else
                {
                    await CommandHandeling.ReplyAsync(Context, "No such role found");
                    return;       
                }
            }

            if (role == null)
            {
                var change = await Context.Guild.CreateRoleAsync($"🍰",
                    new GuildPermissions(), Color.Gold, true, RequestOptions.Default);
                await change.ModifyAsync(p => p.Position = 2);
                guildAccount.BirthdayRoleId = change.Id;
                ServerAccounts.SaveServerAccounts();
                await CommandHandeling.ReplyAsync(Context, $"We have creatyed and set Birthday role! ({change.Name})");
            }
        }

        public async Task<string> ConvertCityToTimeZoneName(string location)
        {
            TimeZoneResponse response = new TimeZoneResponse();
            var plusName = location.Replace(" ", "+");
            var address = "http://maps.google.com/maps/api/geocode/json?address=" + plusName + "&sensor=false";
            var result =  await Global.SendWebRequest(address); //new System.Net.WebClient().DownloadString(address);
            var latLongResult = JsonConvert.DeserializeObject<dynamic>(result);

            if (latLongResult.status == "OK")
            {
                string string1 = latLongResult.results[0].geometry.location.lat.ToString().Replace(",", ".");
                string string2 = latLongResult.results[0].geometry.location.lng.ToString().Replace(",", ".");

                var timeZoneRespontimeZoneRequest = "https://maps.googleapis.com/maps/api/timezone/json?location=" + 
                                                    string1 + "," +
                                                    string2 +
                                                    "&sensor=false&timestamp=1362209227";

                 var timeZoneResponseString = await Global.SendWebRequest(timeZoneRespontimeZoneRequest);  //new System.Net.WebClient().DownloadString(timeZoneRespontimeZoneRequest);
                var timeZoneResult = JsonConvert.DeserializeObject<dynamic>(timeZoneResponseString);

                if (timeZoneResult.status == "OK")
                {

                    response.TimeZoneId = timeZoneResult.timeZoneId;
                    return response.TimeZoneId;
                }
            }
            return "error";
        }

        [Command("MyBirthday")]
        public async Task SetMyBirthday([Remainder] string stringdate)
        { 
            if (!DateTime.TryParse(stringdate, out var myDate))
            {
               await CommandHandeling.ReplyAsync(Context, "Date input is not correct, you can try this `yyyy-mm-dd`");
               return;
            }
            var now = DateTime.UtcNow;
            myDate = DateTime.Parse(stringdate);
            if (now.Year - myDate.Year < 13)
            {
                await CommandHandeling.ReplyAsync(Context, "You must be over 13 years old to use Discord.");
                return;
            }
            var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            account.Birthday = myDate;
          
            UserAccounts.SaveAccounts(Context.Guild.Id);
            await CommandHandeling.ReplyAsync(Context, $"**Done!** You have born on {myDate.Year}-{myDate.Month}-{myDate.Day}\n\n" +
                                                       $"Please say `MyCity city` to set TimeZone (default UTC)\n" +
                                                       $"Btw, uyou may use **your language** to say the city");
        }

        [Command("MyCity")]
        public async Task SetMyCity([Remainder] string city)
        {
            var timeZone = ConvertCityToTimeZoneName(city);
            if (timeZone.Result == "error")
            {
                await CommandHandeling.ReplyAsync(Context, "Something went wrong... Try to check your spelling");
                return;
            }

            var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            var account2 = UserAccounts.GetAccount(Context.User, 0);
            account.TimeZone = $"{timeZone.Result}";
            account2.TimeZone = $"{timeZone.Result}";
            UserAccounts.SaveAccounts(Context.Guild.Id);
            UserAccounts.SaveAccounts(0);
            await CommandHandeling.ReplyAsync(Context, $"We saved it. Your TimeZone is **{timeZone.Result}**");
        }
    }
}
