using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Server;
using OctoBot.Configs.Users;
using OctoBot.Custom_Library;
using OctoBot.Handeling;
using OctoBot.Helper;

namespace OctoBot.Commands
{
    public class ServerSetup : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("build")]
        [RequireOwner]
        public async Task BuildExistingServer()
        {
            var guild = Global.Client.Guilds.ToList();
            foreach (var t in guild) ServerAccounts.GetServerAccount(t);

            await CommandHandeling.ReplyAsync(Context, "Севера бобавлены, бууууль!");
        }

        [Command("prefix")]
        public async Task CheckPrefix()
        {
            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            await CommandHandeling.ReplyAsync(Context, $"boole: `{guild.Prefix}`");
        }

        [Command("setPrefix")]
        [Alias("setpref")]
        [Description("Setuo prefix for currrent Guild")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task SetPrefix([Remainder] string prefix)
        {
            try
            {
                if (prefix.Length >= 5)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        $"boole!! Please choose prefix using up to 4 characters");

                    return;
                }

                var guild = ServerAccounts.GetServerAccount(Context.Guild);
                guild.Prefix = prefix;
                ServerAccounts.SaveServerAccounts();

                await CommandHandeling.ReplyAsync(Context,
                    $"boole is now: `{guild.Prefix}`");
            }
            catch
            {
                //
            }
        }

        [Command("offLog")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetServerActivivtyLogOff()
        {
            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            guild.LogChannelId = 0;
            guild.ServerActivityLog = 0;
            ServerAccounts.SaveServerAccounts();

            await CommandHandeling.ReplyAsync(Context, $"Boole.");
        }

        [Command("SetLog")]
        [Alias("SetLogs")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetServerActivivtyLog(IGuildChannel logChannel = null)
        {
            var guild = ServerAccounts.GetServerAccount(Context.Guild);

            if (logChannel != null)
            {
                try
                {
                    var channel = logChannel;
                    if ((channel as ITextChannel) == null)
                    {
                        await CommandHandeling.ReplyAsync(Context,
                            $"Booole >_< **an error** Maybe I am not an Administrator of this server? I need this permission to access audit, manage channel, emojis and users.");
                        return;
                    }
                       

                    guild.LogChannelId = channel.Id;
                    guild.ServerActivityLog = 1;
                    ServerAccounts.SaveServerAccounts();

                    var text2 =
                        $"Boole! Now we log everything to {((ITextChannel) channel).Mention}, you may rename and move it.\n";
                       // $"Btw, you may say `editIgnore 5` and we we will ignore the message where only 5 **characters** have been changed. This will reduce the number of non-spurious logs (you may say any number)";

                    await CommandHandeling.ReplyAsync(Context, text2);
                }
                catch
                {
                 //   await CommandHandeling.ReplyAsync(Context,
                  //      $"Booole >_< **an error** Maybe I am not an Administrator of this server? I need this permission to access audit, manage channel, emojis and users.");
                }

                return;
            }

            switch (guild.ServerActivityLog)
            {
                case 1:
                    guild.ServerActivityLog = 0;
                    guild.LogChannelId = 0;
                    ServerAccounts.SaveServerAccounts();


                    await CommandHandeling.ReplyAsync(Context,
                        $"Octopuses are not logging any activity now **:c**\n");

                    return;
                case 0:
                    try
                    {
                        try
                        {
                            var tryChannel = Context.Guild.GetTextChannel(guild.LogChannelId);
                            if (tryChannel.Name != null)
                            {
                                guild.LogChannelId = tryChannel.Id;
                                guild.ServerActivityLog = 1;
                                ServerAccounts.SaveServerAccounts();

                                var text2 =
                                    $"Boole! Now we log everything to {tryChannel.Mention}, you may rename and move it.";

                                await CommandHandeling.ReplyAsync(Context, text2);
                            }
                        }
                        catch
                        {
                            var channel = Context.Guild.CreateTextChannelAsync("OctoLogs");
                            guild.LogChannelId = channel.Result.Id;
                            guild.ServerActivityLog = 1;
                            ServerAccounts.SaveServerAccounts();

                            var text =
                                $"Boole! Now we log everything to {channel.Result.Mention}, you may rename and move it.";

                            await CommandHandeling.ReplyAsync(Context, text);
                        }
                    }
                    catch
                    {
                     //   await CommandHandeling.ReplyAsync(Context,
                     //       $"Booole >_< **an error** Maybe I am not an Administrator of this server? I need this permission to access audit, manage channel, emojis and users.");
                    }

                    break;
            }
        }

        [Command("SetLanguage")]
        [Alias("setlang")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [Description("Setting language for this guild")]
        public async Task SetLanguage(string lang)
        {
            if (lang.ToLower() != "en" && lang.ToLower() != "ru")
            {
                await CommandHandeling.ReplyAsync(Context,
                    $"boole! only available options for now: `en`(default) and `ru`");

                return;
            }

            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            guild.Language = lang.ToLower();
            ServerAccounts.SaveServerAccounts();

            await CommandHandeling.ReplyAsync(Context,
                $"boole~ language is now: `{lang.ToLower()}`");
        }

        [Command("SetRoleOnJoin")]
        [Alias("RoleOnJoin")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task SetRoleOnJoin(string role = null)
        {
            string text;
            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            if (role == null)
            {
                guild.RoleOnJoin = null;
                text = $"boole... No one will get role on join from me!";
            }
            else
            {
                guild.RoleOnJoin = role;
                text = $"boole. Everyone will now be getting {role} role on join!";
            }

            ServerAccounts.SaveServerAccounts();

            await CommandHandeling.ReplyAsync(Context, text);
        }

        [Command("chanInfo")]
        [Alias("ci")]
        [Description("Showing usefull Server's Channels Statistics")]
        public async Task ChannelInfo(ulong guildId)
        {
            var channels = Global.Client.GetGuild(guildId).TextChannels.ToList();
            var text = "";
            var text2 = "";
            for (var i = 0; i < channels.Count; i++)
                if (text.Length <= 900)
                    text += $"{i + 1}) {channels[i].Name} - {channels[i].Users.Count}\n";
                else
                    text2 += $"{i + 1}) {channels[i].Name} - {channels[i].Users.Count}\n";

            var embed = new EmbedBuilder();
            embed.AddField($"{Global.Client.GetGuild(guildId).Name} Channel Info", $"{text}");
            if (text2.Length > 1) embed.AddField($"Continued", $"{text2}");


            await ReplyAsync("", false, embed.Build());
            await Task.CompletedTask;
        }

        [Command("role")]
        [Description("Giving any available role to any user in this guild")]
        public async Task TeninzRole(SocketUser user, string role)
        {
            var check = Context.User as IGuildUser;
            var comander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            if (check != null && (comander.OctoPass >= 100 || comander.IsModerator >= 1 ||
                                  check.GuildPermissions.ManageRoles ||
                                  check.GuildPermissions.ManageMessages))
            {
                var guildUser = Global.Client.GetGuild(Context.Guild.Id).GetUser(user.Id);
                var roleToGive = Global.Client.GetGuild(Context.Guild.Id).Roles
                    .SingleOrDefault(x => x.Name.ToString() == role);

                var roleList = guildUser.Roles.ToArray();
                if (roleList.Any(t => t.Name == role))
                {
                    await guildUser.RemoveRoleAsync(roleToGive);
                    await CommandHandeling.ReplyAsync(Context, "Буль!");
                    return;
                }

                await guildUser.AddRoleAsync(roleToGive);
                await CommandHandeling.ReplyAsync(Context, "Буль?");
            }
        }


        [Command("add", RunMode = RunMode.Async)]
        public async Task AddCustomRoleToBotList(string command, [Remainder] string role)
        {
            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            var check = Context.User as IGuildUser;

            var comander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            if (check != null && (comander.OctoPass >= 100 || comander.IsModerator >= 1 ||
                                  check.GuildPermissions.ManageRoles ||
                                  check.GuildPermissions.ManageMessages))
            {

                var serverRolesList = Context.Guild.Roles.ToList();
                var ifSuccsess = false;
                for (var i = 0; i < serverRolesList.Count; i++)
                {
                    if (!string.Equals(serverRolesList[i].Name, role, StringComparison.CurrentCultureIgnoreCase) || ifSuccsess)
                        continue;
                    var i1 = i;
                    guild.Roles.AddOrUpdate(command, serverRolesList[i].Name, (key, value) => serverRolesList[i1].Name);
                    ServerAccounts.SaveServerAccounts();
                    ifSuccsess = true;
                }

                if (ifSuccsess)
                {
                    var embed = new EmbedBuilder();
                    embed.AddField("New Role Command Added To The List:", "Boole!\n" +
                                                                          $"`{guild.Prefix}{command}` will give the user `{role}` Role\n" +
                                                                          ".\n" +
                                                                          "**_____**\n" +
                                                                          "`ar` - see all Role Commands\n" +
                                                                          $"`dr {command}` - remove the command from the list" +
                                                                          "Btw, you can say **role @user role_name** as well.");
                    embed.WithFooter("Tip: Simply edit the previous message instead of writing a new command");
                    await CommandHandeling.ReplyAsync(Context, embed);
                }
                else
                {
                    await CommandHandeling.ReplyAsync(Context, "Error.\n" +
                                                                                          "Example: `add KeyName RoleName` where **KeyName** anything you want(even emoji), and **RoleName** is a role, you want to get by using `*KeyName`\n" +
                                                                                          "You can type **RoleName** all lowercase\n\n" +
                                                                                          "Saying `*KeyName` you will get **RoleName** role.");
                }
            }
        }

        [Command("r")]
        public async Task AddCustomRoleToUser([Remainder] string role)
        {
            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            var guildRoleList = guild.Roles.ToArray();
            SocketRole roleToAdd = null;
            if (guildRoleList.Any(x => x.Key == role))
                foreach (var t in guildRoleList)
                    if (t.Key == role)
                        roleToAdd = Context.Guild.Roles.SingleOrDefault(x => x.Name.ToString() == t.Value);
            else
                return;


            if (!(Context.User is SocketGuildUser guildUser) || roleToAdd == null)
                return;

            var roleList = guildUser.Roles.ToArray();

            if (roleList.Any(t => t.Name == roleToAdd.Name))
            {
                await guildUser.RemoveRoleAsync(roleToAdd);
                await CommandHandeling.ReplyAsync(Context, $"-{roleToAdd.Name}");
                return;
            }

            await guildUser.AddRoleAsync(roleToAdd);
            await CommandHandeling.ReplyAsync(Context, $"+{roleToAdd.Name}");
        }

        [Command("ar")]
        public async Task AllCustomRoles()
        {
            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            var rolesList = guild.Roles.ToList();
            var embed = new EmbedBuilder();
            embed.WithColor(SecureRandomStatic.Random(254), SecureRandomStatic.Random(254), SecureRandomStatic.Random(254));
            embed.WithAuthor(Context.User);
            var text = "";
            foreach (var t in rolesList) text += $"{t.Key} - {t.Value}\n";

            embed.AddField("All Custom Commands To Get Roles:", $"Format: **KeyName - RoleName**\n" +
                                                $"By Saying `{guild.Prefix}KeyName` you will get **RoleName** role.\n" +
                                                $"**______**\n\n" +
                                                $"{text}\n");
            embed.WithFooter("Say **dr KeyName** to delete the Command from the list");

            await CommandHandeling.ReplyAsync(Context, embed);
        }

        [Command("dr")]
        [Alias("rr")]
        [RequireUserPermission(ChannelPermission.ManageRoles)]
        public async Task DeleteCustomRoles(string role)
        {
            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            var embed = new EmbedBuilder();
            embed.WithColor(SecureRandomStatic.Random(254), SecureRandomStatic.Random(254), SecureRandomStatic.Random(254));
            embed.WithAuthor(Context.User);


            var test = guild.Roles.TryRemove(role, out role);

            var text = test ? $"{role} Removed" : "error";
            embed.AddField("Delete Custom Role:", $"{text}");

            await CommandHandeling.ReplyAsync(Context, embed);
        }

        [Command("editIgnore")]
        public async Task LoggingMessEditIgnore(int ignore)
        {
            if (ignore < 0 || ignore > 2000)
            {
                await CommandHandeling.ReplyAsync(Context, "limit 0-2000");
                return;
            }
            var guild = ServerAccounts.GetServerAccount(Context.Guild);
            guild.LoggingMessEditIgnoreChar = ignore;
            ServerAccounts.SaveServerAccounts();
            await CommandHandeling.ReplyAsync(Context, $"Boole? From now on we will ignore {ignore} characters for logging **Message Edit**\n" +
                                                       "Hint: Space is 1 char, an this: `1` is 3 characters (special formatting characters counts as well)");
        }
    }
}