using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;


//Green Boo Event ( not done)
namespace OctoBot.Commands.PersonalCommands
{
    internal static class GreenBuuTimerClass
    {
        private static Timer _loopingTimer;
        private static SocketTextChannel _channel;

        internal static Task StartTimer()
        {
            _channel = Global.Client.GetGuild(375104801018609665).GetTextChannel(375104801018609667);

            _loopingTimer = new Timer
            {
                AutoReset = true,
                Interval = 5000,
                Enabled = true
            };
            _loopingTimer.Elapsed += GreenBuuTimer;


            return Task.CompletedTask;
        }

        private static async void GreenBuuTimer(object sender, ElapsedEventArgs e)
        {
            try
            {
                var buu = new Random();
                var Event = buu.Next(10000);
                if (Event == 228)
                {
                    await _channel.SendMessageAsync("**Бэм-бэм-бэм-бэим! Зелёный злюка в здании!**");

                    var embed = new EmbedBuilder();
                    embed.WithImageUrl("https://i.imgur.com/Auw6W0W.jpg");
                    await _channel.SendMessageAsync("", false, embed.Build());

                    //Random User
                    var rng = new Random();
                    var randomSocketGuildUser = Global.Client.GetGuild(375104801018609665).Users
                        .OrderBy(r => rng.Next()).FirstOrDefault();
                    ///////////////
                    //  var newColor = await Global.Client.GetGuild(375104801018609665).CreateRoleAsync(name: "RED", permissions: null, color: Color.Red, isHoisted: true);
                    //Global.Client.get

                    var mutedRole = Global.Client.GetGuild(375104801018609665).Roles
                        .SingleOrDefault(x => x.Name.ToString() == "Muted");

                    if (randomSocketGuildUser == null) return;
                    await randomSocketGuildUser.AddRoleAsync(mutedRole);
                    // await _channel.SendMessageAsync( $"Мут тебе, бу! {randomSocketGuildUser.Mention}\nПосидишь немного!!");


                    Console.WriteLine("Done");

                    var muteTime = buu.Next(300000, 3600000);

                    await Task.Delay(muteTime);
                    await randomSocketGuildUser.RemoveRoleAsync(mutedRole);
                }
            }
            catch
            {
                //
            }
        }
    }
}