using System.Collections.Concurrent;

namespace OctoBot.Configs.Server
{
    public class ServerSettings
    {
        public string ServerName { get; set; }
        public ulong ServerId { get; set; }
        public string Prefix { get; set; }
        public string Language { get; set; }
        public int ServerActivityLog { get; set; }
        public ulong LogChannelId { get; set; }
        public string RoleOnJoin { get; set; }
        public ulong MessagesReceivedAll { get; set; }

        public ConcurrentDictionary<string, ulong> MessagesReceivedStatisctic { get; set; } =
            new ConcurrentDictionary<string, ulong>();

        public ConcurrentDictionary<string, string> Roles { get; set; } = new ConcurrentDictionary<string, string>();
    }
}