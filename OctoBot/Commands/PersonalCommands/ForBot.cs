using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OctoBot.Configs;
using OctoBot.Custom_Library;
using OctoBot.Handeling;
using OctoBot.Helper;

namespace OctoBot.Commands.PersonalCommands
{
    public class ForBot : ModuleBase<ShardedCommandContextCustom>
    {
        [Command("LG")]
        [Alias("topGuild")]
        [RequireOwner]
        [Description("Leve particular guild ( will show all guilds the bot in with no parameters)")]
        public async Task LeaveGuild(int page = 1)
        {
            if (page < 1)
            {
                await CommandHandeling.ReplyAsync(Context,
                    "Boole! Try different page <_<");
                return;
            }

            var accounts = Global.Client.Guilds.ToList().OrderByDescending(x => x.MemberCount).ToList();
            const int usersPerPage = 9;

            var lastPage = 1 + accounts.Count / (usersPerPage + 1);
            if (page > lastPage)
            {
                await CommandHandeling.ReplyAsync(Context,
                    $"Boole. Last Page is {lastPage}");
                return;
            }

            var ordered = accounts;

            var embB = new EmbedBuilder()
                .WithTitle("Top By Guilds:")
                .WithFooter(
                    $"Page {page}/{lastPage} ● Say \"topGuild 2\" to see second page (you can edit previous message)");


            page--;
            for (var j = 0; j < ordered.Count; j++)
                if (ordered[j].Id == Context.Guild.Id)
                    embB.WithDescription(
                        $"**#{j + usersPerPage * page + 1} {Context.Guild.Name} ({Context.Guild.Id}) {ordered[j].MemberCount} Members**\n**______**");

            for (var i = 1; i <= usersPerPage && i + usersPerPage * page <= ordered.Count; i++)
            {
                var account = ordered[i - 1 + usersPerPage * page];
                //var guildAccount = 
                embB.AddField($"#{i + usersPerPage * page} {account.Name} ({account.Id})", $"{account.MemberCount} Members", true);
            }

            await CommandHandeling.ReplyAsync(Context, embB);
        }


        [Command("setAvatar")]
        [Alias("ava")]
        [Description(
            "Changes Bot's avatar ( useless as bot is chaning its avatar every 30 minutes from 200 mine octopus pictures")]
        public async Task SetAvatar(string link)
        {
            if (Context.User.Id != 181514288278536193)
                return;
            try
            {
                var webClient = new WebClient();
                var imageBytes = webClient.DownloadData(link);

                var stream = new MemoryStream(imageBytes);

                var image = new Image(stream);
                await Context.Client.CurrentUser.ModifyAsync(k => k.Avatar = image);
                await Context.Message.DeleteAsync();
                await ReplyAsync($"Avatar have been set to `{link}`");
            }
            catch 
            {
                /*
                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.AddField("Ошибка", $"Не можем поставить аватарку: {e.Message}");

                await CommandHandeling.ReplyAsync(Context, embed);*/
            }
        }

        [Command("Game")]
        [Alias("ChangeGame", "SetGame")]
        [Description("Set up playing game (palying...)")]
        public async Task SetGame([Remainder] string gamename)
        {
            try
            {
                if (Context.User.Id != 181514288278536193)
                    return;

                await Context.Client.SetGameAsync(gamename);
                await ReplyAsync($"Changed game to `{gamename}`");
                await Context.Message.DeleteAsync();
            }
            catch 
            {
                /*
                var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.AddField("Ошибка", $"Не можем изменить игру: {e.Message}");

                await CommandHandeling.ReplyAsync(Context, embed);*/
            }
        }

        [Command("username")]
        [Description("change bot's Username on context Guild")]
        public async Task ChangeUsername([Remainder] string name)
        {
            await Context.Client.CurrentUser.ModifyAsync(usr => usr.Username = name);
            await ReplyAsync($":ok_hand: Changed my username to {name}");
        }

        [Command("nick")]
        [Description("change bot's nickname on context Guild")]
        public async Task Nickname(SocketGuildUser username, [Remainder] string name)
        {
            if (Context.User.Id != 181514288278536193)
                return;
            try
            {
                await Context.Guild.GetUser(username.Id).ModifyAsync(x => x.Nickname = name);
            }
            catch
            {
                /*var embed = new EmbedBuilder();
                embed.WithFooter("lil octo notebook");
                embed.AddField("Ошибка", $"Не можем изменить ник: {e.Message}");

                await CommandHandeling.ReplyAsync(Context, embed);*/
            }
        }

        [Command("ApdMessEm")]
        [Description("edit bot's message (embed)")]
        public async Task ApdMessEmbed(ulong channeld, ulong messId, [Remainder] string messa)
        {
            if (Context.User.Id != 181514288278536193)
                return;
            try
            {
                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User);
                embed.WithColor(Color.Blue);
                // embed.WithFooter("lil octo notebook");
                embed.WithFooter("lil octo notebook");
                embed.AddField("Сообщение:",
                    $"{messa}");

                if (await Context.Guild.GetTextChannel(channeld)
                    .GetMessageAsync(messId) is IUserMessage textChannel)
                    await textChannel.ModifyAsync(mess =>
                    {
                        mess.Embed = embed.Build();
                        // This somehow can't be empty or it won't update the 
                        // embed propperly sometimes... I don't know why
                        // message.Content =  Constants.InvisibleString;
                    });
                await Context.Message.DeleteAsync();
            }
            catch
            {
             //   await ReplyAsync("We cannot update this message!");
            }
        }

