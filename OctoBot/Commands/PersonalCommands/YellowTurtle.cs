using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;

namespace OctoBot.Commands.PersonalCommands
{
    //Yellow Turtle Event ( Done)
    public class YellowTurtle : ModuleBase<ShardedCommandContextCustom>
    {
        private static SocketTextChannel _channel;
        private static Timer _loopingTimer;


        [Command("YellowEvent")]
        public async Task Teeeest()
        {
            await StartTimer25();

            Task StartTimer25()
            {
                _loopingTimer = new Timer
                {
                    AutoReset = false,
                    Interval = 5000,
                    Enabled = true
                };
                _loopingTimer.Elapsed += YellowTurtleEvent;
                // _loopingTimer.Elapsed += Reminder.CheckAllReminders;
                return Task.CompletedTask;
            }

            await Task.CompletedTask;
        }


        [Command("нашёл")]
        public async Task FoundTurtle()
        {
            if (Global.CommandEnabled != 1)
                return;

            var account = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            if (Context.Channel.Id == Global.YellowTurlteChannelId && account.YellowTries != 2)
            {
                Global.CommandEnabled = 0;
                await Context.Channel.SendMessageAsync("Экстренная помощь черепашке!");

                var embed = new EmbedBuilder();
                embed.WithImageUrl("https://i.imgur.com/HQWbEbC.jpg");
                await Context.Channel.SendMessageAsync("", false, embed.Build());

                await Task.Delay(50000);
                await Context.Channel.SendMessageAsync(
                    $"Спасибо тебе {Context.User.Mention} за помощь!\nВ качестве вознагрождение тебе будет выплчен грант в размере **100** репутации!");


                account.Rep += 100;
                UserAccounts.SaveAccounts(Context.Guild.Id);

                var embedtwo = new EmbedBuilder();
                embedtwo.WithImageUrl("https://i.imgur.com/c4x1Cgw.jpg");
                await Context.Channel.SendMessageAsync("", false, embedtwo.Build());
            }
            else if (account.YellowTries == 1)
            {
                var embed = new EmbedBuilder();
                embed.WithDescription(
                    "Мы проверили твою информацию, тут нет черепашки! **Ты только тратишь наше время!**\n**Мы тебе больше не доверяем!**");
                embed.WithImageUrl("https://i.imgur.com/VihPt87.png");
                await Context.Channel.SendMessageAsync("", false, embed.Build());

                account.YellowTries += 1;
                UserAccounts.SaveAccounts(Context.Guild.Id);
            }
            else if (account.YellowTries >= 2)
            {
                var embed = new EmbedBuilder();
                embed.WithDescription("**Мы тебе больше не доверяем!**");
                embed.WithImageUrl(
                    "https://media.discordapp.net/attachments/239546250800660490/436362901880700939/20180418_231015.jpg?width=1248&height=702");
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                var embed = new EmbedBuilder();
                embed.WithDescription(
                    "Мы проверили твою информацию, тут нет черепашки! **Ты только тратишь наше время!**\nЕсли вызовишь нас еще раз просто так, мы больше не будем тебе доверять!");
                embed.WithImageUrl("https://i.imgur.com/VihPt87.png");
                await Context.Channel.SendMessageAsync("", false, embed.Build());

                account.YellowTries += 1;
                UserAccounts.SaveAccounts(Context.Guild.Id);
            }
        }


        internal static Task StartTimer()
        {
            _loopingTimer = new Timer
            {
                AutoReset = false,
                Interval = 5000,
                Enabled = true
            };
            _loopingTimer.Elapsed += YellowTurtleEvent;


            return Task.CompletedTask;
        }

        public static async void YellowTurtleEvent(object sender, ElapsedEventArgs e)
        {
            /* HAEM-HAEM
            var buu = new Random();
            var Event = buu.Next(10000);
            */
            //Starts Yellow Turtle event//

            Global.CommandEnabled = 0;
            var channelList =
                Global.Client.GetGuild(375104801018609665).TextChannels.ToList(); // change ulong to a random CLienGuild


            var ev = new Random();
            var index = ev.Next(channelList.Count);
            _channel = Global.Client.GetGuild(375104801018609665)
                .GetTextChannel(channelList[index].Id); // change ulong to a random CLienGuild

            var message = await _channel.SendMessageAsync("буль.");


            Global.YellowTurlteMessageTorack = message;


            Global.YellowTurlteChannelId = channelList[index].Id;
            await StartTimer2();


            Task StartTimer2()
            {
                _loopingTimer = new Timer
                {
                    AutoReset = false,
                    Interval = 10000,
                    Enabled = true
                };
                _loopingTimer.Elapsed += Anounce;
                return Task.CompletedTask;
            }
        }

        private static async void Anounce(object sender, ElapsedEventArgs e)
        {
            _channel = Global.Client.GetGuild(375104801018609665).GetTextChannel(375104801018609667);

            await _channel.SendMessageAsync(
                "О нет! Желтая черепашка опять потерялась!\nОна забыла колокольчик, как мы теперь ее найдём!?\n " +
                "Если Увидете её, напишите `*нашёл`, и мы придем перепроверить.\n" +
                "**Только не нужно вызывать нас просто так!**");
            var embed = new EmbedBuilder();
            embed.WithColor(Color.Gold);
            embed.WithImageUrl("https://i.imgur.com/YQt5WLs.jpg");
            await _channel.SendMessageAsync("", false, embed.Build());

            await StartTimer3();

            Task StartTimer3()
            {
                _loopingTimer = new Timer
                {
                    AutoReset = false,
                    Interval = 15000,
                    Enabled = true
                };
                _loopingTimer.Elapsed += YellowTurtleIn;
                return Task.CompletedTask;
            }
        }

        private static async void YellowTurtleIn(object sender, ElapsedEventArgs e)
        {
            _channel = Global.Client.GetGuild(375104801018609665).GetTextChannel(375104801018609667);
            await _channel.SendMessageAsync(
                "Прошел слух, что кто-то заметил черепашку на этом сервере!\nПредлагаю проверить каналы в поисках следов!");

            Global.CommandEnabled = 1;
            var builder = new StringBuilder();
            builder.Append("https://i.imgur.com/ESaQFLM.jpg");
            Console.WriteLine($"/n{Global.CommandEnabled}/n");
            await Global.YellowTurlteMessageTorack.ModifyAsync(m => m.Content = builder.ToString());
        }
    }
}