using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using OctoBot.Configs;
using OctoBot.Handeling;
using OctoBot.Helper;

namespace OctoBot.Automated
{
    public class TimerForChangingAvatar
    {
        private static Timer _loopingTimerForOctoAva;

        internal static Task TimerForChangeBotAvatar()
        {
            _loopingTimerForOctoAva = new Timer
            {
                AutoReset = true,
                Interval = 3600000,
                Enabled = true
            };
            _loopingTimerForOctoAva.Elapsed += SetBotAva;

            return Task.CompletedTask;
        }

        public static async void SetBotAva(object sender, ElapsedEventArgs e)
        {
            try
            {
                var randomIndex = SecureRandom.Random(0, OctoPicPull.OctoPics.Length-1);
                var octoToPost = OctoPicPull.OctoPics[randomIndex];

                var webClient = new WebClient();
                var imageBytes = webClient.DownloadData(octoToPost);

                var stream = new MemoryStream(imageBytes);

                var image = new Image(stream);
                await Global.Client.CurrentUser.ModifyAsync(k => k.Avatar = image);
            }
            catch (Exception ex)
            {
                ConsoleLogger.Log($"[Exception] (Change avatar) - {ex.Message}",
                    ConsoleColor.DarkBlue);
            }
        }
    }
}