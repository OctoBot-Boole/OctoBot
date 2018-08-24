using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;

namespace OctoBot.CustomForServers
{
    internal static class ColorRoleReaction
    {
        public static async Task ReactionAddedForRole(Cacheable<IUserMessage, ulong> cash,
            ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if (reaction.MessageId == 445501974608216064)
                {
                    if (reaction.User.Value.IsBot)
                        return;
                    var globalAccount = Global.Client.GetUser(reaction.UserId);
                    switch (reaction.Emote.Name)
                    {
                        case "rem":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Weeb");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Weeb"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                await cash.GetOrDownloadAsync().Result
                                    .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🦊":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Fox");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Fox"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "PeaceKeepo":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Deus Vult");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Deus Vult"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "Steampunk":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Steampunk");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Steampunk"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "praise":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Слуги солнца");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Слуги солнца"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "monkaS":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Meme-boy");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Meme-boy"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🐲":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Dragon");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Dragon"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🐼":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Panda");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Panda"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🦎":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Lizards");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Lizards"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🌑":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Shadow");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Shadow"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "mumu":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Nazrin");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Nazrin"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "🐱":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Cat");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Cat"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);


                            break;
                        }
                        case "pekaohmy":
                        {
                            var guildUser =
                                Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId) as IGuildUser;
                            if(!guildUser.GuildPermissions.ManageMessages && !guildUser.GuildPermissions.ManageMessages && !guildUser.GuildPermissions.MuteMembers)
                                return;
                            var k = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 3, globalAccount);
                            break;
                        }
                        default:
                            return;
                    }

                    var kk = RoomRoleReaction.RemoveReactions(cash, channel, reaction, 1, globalAccount);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Reaction for Roles not workind. '{0}'", error);
            }
        }
    }
}