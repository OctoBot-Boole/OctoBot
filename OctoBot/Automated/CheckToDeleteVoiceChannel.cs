using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using OctoBot.Configs;
using OctoBot.Configs.Users;

namespace OctoBot.Automated
{
    public class CheckToDeleteVoiceChannel
    {
        private static Timer _loopingTimer;

        internal static Task CheckTimer()
        {
            _loopingTimer = new Timer
            {
                AutoReset = true,
                Interval = 30000,
                Enabled = true
            };
            _loopingTimer.Elapsed += CheckToDeleteVoice;    
            return Task.CompletedTask;
        }

        private static void CheckToDeleteVoice(object sender, ElapsedEventArgs e)
        {
            var allUserAccounts = UserAccounts.GetOrAddUserAccountsForGuild(0);
            foreach (var t in allUserAccounts)
                try
                {
                    var globalAccount = Global.Client.GetUser(t.Id);
                    var account = UserAccounts.GetAccount(globalAccount, 0);
                    if (account.VoiceChannelList.Count <= 0)
                        continue;

                    var difference = DateTime.UtcNow - account.VoiceChannelList[0].LastTimeLeftChannel;

                    if (difference.Minutes > 10)
                    {
                        var voiceChan = Global.Client.GetGuild(account.VoiceChannelList[0].GuildId)
                            .GetVoiceChannel(account.VoiceChannelList[0].VoiceChannelId);

                        account.VoiceChannelList = new List<AccountSettings.CreateVoiceChannel>();
                        UserAccounts.SaveAccounts(0);

                        if (voiceChan.Users.Count <= 0)
                        {
                            voiceChan.DeleteAsync();
                        }
                        else if (voiceChan.Users.Count >= 1)
                        {
                            var usersList = voiceChan.Users.ToList();
                            var newHolder = UserAccounts.GetAccount(usersList[0], 0);

                            var newVoice = new AccountSettings.CreateVoiceChannel(DateTime.UtcNow.AddHours(10),
                                voiceChan.Id, voiceChan.Guild.Id);
                            newHolder.VoiceChannelList.Add(newVoice);
                            UserAccounts.SaveAccounts(0);
                            var guildUser = Global.Client.GetGuild(newHolder.VoiceChannelList[0].GuildId)
                                .GetUser(newHolder.Id);
                            var k = voiceChan.AddPermissionOverwriteAsync(guildUser,
                                OverwritePermissions.AllowAll(voiceChan),
                                RequestOptions.Default);
                            var kk = voiceChan.RemovePermissionOverwriteAsync(globalAccount, RequestOptions.Default);
                        }
                    }
                }
                catch
                {
                    //
                }
        }
    }
}