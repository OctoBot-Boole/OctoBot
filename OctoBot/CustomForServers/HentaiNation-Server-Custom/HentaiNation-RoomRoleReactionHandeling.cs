using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;

namespace OctoBot.CustomForServers
{
    internal static class RoomRoleReactionHandeling
    {
        public static async Task RemoveReactions(Cacheable<IUserMessage, ulong> cash,
            ISocketMessageChannel channel, SocketReaction reaction, int editCheck, SocketUser globalAccount)
        {
            try
            {
                if (editCheck == 1)
                {
                    await cash.GetOrDownloadAsync().Result
                        .RemoveReactionAsync(reaction.Emote, globalAccount, RequestOptions.Default);
                }
                else if (editCheck == 2)
                {
                    var pedoFedora = Emote.Parse("<:PedoFedora:396676199738507265>");
                    var gasm = Emote.Parse("<:Gasm:396672726783361034>");
                    var kappaPride = Emote.Parse("<:KappaPride:396676035715792896>");
                    var yuiStop = Emote.Parse("<:YuiStop:398278458783825920>");
                    var realy = Emote.Parse("<:realy:397035718913818624>");
                    var henlo = Emote.Parse("<:henlo:396773624909922316>");
                    var aworry = Emote.Parse("<:aworry:396773971959218185>");
                    var pekaPled = Emote.Parse("<:PekaPled:398276512119390208>");
                    var woah = Emote.Parse("<:Woah:396773773644267532>");
                    var angery = Emote.Parse("<:Angery:398280630116548623>");
                    var feelsBadMan = Emote.Parse("<:FeelsBadMan:397035718901366786>");
                    var mumu = Emote.Parse("<:mumu:396672675977756674>");
                    var padoru = Emote.Parse("<:PADORU:399269451448713238>");
                    var oSsloth = Emote.Parse("<:OSsloth:398276510831869952>");
                    var takai = Emote.Parse("<:Takai:406938709058125825>");
                    var pekaApple = Emote.Parse("<:pekaApple:402174155954585601>");


                    await cash.GetOrDownloadAsync().Result.RemoveAllReactionsAsync();
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(pedoFedora);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(gasm);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(kappaPride);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(yuiStop);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(realy);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(henlo);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(aworry);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(pekaPled);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(woah);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(angery);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(feelsBadMan);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(mumu);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(padoru);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(oSsloth);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(takai);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(pekaApple);
                }
            }
            catch
            {
                //
            }
        }

        public static async Task ReactionAddedForRole(Cacheable<IUserMessage, ulong> cash,
            ISocketMessageChannel channel, SocketReaction reaction)
        {
            try
            {
                if (reaction.MessageId == 463053512620900362)
                {
                    if (reaction.User.Value.IsBot)
                        return;
                    var globalAccount = Global.Client.GetUser(reaction.UserId);
                    switch (reaction.Emote.Name)
                    {
                        case "PedoFedora":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Любитель маленьких девочек");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Любитель маленьких девочек"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "Gasm":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Цветок лилии");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Цветок лилии"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "KappaPride":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Гачи");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Гачи"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "YuiStop":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Небольшой нюанс");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Небольшой нюанс"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "realy":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Весь в коже");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Весь в коже"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "henlo":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Кошколюб");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Кошколюб"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "aworry":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Люблю всякое говно");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Люблю всякое говно"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "PekaPled":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Любитель Китайских Фигурок");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Любитель Китайских Фигурок"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "Woah":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Торговец экзотикой");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Торговец экзотикой"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "Angery":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Фуррифаг");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Фуррифаг"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "FeelsBadMan":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Ловушка");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Ловушка"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "mumu":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Тохоёб");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Тохоёб"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "PADORU":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Фейтодрочер");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Фейтодрочер"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "OSsloth":
                        {
                            var guildUser = Global.Client.GetGuild(396590804984135680).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(396590804984135680).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "ЛовДрочер");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "ЛовДрочер"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);

                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "pekaApple" when reaction.UserId == 181514288278536193:
                        {
                            var k = RemoveReactions(cash, channel, reaction, 2, globalAccount);
                            break;
                        }
                        default:
                            var kk = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                            return;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Reaction for Roles not workind. '{0}'", error);
            }
        }
    }
}