        [Command("ApdMess")]
        [Description("edit bot's message (string)")]
        public async Task ApdMessString(ulong channeld, ulong messId, [Remainder] string messa)
        {
            try
            {
                if (Context.User.Id != 181514288278536193)
                    return;

                var message = await Context.Guild.GetTextChannel(channeld)
                    .GetMessageAsync(messId) as IUserMessage;

                var builder = new StringBuilder();
                builder.Append($"{messa}");

                if (message != null) await message.ModifyAsync(m => m.Content = builder.ToString());
                await Context.Message.DeleteAsync();
            }
            catch
            {
              //  await ReplyAsync("We cannot update this message!");
            }
        }

        [Command("SendMess")]
        [RequireOwner]
        [Description("Echo command")]
        public async Task SendMessString(ulong channeld, [Remainder] string messa)
        {
            try
            {
                var textChannel = Context.Guild.GetTextChannel(channeld);
                await textChannel.SendMessageAsync($"{messa}");
                await Context.Message.DeleteAsync();
            }
            catch
            {
              //  await ReplyAsync("We cannot send this message!");
            }
        }

        [Command("emo")]
        [Description("Placing an THE emoji under particular message")]
        public async Task EmoteToMEss(ulong channeld, ulong messId, [Remainder] string messa)
        {
            try
            {
                Console.WriteLine(messa);
                if (Context.User.Id != 181514288278536193)
                    return;
                var emote = Emote.Parse($"{messa}");

                var check = messa.ToCharArray();
                if (check[0] == '<')
                    if (await Context.Guild.GetTextChannel(channeld).GetMessageAsync(messId) is IUserMessage message)
                        await message.AddReactionAsync(emote);
                    else if (await Context.Guild.GetTextChannel(channeld).GetMessageAsync(messId) is IUserMessage mess)
                        await mess.AddReactionAsync(new Emoji($"{messa}"));

                await Task.CompletedTask;
            }
            catch
            {
                //
            }
        }

        [Command("stuff")]
        [Description("bit stuffing")]
        public async Task Stuffing(string number)
        {
            //011111101111110
            var array = number.Select(ch => ch - '0').ToArray();

            var check = 0;
            var afterStuff = "";
            var times = 0;
            try
            {
                for (var i = 0; i < array.Length; i++)
                    if (array[i] == 1 && array[i] == array[i + 1])
                    {
                        check += 1;
                        afterStuff += $"{array[i].ToString()}";
                        if (check == 5)
                        {
                            afterStuff += "**__0__**";
                            times++;
                            check = 0;
                        }
                    }
                    else
                    {
                        afterStuff += $"{array[i].ToString()}";
                        check = 0;
                    }
            }
            catch
            {
                afterStuff += $"{array[array.Length - 1].ToString()}";
            }

            var embed = new EmbedBuilder();
            embed.WithColor(Color.Green);
            embed.AddField("Хм... что-бы это значило...", $"Before Stuffing: {number} - {number.Length} characters\n" +
                                                          $"After Stuffing: {afterStuff} - {afterStuff.Length - times * 8} characters\n" +
                                                          $"After Framing: **01111110**{afterStuff}**01111110**");

            await CommandHandeling.ReplyAsync(Context, embed);
        }


        [Command("getInvite")]
        [Alias("inv")]
        [RequireOwner]
        public async Task GetInviteToTheServer(ulong guildId)
        {
            var inviteUrl = Global.Client.GetGuild(guildId).DefaultChannel.CreateInviteAsync();
            await CommandHandeling.ReplyAsync(Context, $"{inviteUrl.Result.Url}");
        }


        [Command("uptime")]
        [Alias("runtime")]
        public async Task UpTime()
        {
            var proc = Process.GetCurrentProcess();

            var mem = proc.WorkingSet64 / 1000000;
            var threads = proc.Threads;
            var time = DateTime.Now - proc.StartTime;
            var cpu = proc.TotalProcessorTime.TotalMilliseconds / proc.PrivilegedProcessorTime.TotalMilliseconds;


            var sw = Stopwatch.StartNew();
            var msg = await Context.Channel.SendMessageAsync("check").ConfigureAwait(false);
            sw.Stop();

            await HelperFunctions.DeleteMessOverTime(msg, 0);
            var wtf = Global.CommandList;





            var embed = new EmbedBuilder();
            embed.WithColor(SecureRandomStatic.Random(254), SecureRandomStatic.Random(254), SecureRandomStatic.Random(254));
            embed.AddField("Bot Statistics:", $"Your ping: {(int) sw.Elapsed.TotalMilliseconds}ms\n" +
                                              $"Runtime: {time.Hours}h:{time.Minutes}m\n" +
                                              //$"CPU usage: {cpu:n0} (not right)\n" +
                                              $"Memory: {mem:n0}Mb\n" +
                                              $"Threads using: {threads.Count}\n" +
                                              $"Commands in cache(100 max): {wtf.Count}\n" +
                                              $"Servers in: {Global.Client.Guilds.Count}\n");
            await CommandHandeling.ReplyAsync(Context, embed);


        }

    }
}