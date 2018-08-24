using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using OctoBot.Automated;
using OctoBot.Commands.PersonalCommands;
using OctoBot.CustomForServers;
using OctoBot.Games.Game2048;
using OctoBot.Games.OctoGame;

namespace OctoBot.Handeling
{
    public class DiscordEventHandler
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private readonly DiscordShardedClient _client;
        private readonly CommandHandeling _commandHandler;
        private readonly ServerActivityLogger _serverActivityLogger;
        private readonly CheckIfCommandGiveRole _checkIfCommandGiveRole;
        private readonly ReactionsHandelingForBlogAndArt _reactionsHandelingForBlogAndArt;
        private readonly GiveRoleOnJoin _giveRoleOnJoin;
        private readonly CheckForVoiceChannelStateForVoiceCommand _checkForVoiceChannelStateForVoiceCommand;
        private readonly LvLing _lvLing;
        private readonly UserSkatisticsCounter _userSkatisticsCounter;
        private readonly TimerForChangingAvatar _timerForChangingAvatar;

        public DiscordEventHandler(DiscordShardedClient client, CommandHandeling commandHandler
            , ServerActivityLogger serverActivityLogger, CheckIfCommandGiveRole checkIfCommandGiveRole,
            CheckForVoiceChannelStateForVoiceCommand checkForVoiceChannelStateForVoiceCommand,
            GiveRoleOnJoin giveRoleOnJoin, ReactionsHandelingForBlogAndArt reactionsHandelingForBlogAndArt,
            LvLing lvLing, UserSkatisticsCounter userSkatisticsCounter, TimerForChangingAvatar timerForChangingAvatar)
        {
            _client = client;
            _commandHandler = commandHandler;
            _serverActivityLogger = serverActivityLogger;
            _checkIfCommandGiveRole = checkIfCommandGiveRole;
            _checkForVoiceChannelStateForVoiceCommand = checkForVoiceChannelStateForVoiceCommand;
            _giveRoleOnJoin = giveRoleOnJoin;
            _reactionsHandelingForBlogAndArt = reactionsHandelingForBlogAndArt;
            _lvLing = lvLing;
            _userSkatisticsCounter = userSkatisticsCounter;
            _timerForChangingAvatar = timerForChangingAvatar;
        }

        public void InitDiscordEvents()
        {
            _client.ChannelCreated += ChannelCreated;
            _client.ChannelDestroyed += ChannelDestroyed;
            _client.ChannelUpdated += ChannelUpdated;
            _client.CurrentUserUpdated += CurrentUserUpdated;
            _client.ShardDisconnected += _client_ShardDisconnected;
            _client.GuildAvailable += GuildAvailable;
            _client.GuildMembersDownloaded += GuildMembersDownloaded;
            _client.GuildMemberUpdated += GuildMemberUpdated;
            _client.GuildUnavailable += GuildUnavailable;
            _client.GuildUpdated += GuildUpdated;
            _client.JoinedGuild += JoinedGuild;
            _client.LeftGuild += LeftGuild;
            _client.Log += Log;
            _client.LoggedIn += LoggedIn;
            _client.LoggedOut += LoggedOut;
            _client.MessageDeleted += MessageDeleted;
            _client.MessageReceived += MessageReceived;
            _client.MessageUpdated += MessageUpdated;
            _client.ReactionAdded += ReactionAdded;
            _client.ReactionRemoved += ReactionRemoved;
            _client.ReactionsCleared += ReactionsCleared;
            _client.ShardReady += _client_ShardReady;
            _client.ShardConnected += _client_ShardConnected;
            _client.RecipientAdded += RecipientAdded;
            _client.RecipientRemoved += RecipientRemoved;
            _client.RoleCreated += RoleCreated;
            _client.RoleDeleted += RoleDeleted;
            _client.RoleUpdated += RoleUpdated;
            _client.UserBanned += UserBanned;
            _client.UserIsTyping += UserIsTyping;
            _client.UserJoined += UserJoined;
            _client.UserLeft += UserLeft;
            _client.UserUnbanned += UserUnbanned;
            _client.UserUpdated += UserUpdated;
            _client.UserVoiceStateUpdated += UserVoiceStateUpdated;
        }

        private async Task _client_ShardReady(DiscordSocketClient arg)
        {
            GreenBuuTimerClass.StartTimer(); ////////////// Timer1 Green Boo starts
            CheckForPull.CheckTimerForPull();
            CheckForMute.CheckTimer();
            CheckReminders.CheckTimer();
            CheckBirthday.CheckTimer();
            _timerForChangingAvatar.TimerForChangeBotAvatar();
            CheckToDeleteVoiceChannel.CheckTimer();
            //  _client.Ready += YellowTurtle.StartTimer; //// Timer3
        }

        private async Task _client_ShardDisconnected(Exception arg1, DiscordSocketClient arg2)
        {
            Console.WriteLine($"Shard {arg2.ShardId} Disconnected");
            _serverActivityLogger.Client_Disconnected(arg1);
            
        }

        private async Task _client_ShardConnected(DiscordSocketClient arg)
        {
            _serverActivityLogger.Client_Connected();
        }

        private async Task ChannelCreated(SocketChannel channel)
        {
            _serverActivityLogger.Client_ChannelCreated(channel);
        }

        private async Task ChannelDestroyed(SocketChannel channel)
        {
            _serverActivityLogger.Client_ChannelDestroyed(channel);
        }

