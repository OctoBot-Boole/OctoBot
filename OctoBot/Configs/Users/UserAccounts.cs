using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Discord;
using OctoBot.Configs.Server;

namespace OctoBot.Configs.Users
{
    public static class UserAccounts
    {
        private static readonly ConcurrentDictionary<ulong, List<AccountSettings>> UserAccountsDictionary =
            new ConcurrentDictionary<ulong, List<AccountSettings>>();


        static UserAccounts()
        {
            var guildList = ServerAccounts.GetAllServerAccounts();
            foreach (var guild in guildList)
                UserAccountsDictionary.GetOrAdd(guild.ServerId,
                    x => DataStorage.LoadAccountSettings(guild.ServerId).ToList());
        }

        public static List<AccountSettings> GetOrAddUserAccountsForGuild(ulong guildId)
        {
            return UserAccountsDictionary.GetOrAdd(guildId, x => DataStorage.LoadAccountSettings(guildId).ToList());
        }

        public static AccountSettings GetAccount(IUser user, ulong guildId)
        {
            return GetOrCreateAccount(user, guildId);
        }


        private static AccountSettings GetOrCreateAccount(IUser user, ulong guildId)
        {
            var accounts = GetOrAddUserAccountsForGuild(guildId);

            var result = from a in accounts
                where a.Id == user.Id
                select a;

            var account = result.FirstOrDefault();
            if (account == null)
                account = CreateUserAccount(user, guildId);

            return account;
        }


        public static void SaveAccounts(ulong guildId)
        {
            var accounts = GetOrAddUserAccountsForGuild(guildId);
            DataStorage.SaveAccountSettings(accounts, guildId);
        }

        internal static List<AccountSettings> GetAllAccountForAllGuild()
        {
            var accounts = new List<AccountSettings>();
            foreach (var values in UserAccountsDictionary.Values) accounts.AddRange(values);
            return accounts;
        }


        internal static List<AccountSettings> GetFilteredAccounts(Func<AccountSettings, bool> filter, ulong guildId)
        {
            var accounts = GetOrAddUserAccountsForGuild(guildId);
            return accounts.Where(filter).ToList();
        }


        private static AccountSettings CreateUserAccount(IUser user, ulong guildId)
        {
            var accounts = GetOrAddUserAccountsForGuild(guildId);

            var newAccount = new AccountSettings
            {
                Id = user.Id,
                UserName = user.Username
            };

            accounts.Add(newAccount);
            SaveAccounts(guildId);
            return newAccount;
        }
    }
}