using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace OctoBot.Configs
{
    public class Global
    {
        internal static DiscordShardedClient Client { get; set; }


        internal static ulong YellowTurlteChannelId { get; set; }
        internal static RestUserMessage YellowTurlteMessageTorack { get; set; }
        internal static int CommandEnabled { get; set; }


        internal static int OctoGamePlaying { get; set; }

        public static List<OctoGameMessAndUserTrack> OctopusGameMessIdList { get; internal set; } =
            new List<OctoGameMessAndUserTrack>();

        public struct OctoGameMessAndUserTrack
        {
            public ulong OctoGameMessIdToTrack;
            public ulong OctoGameUserIdToTrack;
            public RestUserMessage SocketMsg;
            public IUser Iuser;


            public OctoGameMessAndUserTrack(ulong octoGameMessIdToTrack, ulong octoGameUserIdToTrack,
                RestUserMessage socketMsg, IUser iuser)
            {
                OctoGameMessIdToTrack = octoGameMessIdToTrack;
                OctoGameUserIdToTrack = octoGameUserIdToTrack;
                SocketMsg = socketMsg;
                Iuser = iuser;
            }
        }

        public static List<OctoGameMessAndUserTrack2048> OctopusGameMessIdList2048 { get; internal set; } =
            new List<OctoGameMessAndUserTrack2048>();

        public struct OctoGameMessAndUserTrack2048
        {
            public ulong OctoGameMessIdToTrack2048;
            public ulong OctoGameUserIdToTrack2048;
            public RestUserMessage SocketMsg;
            public IUser Iuser;

            public OctoGameMessAndUserTrack2048(ulong octoGameMessIdToTrack2048, ulong octoGameUserIdToTrack2048,
                RestUserMessage socketMsg, IUser iuser)
            {
                OctoGameMessIdToTrack2048 = octoGameMessIdToTrack2048;
                OctoGameUserIdToTrack2048 = octoGameUserIdToTrack2048;
                SocketMsg = socketMsg;
                Iuser = iuser;
            }
        }


        public static List<BlogVotes> BlogVotesMessIdList { get; set; } = new List<BlogVotes>();

        public class BlogVotes
        {
            public IUser BlogAuthor;
            public IUserMessage SocketMsg;
            public IUser ReactionUser;
            public int Available;

            public BlogVotes(IUser blogAuthor, IUserMessage socketMsg, IUser reactionUser, int available)
            {
                BlogAuthor = blogAuthor;
                SocketMsg = socketMsg;
                ReactionUser = reactionUser;
                Available = available;
            }
        }


        public static List<ArtVotes> ArtVotesList { get; set; } = new List<ArtVotes>();

        public class ArtVotes
        {
            public IUser BlogAuthor;
            public IUserMessage SocketMsg;
            public List<IUser> UserVoted = new List<IUser>();
            public List<string> Emotename = new List<string>();

            public ArtVotes(IUser blogAuthor, IUserMessage socketMsg, IUser userVoted, string emotename)
            {
                BlogAuthor = blogAuthor;
                SocketMsg = socketMsg;
                UserVoted.Add(userVoted);
                Emotename.Add(emotename);
            }
        }


        public static List<CommandRam> CommandList { get; set; } = new List<CommandRam>();

        public class CommandRam
        {
            public IUser BlogAuthor;
            public IUserMessage UserSocketMsg;
            public IUserMessage BotSocketMsg;


            public CommandRam(IUser blogAuthor, IUserMessage userSocketMsg, IUserMessage botSocketMsg)
            {
                BlogAuthor = blogAuthor;
                UserSocketMsg = userSocketMsg;
                BotSocketMsg = botSocketMsg;
            }
        }

        public static async Task<string> SendWebRequest(string requestUrl)
        {
            using (var client = new HttpClient(new HttpClientHandler()))
            {
                client.DefaultRequestHeaders.Add("User-Agent", "OctoBot");
                using (var response = await client.GetAsync(requestUrl))
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        return response.StatusCode.ToString();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

    }

    internal class Config
    {
        public static BotConfig Bot;

        static Config()
        {
            var json = File.ReadAllText(@"OctoDataBase/config.json");
            Bot = JsonConvert.DeserializeObject<BotConfig>(json);
        }
    }

    public struct BotConfig
    {
        public string Token;
        public string DbLtoken;
    }



}