        private async Task ChannelUpdated(SocketChannel channelBefore, SocketChannel channelAfter)
        {
            _serverActivityLogger.Client_ChannelUpdated(channelBefore, channelAfter);
        }


        private async Task CurrentUserUpdated(SocketSelfUser userBefore, SocketSelfUser userAfter)
        {
        }

        private async Task GuildAvailable(SocketGuild guild)
        {
        }

        private async Task GuildMembersDownloaded(SocketGuild guild)
        {
        }

        private async Task GuildMemberUpdated(SocketGuildUser userBefore, SocketGuildUser userAfter)
        {
            _serverActivityLogger.Client_GuildMemberUpdated(userBefore, userAfter);
        }

        private async Task GuildUnavailable(SocketGuild guild)
        {
        }

        private async Task GuildUpdated(SocketGuild guildBefore, SocketGuild guildAfter)
        {
        }

        private async Task JoinedGuild(SocketGuild guild)
        {
            _serverActivityLogger.Client_JoinedGuild(guild);
        }


        private async Task LeftGuild(SocketGuild guild)
        {
        }

        private async Task Log(LogMessage logMessage)
        {
            ConsoleLogger.Log(logMessage);
        }

        private async Task LoggedIn()
        {
        }

        private async Task LoggedOut()
        {
        }

        private async Task MessageDeleted(Cacheable<IMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
            if(!cacheMessage.HasValue)
                return;
            if(cacheMessage.Value.Author.IsBot)
                return;
            _serverActivityLogger.Client_MessageDeleted(cacheMessage, channel);
            _userSkatisticsCounter.Client_MessageDeleted(cacheMessage, channel);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if(message.Author.IsBot)
                return;
            _checkIfCommandGiveRole.Client_MessageReceived(message, _client);
            _commandHandler.HandleCommandAsync(message);
            _serverActivityLogger.Client_MessageReceived(message);
            _serverActivityLogger.Client_MessageRecivedForServerStatistics(message);
            _lvLing.Client_UserSentMess(message);
            _userSkatisticsCounter.Clien_MessageReceived(message);
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> cacheMessageBefore, SocketMessage messageAfter,
            ISocketMessageChannel channel)
        {
            if(!cacheMessageBefore.HasValue)
                return;
            if(cacheMessageBefore.Value.Author.IsBot)
                return;
            _commandHandler._client_MessageUpdated(cacheMessageBefore, messageAfter, channel);
            _serverActivityLogger.Client_MessageUpdated(cacheMessageBefore, messageAfter, channel);
            _userSkatisticsCounter.Client_MessageUpdated(cacheMessageBefore, messageAfter, channel);       
        }

        private async Task ReactionAdded(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel,
            SocketReaction reaction)
        {
            if (reaction.User.Value.IsBot) return;
            Reaction.ReactionAddedFor2048(cacheMessage, channel, reaction);
            OctoGameReaction.ReactionAddedForOctoGameAsync(cacheMessage, channel, reaction);
            ColorRoleReaction.ReactionAddedForRole(cacheMessage, channel, reaction);
            RoomRoleReaction.ReactionAddedForRole(cacheMessage, channel, reaction);
            RoomRoleReactionHandeling.ReactionAddedForRole(cacheMessage, channel, reaction);
            _reactionsHandelingForBlogAndArt.Client_ReactionAddedForArtVotes(cacheMessage, channel, reaction);
            _reactionsHandelingForBlogAndArt.Client_ReactionAddedAsyncForBlog(cacheMessage, channel, reaction);
        }

        private async Task ReactionRemoved(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel,
            SocketReaction reaction)
        {
            if (reaction.User.Value.IsBot) return;
            _reactionsHandelingForBlogAndArt.Client_ReactionRemovedForBlog(cacheMessage, channel, reaction);
            _reactionsHandelingForBlogAndArt.Client_ReactionRemovedForArtVotes(cacheMessage, channel, reaction);
        }

        private async Task ReactionsCleared(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
        }

        private async Task RecipientAdded(SocketGroupUser user)
        {
        }

        private async Task RecipientRemoved(SocketGroupUser user)
        {
        }

        private async Task RoleCreated(SocketRole role)
        {
        }

        private async Task RoleDeleted(SocketRole role)
        {
            _serverActivityLogger.Client_RoleDeleted(role);
        }

        private async Task RoleUpdated(SocketRole roleBefore, SocketRole roleAfter)
        {
            _serverActivityLogger.Client_RoleUpdated(roleBefore, roleAfter);
        }

        private async Task UserBanned(SocketUser user, SocketGuild guild)
        {
        }

        private async Task UserIsTyping(SocketUser user, ISocketMessageChannel channel)
        {
        }

        private async Task UserJoined(SocketGuildUser user)
        {
            Announcer.AnnounceUserJoin(user);
            _giveRoleOnJoin.Client_UserJoined_ForRoleOnJoin(user);
        }

        private async Task UserLeft(SocketGuildUser user)
        {
        }

        private async Task UserUnbanned(SocketUser user, SocketGuild guild)
        {
        }

        private async Task UserUpdated(SocketUser user, SocketUser guild)
        {
        }

        private async Task UserVoiceStateUpdated(SocketUser user, SocketVoiceState voiceStateBefore,
            SocketVoiceState voiceStateAfter)
        {
            _checkForVoiceChannelStateForVoiceCommand.Client_UserVoiceStateUpdatedForCreateVoiceChannel(user,
                voiceStateBefore, voiceStateAfter);
        }
    }
}