using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OctoBot.Configs.Users
{
    public class AccountSettings
    {
        public string UserName { get; set; }
        public ulong Id { get; set; }
        public int IsModerator { get; set; }
        public string ExtraUserName { get; set; }

        public long Rep { get; set; }
        public string Warnings { get; set; }


        public long Points { get; set; }
        public double Lvl { get; set; }
        public uint LvlPoinnts { get; set; }

        public string Fuckt { get; set; }
        public int OctoPass { get; set; }

        public string Octopuses { get; set; }
        public uint Pinki { get; set; } //розовый айсика
        public uint Cooki { get; set; } // Куки!

        public uint Raqinbow { get; set; }

        public int YellowTries { get; set; }

        public int Lost { get; set; }

        public List<CreateReminder> ReminderList { get; internal set; } = new List<CreateReminder>();

        ///////////DailuPull////////////////
        public DateTime LastDailyPull { get; set; } = DateTime.UtcNow.AddDays(-2);
        public int DailyPullPoints { get; set; }

        public string KeyPullName { get; set; }
        public string KeyPullKey { get; set; }
        public string PullToChoose { get; set; }

        ///////////Subscriptions////////////////
        public string SubToPeople { get; set; }

        public string SubedToYou { get; set; }


        public int Best2048Score { get; set; }

        public ulong BlogVotesQty { get; set; }
        public ulong BlogVotesSum { get; set; }
        public double BlogAvarageScoreVotes { get; set; }


        public ulong ArtVotesQty { get; set; }
        public ulong ArtVotesSum { get; set; }
        public double ArtAvarageScoreVotes { get; set; }

        public DateTime MuteTimer { get; set; }
        public List<CreateVoiceChannel> VoiceChannelList { get; internal set; } = new List<CreateVoiceChannel>();
        public DateTime LastOctoPic { get; set; }
        public string MyPrefix { get; set; }
        public DateTime Birthday { get; set; }
        public string TimeZone { get; set; }

        public ConcurrentDictionary<string, ulong> UserStatistics { get; set; } = new ConcurrentDictionary<string, ulong>();

        public struct CreateReminder
        {
            public DateTime DateToPost;
            public string ReminderMessage;

            public CreateReminder(DateTime dateToPost, string reminderMessage)
            {
                DateToPost = dateToPost;
                ReminderMessage = reminderMessage;
            }
        }

        public struct CreateVoiceChannel
        {
            public DateTime LastTimeLeftChannel;
            public ulong VoiceChannelId;
            public ulong GuildId;

            public CreateVoiceChannel(DateTime lastTimeLeftChannel, ulong voiceChannelId, ulong guildId)
            {
                LastTimeLeftChannel = lastTimeLeftChannel;
                VoiceChannelId = voiceChannelId;
                GuildId = guildId;
            }
        }
    }
}