﻿using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace OctoBot.Commands
{
    public class ServerManaging : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("purge",  RunMode = RunMode.Async)]
        [Alias("clean", "убрать", "clear")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        public async Task Delete(int number, IGuildUser user = null)
        {
            try
            {
                if (number > 101 || number < 0)
                {
                    await CommandHandeling.ReplyAsync(Context, "Limit 100 messages. You may say `clear 5` or `clear 5 @user`" +
                                                                                          "if you want to delte a users' messages");
                    return;
                }
                    

                var check = Context.User as IGuildUser;
                var comander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (check != null && (comander.OctoPass >= 100 || comander.IsModerator >= 1 ||
                                      check.GuildPermissions.ManageMessages))
                {
                    if (user == null)
                    {
                        var items = Context.Channel.GetCachedMessages(number+1);
                        if (Context.Channel is ITextChannel channel) await channel.DeleteMessagesAsync(items);
                    }
                    else
                    {
                        var items = Context.Channel.GetCachedMessages(300);
                        List<ulong> messagesToDelte = new List<ulong>();
                        var count = 0;
                        var messagesList = items.ToList();

                        for (var i = 0; i < messagesList.Count-1; i++)
                        {
                            if(count == number)
                                continue;
                            if (messagesList[i].Author == user as SocketUser)
                            {
                                messagesToDelte.Add(messagesList[i].Id);
                                count++;
                            }
                        }
                        if(count <= 0)
                            return;
                        if (Context.Channel is ITextChannel channel) await channel.DeleteMessagesAsync(messagesToDelte);
                    }

                    var embed = new EmbedBuilder();
                    embed.WithColor(Color.DarkRed);
                    embed.AddField($"🛡️**PURGE** {number}", $"Used By {Context.User.Mention} in {Context.Channel}")
                        .WithThumbnailUrl(Context.User.GetAvatarUrl())
                        .WithTimestamp(DateTimeOffset.UtcNow);


                    var guild = ServerAccounts.GetServerAccount(Context.Guild);
                    await Context.Guild.GetTextChannel(guild.LogChannelId).SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Boole! You do not have a tolerance of this level!");
                }
            }
            catch
            {
             //   await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **clear [number]**\n" +
             //       "Alias: purge, clean, убрать");
            }
        }

        [Command("warn")]
        [Alias("варн", "предупреждение", "warning")]
        // [RequireUserPermission(GuildPermission.Administrator)]
        public async Task WarnUser(IGuildUser user, [Remainder] string message = null)
        {
            try
            {
                if (message == null)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Boole! You need to specify reason!");
                    return;
                }

                var check = Context.User as IGuildUser;
                var comander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (check != null && (comander.OctoPass >= 100 || comander.IsModerator >= 1 ||
                                      check.GuildPermissions.ManageRoles ||
                                      check.GuildPermissions.ManageMessages))
                {
                    var time = DateTime.Now.ToString("");
                    var account = UserAccounts.GetAccount((SocketUser) user, Context.Guild.Id);
                    account.Warnings += $"{time} {Context.User}: [warn]" + message + "|";
                    UserAccounts.SaveAccounts(Context.Guild.Id);


                    await CommandHandeling.ReplyAsync(Context,
                        user.Mention + " Was Forewarned");


                    var embed = new EmbedBuilder()
                        .WithColor(Color.DarkRed)
                        .AddField("📉 **WARN** used", $"By {Context.User.Mention} in {Context.Channel}\n" +
                                                      $"**Content:**\n" +
                                                      $"{user.Mention} - {message}")
                        .WithThumbnailUrl(Context.User.GetAvatarUrl())
                        .WithTimestamp(DateTimeOffset.UtcNow);

                    var guild = ServerAccounts.GetServerAccount(Context.Guild);
                    await Context.Guild.GetTextChannel(guild.LogChannelId).SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Boole! You do not have a tolerance of this level!");
                }
            }
            catch
            {
             //   await ReplyAsync(
            //        "boo... An error just appear >_< \nTry to use this command properly: **warn [user_ping(or user ID)] [reason_mesasge]**\n" +
            //        "Alias: варн, warning, предупреждение");
            }
        }

        [Command("kick")]
        [Alias("кик")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, [Remainder] string reason = null)
        {
            try
            {
                if (reason == null)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Boole! You need to specify reason!");
                    return;
                }

                await user.KickAsync(reason);
                var time = DateTime.Now.ToString("");
                var account = UserAccounts.GetAccount((SocketUser) user, Context.Guild.Id);
                account.Warnings += $"{time} {Context.User}: [kick]" + reason + "|";
                UserAccounts.SaveAccounts(Context.Guild.Id);
                var embed = new EmbedBuilder()
                    .WithColor(Color.DarkRed)
                    .AddField("🥁 Kick", $"By {Context.User.Mention} in {Context.Channel}\n" +
                                         $"**{user.Mention} Have been kicked**\n" +
                                         $"Reason: {reason}")
                    .WithThumbnailUrl(Context.User.GetAvatarUrl())
                    .WithTimestamp(DateTimeOffset.UtcNow);
                var guild = ServerAccounts.GetServerAccount(Context.Guild);
                await Context.Guild.GetTextChannel(guild.LogChannelId).SendMessageAsync("", false, embed.Build());
            }
            catch
            {
             //   await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **kick [user_ping(or user ID)] [reason_mesasge]**\n" +
             //       "Alias: кик");
            }
        }

        [Command("ban")]
        [Alias("бан")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, string reason = null)
        {
            try
            {
                if (reason == null)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Boole! You need to specify reason!");
                    return;
                }

                await user.Guild.AddBanAsync(user, 0, reason);
                var time = DateTime.Now.ToString("");
                var account = UserAccounts.GetAccount((SocketUser) user, Context.Guild.Id);
                account.Warnings += $"{time} {Context.User}: [ban]" + reason + "|";
                UserAccounts.SaveAccounts(Context.Guild.Id);
                var embed = new EmbedBuilder()
                    .WithColor(Color.DarkRed)
                    .AddField("💥 **ban** used", $"By {Context.User.Mention} in {Context.Channel}\n" +
                                                 $"**Content:**\n" +
                                                 $"{user.Mention} - {reason}")
                    .WithThumbnailUrl(Context.User.GetAvatarUrl())
                    .WithTimestamp(DateTimeOffset.UtcNow);
                var guild = ServerAccounts.GetServerAccount(Context.Guild);
                await Context.Guild.GetTextChannel(guild.LogChannelId).SendMessageAsync("", false, embed.Build());
            }
            catch
            {
             //   await ReplyAsync(
             //       "boo... An error just appear >_< \nTry to use this command properly: **ban [user_ping(or user ID)] [reason_mesasge]**\n" +
            //        "Alias: бан");
            }
        }


        [Command("mute")]
        public async Task MuteCommand(SocketGuildUser user, uint minute, [Remainder] string warningMess = null)
        {
            try
            {
                if (warningMess == null)
                {
                    await CommandHandeling.ReplyAsync(Context,
                        "Boole! You need to specify reason!");
                    return;
                }

                var check = Context.User as IGuildUser;
                var commandre = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
                if (check != null && (commandre.OctoPass >= 100 || commandre.IsModerator > 0 ||
                                      check.GuildPermissions.MuteMembers))
                {
                    var hour = 0;
                    var timeFormat = $"{minute}m";

                    if (minute >= 60)
                        for (var i = 0; minute >= 59; i++)
                        {
                            minute = minute - 59;
                            hour++;

                            timeFormat = $"{hour}h {minute}m";
                        }

                    var timeString = timeFormat; //// MAde t ominutes

                    var timeDateTime = DateTime.UtcNow +
                                       TimeSpan.ParseExact(timeString, ReminderFormat.Formats,
                                           CultureInfo.CurrentCulture);

                    var roleToGive = Context.Guild.Roles
                        .SingleOrDefault(x => x.Name.ToString() == "Muted");


                    await user.ModifyAsync(u => { u.Mute = true; });
                    await user.AddRoleAsync(roleToGive);

                    var account = UserAccounts.GetAccount(user, 0);
                    account.MuteTimer = timeDateTime;
                    var time = DateTime.Now.ToString("");
                    account.Warnings += $"{time} {Context.User}: [mute]" + warningMess + "|";
                    UserAccounts.SaveAccounts(0);


                    await CommandHandeling.ReplyAsync(Context, $"{user.Mention} бу!");
                }
            }
            catch
            {
             //   await ReplyAsync(
            //        "boo... An error just appear >_< \nTry to use this command properly: **mute [user] [time_in_minutes] [Any_text]**\n");
            }
        }

        [Command("unmute")]
        [Alias("umute")]
        public async Task UnMuteCommand(SocketGuildUser user)
        {
            var check = Context.User as IGuildUser;
            var commandre = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            if (check != null && (commandre.OctoPass >= 100 || commandre.IsModerator >= 1 ||
                                  check.GuildPermissions.MuteMembers))
            {
                await user.ModifyAsync(u => u.Mute = false);
                var roleToGive = Global.Client.GetGuild(Context.Guild.Id).Roles
                    .SingleOrDefault(x => x.Name.ToString() == "Muted");
                await user.RemoveRoleAsync(roleToGive);
                var account = UserAccounts.GetAccount(user, 0);
                account.MuteTimer = Convert.ToDateTime("0001-01-01T00:00:00");
                UserAccounts.SaveAccounts(0);


                await CommandHandeling.ReplyAsync(Context, "boole...");
            }
        }

        [Command("moderator")]
        [Alias("moder")]
        public async Task SetModerator(SocketGuildUser user)
        {
            var check = Context.User as IGuildUser;
            var commander = UserAccounts.GetAccount(Context.User, Context.Guild.Id);
            if (check != null && (Context.Guild.Owner.Id == Context.User.Id || commander.IsModerator >= 2 ||
                                  check.GuildPermissions.ManageRoles))
            {
                var account = UserAccounts.GetAccount(user, Context.Guild.Id);
                account.IsModerator = 1;
                UserAccounts.SaveAccounts(Context.Guild.Id);


                await CommandHandeling.ReplyAsync(Context,
                    $"{user.Mention} is now a moderator! Booole~");
            }
        }
    }
}