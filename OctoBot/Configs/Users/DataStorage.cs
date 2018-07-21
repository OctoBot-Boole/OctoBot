using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OctoBot.Configs.Users
{
    public static class DataStorage
    {
        //Save all AccountSettings

        public static void SaveAccountSettings(IEnumerable<AccountSettings> accounts, string idString, string json)
        {
            try
            {
                var filePath = $@"OctoDataBase/GuildAccounts/account-{idString}.json";
                File.WriteAllText(filePath, json);
            }
            catch
            {
                Console.WriteLine("Failed To ReadFile(SaveAccountSettings). Will ty in 5 sec.");
            }
        }


        public static void SaveAccountSettings(IEnumerable<AccountSettings> accounts, ulong guildId)
        {
            try
            {
                var filePath = $@"OctoDataBase/GuildAccounts/account-{guildId}.json";

                var json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch
            {
                Console.WriteLine("Failed To ReadFile(SaveAccountSettings). Will ty in 5 sec.");
            }
        }

        //Get AccountSettings

        public static IEnumerable<AccountSettings> LoadAccountSettings(ulong guildId)
        {
            var filePath = $@"OctoDataBase/GuildAccounts/account-{guildId}.json";
            if (!File.Exists(filePath))
            {
                var newList = new List<AccountSettings>();
                SaveAccountSettings(newList, guildId);
                return newList;
            }

            var json = File.ReadAllText(filePath);

            try
            {
                return JsonConvert.DeserializeObject<List<AccountSettings>>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine($"LoadAccountSettings TRY_CATCH: {e}");
                var newList = new List<AccountSettings>();
                SaveAccountSettings(newList, $"{guildId}-BACK_UP", json);
                return newList;
            }
        }

        public static bool SaveExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}