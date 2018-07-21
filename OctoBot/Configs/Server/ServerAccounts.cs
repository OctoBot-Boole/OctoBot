using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Discord.WebSocket;

namespace OctoBot.Configs.Server
{
    public static class ServerAccounts
    {
        private static readonly List<ServerSettings> Accounts;

        private static readonly string _serverAccountsFile = @"OctoDataBase/ServerAccounts.json";

        static ServerAccounts()
        {
            if (ServerDataStorage.SaveExists(_serverAccountsFile))
            {
                Accounts = ServerDataStorage.LoadServerSettings(_serverAccountsFile).ToList();
            }
            else
            {
                Accounts = new List<ServerSettings>();
                SaveServerAccounts();
            }
        }

        public static void SaveServerAccounts()
        {
            ServerDataStorage.SaveServerSettings(Accounts, _serverAccountsFile);
        }

        public static ServerSettings GetServerAccount(SocketGuild guild)
        {
            return GetOrCreateServerAccount(guild.Id, guild.Name);
        }

        public static ServerSettings GetServerAccount(IGuildChannel guild)
        {
            return GetOrCreateServerAccount(guild.Guild.Id, guild.Guild.Name);
        }

        private static ServerSettings GetOrCreateServerAccount(ulong id, string name)
        {
            var result = from a in Accounts
                where a.ServerId == id
                select a;
            var account = result.FirstOrDefault() ?? CreateServerAccount(id, name);

            return account;
        }


        internal static List<ServerSettings> GetAllServerAccounts()
        {
            return Accounts.ToList();
        }

        internal static List<ServerSettings> GetFilteredServerAccounts(Func<ServerSettings, bool> filter)
        {
            return Accounts.Where(filter).ToList();
        }


        private static ServerSettings CreateServerAccount(ulong id, string name)
        {
            var newAccount = new ServerSettings
            {
                ServerName = name,
                ServerId = id,
                Prefix = "*",
                ServerActivityLog = 0,
                Language = "en"
            };

            Accounts.Add(newAccount);
            SaveServerAccounts();
            return newAccount;
        }
    }
}