using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;

namespace OctoBot.CustomForServers
{
    internal static class RoomRoleReaction
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
                    var rkn = Emote.Parse("<:RKN:445325930022436874>");
                    var realy = Emote.Parse("<:realy:374655750657540106>");
                    var feelsBadMan = Emote.Parse("<:FeelsBadMan:374655964843868162>");
                    var woahMorfin = Emote.Parse("<:haHAA:463392036163289099>");
                    var thonk = Emote.Parse("<:thonk:445324435403309087>");
                    var akaShrug = Emote.Parse("<:AkaShrug:374802737596071936>");
                    var such = Emote.Parse("<:such:445322074781908993>");
                    var kannNom = Emote.Parse("<:GWnanamiKannaNom:445321264169746434>");
                    var pogChamp = Emote.Parse("<:PogChamp:374656108117098517>");
                    var hanzo = Emote.Parse("<:hanzo:445324859690582018>");

                    var yasuo = Emote.Parse("<:yasuo:445323301137547264>");
                    var gacHiPride = Emote.Parse("<:gacHIPride:394782921749430273>");
                    var pekaohmy = Emote.Parse("<:pekaohmy:374656330742497280>");
                    var warframe = Emote.Parse("<:warframe:445467639242948618>");
                    var nintendoswitch = Emote.Parse("<:nintendoswitch:447209808064413707>");
                    var fps = Emote.Parse("<:fuckyeah:430853466408353792>");
                    var sir = Emote.Parse("<:sir:430853466110427137>");

                    await cash.GetOrDownloadAsync().Result.RemoveAllReactionsAsync();
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(rkn);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(realy);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(feelsBadMan);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(woahMorfin);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(thonk);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(akaShrug);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(such);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(kannNom);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(pogChamp);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(hanzo);

                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(yasuo);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(gacHiPride);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(warframe);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(nintendoswitch);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(fps);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(pekaohmy);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(sir);
                }
                else if (editCheck == 3)
                {
                    var peaceKeepo = Emote.Parse("<:PeaceKeepo:438257037667729408>");
                    var praise = Emote.Parse("<:praise:445274481917952001>");
                    var rem = Emote.Parse("<:rem:445275743719522304>");
                    var steampunk = Emote.Parse("<:Steampunk:445276776676196353>");
                    var mumu = Emote.Parse("<:mumu:445277916872310785>");
                    var monkaS = Emote.Parse("<:monkaS:398183436613058570>");
                    var pekaohmy = Emote.Parse("<:pekaohmy:374656330742497280>");

                    await cash.GetOrDownloadAsync().Result.RemoveAllReactionsAsync();
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(rem);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("🦊"));
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(peaceKeepo);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(steampunk);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(praise);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(monkaS);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("🐲"));
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("🐼"));
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("🦎"));
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("🌑"));
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(mumu);
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("🐱"));
                    await cash.GetOrDownloadAsync().Result.AddReactionAsync(pekaohmy);
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
                if (reaction.MessageId == 445502492088860672)
                {
                    if (reaction.User.Value.IsBot)
                        return;
                    var globalAccount = Global.Client.GetUser(reaction.UserId);
                    switch (reaction.Emote.Name)
                    {
                        case "RKN":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "блок");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "блок"))
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
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "антисрач");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "антисрач"))
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
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "event block");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "event block"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "haHAA":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "voice-game");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "voice-game"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "thonk":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "riddler");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "riddler"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "AkaShrug":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "настолочник");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "настолочник"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "such":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Технарь");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Технарь"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "GWnanamiKannaNom":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Аниме");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Аниме"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "PogChamp":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "card player");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "card player"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "hanzo":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "hots");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "hots"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "yasuo":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "LoL");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "LoL"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "gacHIPride":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Recruit");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Recruit"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
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
                            var k =RemoveReactions(cash, channel, reaction, 2, globalAccount);
                            break;
                        }
                        case "warframe":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "warframe");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "warframe"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        } //fuckyeah
                        case "nintendoswitch":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "nintendofag");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "nintendofag"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "fuckyeah":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "fps");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "fps"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        case "sir":
                        {
                            var guildUser = Global.Client.GetGuild(338355570669256705).GetUser(reaction.UserId);
                            var roleToGive = Global.Client.GetGuild(338355570669256705).Roles
                                .SingleOrDefault(x => x.Name.ToString() == "Strategy");

                            var roleList = guildUser.Roles.ToArray();
                            if (roleList.Any(t => t.Name == "Strategy"))
                            {
                                await guildUser.RemoveRoleAsync(roleToGive);
                                var k = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                                return;
                            }

                            await guildUser.AddRoleAsync(roleToGive);
                            break;
                        }
                        default:

                            return;
                    }

                    var kk = RemoveReactions(cash, channel, reaction, 1, globalAccount);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Reaction for Roles not workind. '{0}'", error);
            }
        }
    }
}