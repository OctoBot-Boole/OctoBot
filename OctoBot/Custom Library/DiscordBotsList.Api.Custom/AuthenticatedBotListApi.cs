using Miki.Rest;

namespace OctoBot.Custom_Library.DiscordBotsList.Api.Custom
{
    public class AuthDiscordBotListApi 
    {
        public ulong SelfId { get; }
        public string Token { get; }
        public RestClient RestClient = new RestClient("https://discordbots.org/api/");
        public AuthDiscordBotListApi(ulong selfId, string token)
        {
            SelfId = selfId;
            Token = token;
        }
    }
}