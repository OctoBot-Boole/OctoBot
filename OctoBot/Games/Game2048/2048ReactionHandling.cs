using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;

namespace OctoBot.Games.Game2048
{
    internal static class Reaction
    {
        public static async Task ReactionAddedFor2048(Cacheable<IUserMessage, ulong> cash,
            ISocketMessageChannel channel,
            SocketReaction reaction)
        {
            for (var i = 0; i < Global.OctopusGameMessIdList2048.Count; i++)
                if (reaction.MessageId == Global.OctopusGameMessIdList2048[i].OctoGameMessIdToTrack2048 &&
                    reaction.UserId == Global.OctopusGameMessIdList2048[i].OctoGameUserIdToTrack2048 &&
                    reaction.UserId != 423593006436712458) //Id for bot
                    switch (reaction.Emote.Name)
                    {
                        case "⬆":
                            NewGame.MakeMove(reaction.UserId, GameWork.MoveDirection.Up,
                                Global.OctopusGameMessIdList2048[i].SocketMsg);
                            await Global.OctopusGameMessIdList2048[i].SocketMsg.RemoveReactionAsync(reaction.Emote,
                                Global.OctopusGameMessIdList2048[i].Iuser, RequestOptions.Default);
                            break;
                        case "⬇":
                            NewGame.MakeMove(reaction.UserId, GameWork.MoveDirection.Down,
                                Global.OctopusGameMessIdList2048[i].SocketMsg);
                            await Global.OctopusGameMessIdList2048[i].SocketMsg.RemoveReactionAsync(reaction.Emote,
                                Global.OctopusGameMessIdList2048[i].Iuser, RequestOptions.Default);
                            break;
                        case "⬅":
                            NewGame.MakeMove(reaction.UserId, GameWork.MoveDirection.Left,
                                Global.OctopusGameMessIdList2048[i].SocketMsg);
                            await Global.OctopusGameMessIdList2048[i].SocketMsg.RemoveReactionAsync(reaction.Emote,
                                Global.OctopusGameMessIdList2048[i].Iuser, RequestOptions.Default);
                            break;
                        case "➡":
                            NewGame.MakeMove(reaction.UserId, GameWork.MoveDirection.Right,
                                Global.OctopusGameMessIdList2048[i].SocketMsg);
                            await Global.OctopusGameMessIdList2048[i].SocketMsg.RemoveReactionAsync(reaction.Emote,
                                Global.OctopusGameMessIdList2048[i].Iuser, RequestOptions.Default);
                            break;
                        case "❌":
                            NewGame.EndGame(reaction.UserId);
                            break;
                        case "🔃":
                            await cash.GetOrDownloadAsync().Result.RemoveAllReactionsAsync();
                            await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("⬅"));
                            await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("➡"));
                            await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("⬆"));
                            await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("⬇"));
                            await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("🔃"));
                            await cash.GetOrDownloadAsync().Result.AddReactionAsync(new Emoji("❌"));
                            break;
                        default:
                            return;
                    }
            await Task.CompletedTask;
        }
    }
}