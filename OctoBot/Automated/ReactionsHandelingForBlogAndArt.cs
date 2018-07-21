using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Configs.Users;

namespace OctoBot.Automated
{
    public class ReactionsHandelingForBlogAndArt
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        public async Task ReactionAddedForArtVotes(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (arg3.User.Value.IsBot)
                return;


            var artMessagesList = Global.ArtVotesList;

            if (arg3.Emote.Name == "📊" || arg3.Emote.Name == "🎨" || arg3.Emote.Name == "🏆")
            {
                foreach (var v in artMessagesList)
                    if (arg1.Value.Id == v.SocketMsg.Id)
                        return;

                try
                {
                    var artVoteMess = new Global.ArtVotes(arg1.Value.Author, arg1.Value, arg1.Value.Author,
                        arg3.Emote.Name);
                    Global.ArtVotesList.Add(artVoteMess);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                await arg1.GetOrDownloadAsync().Result
                    .RemoveReactionAsync(arg3.Emote, arg3.User.Value, RequestOptions.Default);
                await arg1.Value.AddReactionAsync(new Emoji("1⃣"));
                await arg1.Value.AddReactionAsync(new Emoji("2⃣"));
                await arg1.Value.AddReactionAsync(new Emoji("3⃣"));
                await arg1.Value.AddReactionAsync(new Emoji("4⃣"));
                await arg1.Value.AddReactionAsync(new Emoji("5⃣"));
            }


            if (arg3.User.Value.Id == arg1.Value.Author.Id)
                return;
            foreach (var v1 in artMessagesList)
                if (!v1.UserVoted.Contains(arg3.User.Value) && v1.SocketMsg == arg1.Value)
                {
                    // Console.WriteLine($"working2");
                    if (arg3.Channel is IGuildChannel chanGuild)
                    {
                        var account = UserAccounts.GetAccount(v1.BlogAuthor, chanGuild.Guild.Id);
                        switch (arg3.Emote.Name)
                        {
                            case "1⃣":
                                account.ArtVotesQty += 1;
                                account.ArtVotesSum += 1;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "2⃣":
                                account.ArtVotesQty += 1;
                                account.ArtVotesSum += 2;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "3⃣":
                                account.ArtVotesQty += 1;
                                account.ArtVotesSum += 3;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "4⃣":
                                account.ArtVotesQty += 1;
                                account.ArtVotesSum += 4;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "5⃣":
                                account.ArtVotesQty += 1;
                                account.ArtVotesSum += 5;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                        }
                    }

                    v1.UserVoted.Add(arg3.User.Value);
                    v1.Emotename.Add(arg3.Emote.Name);
                }

            //return;
        }

        public async Task Client_ReactionAddedForArtVotes(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            ReactionAddedForArtVotes(arg1, arg2, arg3);
        }

        public async Task ReactionRemovedForArtVotes(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (arg3.User.Value.IsBot)
                return;
            if (arg3.User.Value.Id == arg1.Value.Author.Id)
                return;

            var artMessagesList = Global.ArtVotesList;
            foreach (var v in artMessagesList)
                if (v.UserVoted.Contains(arg3.User.Value) && v.SocketMsg == arg1.Value)
                    for (var j = 0; j < v.UserVoted.Count; j++)
                        //  Console.WriteLine($"working remove voted123 emote: {artMessagesList[i].Emotename[j]}  entered: {arg3.Emote.Name}");
                        if (arg3.Emote.Name == v.Emotename[j] && arg3.User.Value.Id == v.UserVoted[j].Id)
                        {
                            // Console.WriteLine($"working remove voted = {artMessagesList[i].UserVoted.Count}");
                            if (arg3.Channel is IGuildChannel chanGuild)
                            {
                                var account = UserAccounts.GetAccount(v.BlogAuthor, chanGuild.Guild.Id);
                                switch (arg3.Emote.Name)
                                {
                                    case "1⃣":
                                        account.ArtVotesQty -= 1;
                                        account.ArtVotesSum -= 1;
                                        UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                        break;
                                    case "2⃣":
                                        account.ArtVotesQty -= 1;
                                        account.ArtVotesSum -= 2;
                                        UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                        break;
                                    case "3⃣":
                                        account.ArtVotesQty -= 1;
                                        account.ArtVotesSum -= 3;
                                        UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                        break;
                                    case "4⃣":
                                        account.ArtVotesQty -= 1;
                                        account.ArtVotesSum -= 4;
                                        UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                        break;
                                    case "5⃣":
                                        account.ArtVotesQty -= 1;
                                        account.ArtVotesSum -= 5;
                                        UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                        break;
                                }
                            }

                            v.UserVoted.Remove(arg3.User.Value);
                            v.Emotename.Remove(arg3.Emote.Name);
                            //  Console.WriteLine($"removed from voted = {artMessagesList[i].UserVoted.Count}");
                        }
        }

        public async Task Client_ReactionRemovedForArtVotes(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            ReactionRemovedForArtVotes(arg1, arg2, arg3);
        }

        public async Task ReactionAddedAsyncForBlog(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (arg3.User.Value.IsBot)
                return;

            var blogList = Global.BlogVotesMessIdList;
            foreach (var v in blogList)
                if (v.SocketMsg.Id == arg1.Id && v.ReactionUser.Id == arg3.User.Value.Id)
                {
                    if (arg3.User.Value.Id == v.BlogAuthor.Id)
                    {
                        await arg3.Channel.SendMessageAsync("Ты не можешь ставить оценку самому себе!");
                        return;
                    }

                    /*
                    if (blogList[i].Available == 0)
                    {
                       await arg3.Channel.SendMessageAsync($"Ты уже голосовал! Сними прошлую оценку, чтобы поставить новую.");
                        continue;
                    }*/
                    if (arg3.Channel is IGuildChannel chanGuild)
                    {
                        var account = UserAccounts.GetAccount(v.BlogAuthor, chanGuild.Guild.Id);

                        switch (arg3.Emote.Name)
                        {
                            case "1⃣":
                                account.BlogVotesQty += 1;
                                account.BlogVotesSum += 1;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "2⃣":
                                account.BlogVotesQty += 1;
                                account.BlogVotesSum += 2;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "3⃣":
                                account.BlogVotesQty += 1;
                                account.BlogVotesSum += 3;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "4⃣":
                                account.BlogVotesQty += 1;
                                account.BlogVotesSum += 4;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "zazz":
                                account.BlogVotesQty += 1;
                                account.BlogVotesSum += 5;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                        }
                    }
                }
        }

        public async Task Client_ReactionAddedAsyncForBlog(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            ReactionAddedAsyncForBlog(arg1, arg2, arg3);
        }

        public async Task ReactionRemovedForBlog(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            var blogList = Global.BlogVotesMessIdList;
            foreach (var v in blogList)
                if (v.SocketMsg.Id == arg1.Id && v.ReactionUser.Id == arg3.User.Value.Id)
                {
                    if (arg3.User.Value.Id == v.BlogAuthor.Id) return;

                    if (arg3.Channel is IGuildChannel chanGuild)
                    {
                        var account = UserAccounts.GetAccount(v.BlogAuthor, chanGuild.Guild.Id);
                        switch (arg3.Emote.Name)
                        {
                            case "1⃣":
                                account.BlogVotesQty--;
                                account.BlogVotesSum -= 1;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "2⃣":
                                account.BlogVotesQty--;
                                account.BlogVotesSum -= 2;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "3⃣":
                                account.BlogVotesQty--;
                                account.BlogVotesSum -= 3;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "4⃣":
                                account.BlogVotesQty--;
                                account.BlogVotesSum -= 4;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                            case "zazz":
                                account.BlogVotesQty--;
                                account.BlogVotesSum -= 5;
                                UserAccounts.SaveAccounts(chanGuild.Guild.Id);
                                break;
                        }
                    }

                    v.Available = 1;
                }
        }

        public async Task Client_ReactionRemovedForBlog(Cacheable<IUserMessage, ulong> arg1,
            ISocketMessageChannel arg2, SocketReaction arg3)
        {
            ReactionRemovedForBlog(arg1, arg2, arg3);
        }
    }
}