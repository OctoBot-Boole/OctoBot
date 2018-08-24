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
        private readonly SecureRandom _secureRandom;

        public TimerForChangingAvatar(SecureRandom secureRandom)
        {
            _secureRandom = secureRandom;
        }

        private  Timer _loopingTimerForOctoAva;

        internal  Task TimerForChangeBotAvatar()
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

        public async void SetBotAva(object sender, ElapsedEventArgs e)
        {
            try
            {
                var octoIndex = OctoPicPull.OctoPics.Length - 1;
                var randomIndex = _secureRandom.Random(0, octoIndex);
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