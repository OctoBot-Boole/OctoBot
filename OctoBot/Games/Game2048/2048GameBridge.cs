using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.Rest;
using OctoBot.Configs;
using OctoBot.Configs.Users;

namespace OctoBot.Games.Game2048
{
    public struct GameStruct
    {
        public ulong PlayerId { get; set; }
        public RestUserMessage Message { get; set; }
        public int[][] Grid { get; set; }
        public int Score { get; set; }
        public GameWork.GameState State { get; set; }
        public int Move { get; set; }
    }

    public static class NewGame
    {
        public static List<GameStruct> Games;

        static NewGame()
        {
            Games = new List<GameStruct>();
        }

        public static bool UserIsPlaying(ulong userId)
        {
            return Games.Any(g => g.PlayerId == userId);
        }

        public static void CreateNewGame(ulong userId, RestUserMessage message)
        {
            var game = new GameStruct
            {
                Grid = GameWork.GetNewGameBoard(),
                Score = 0,
                State = GameWork.GameState.Playing,
                PlayerId = userId,
                Message = message,
                Move = 0
                ////////////////////
            };

            Games.Add(game);
            UpdateMessage(game, userId);
        }

        public static void MakeMove(ulong userId, GameWork.MoveDirection direction, RestUserMessage socketMsg)
        {
            for (var i = 0; i < Global.OctopusGameMessIdList2048.Count; i++)
                if (userId == Global.OctopusGameMessIdList2048[i].OctoGameUserIdToTrack2048)
                {
                    var game = Games.FirstOrDefault(g => g.PlayerId == userId);
                    var gamesId = Games.IndexOf(game);

                    var result = GameWork.MakeMove(game.Grid, direction);

                    game.Score += result.GainedScore;
                    game.State = result.State;
                    game.Grid = result.Board;
                    game.Move++;

                    Games[gamesId] = game;

                    UpdateMessage(game, userId);
                    return;
                }
        }

        public static async void EndGame(ulong userId)
        {
            try
            {
                var game = Games.FirstOrDefault(g => g.PlayerId == userId);
                Games.Remove(game);

                var builder = new StringBuilder();
                builder.Append($"boole... where did it go?");

                await game.Message.ModifyAsync(m => m.Content = builder.ToString());
                await game.Message.RemoveAllReactionsAsync();
            }
            catch
            {
                // ignored
            }
        }


        public static async void UpdateMessage(GameStruct game, ulong userId)
        {
            try
            {
                var globalAccount = Global.Client.GetUser(userId);
                var chanelGuil = game.Message.Channel as IGuildChannel;
                var account = UserAccounts.GetAccount(globalAccount, chanelGuil.Guild.Id);
                if (game.Score > account.Best2048Score)
                {
                    account.Best2048Score = game.Score;
                    UserAccounts.SaveAccounts(chanelGuil.Guild.Id);
                }


                var builder = new StringBuilder();
                builder.Append("Score: ");
                builder.Append(game.Score);
                builder.Append("\nMoves: ");
                builder.Append(game.Move);
                builder.Append("\n```cpp\n");
                builder.Append(FormatBoard(game.Grid));


                if (game.State == GameWork.GameState.Lost)
                {
                    builder.Clear();
                    builder.Append("lol, you died, lol-lol you died\n");
                    builder.Append("Score: ");
                    builder.Append(game.Score);

                    EndGame(userId);
                }
                else if (game.State == GameWork.GameState.Won)
                {
                    builder.Clear();
                    builder.Append("**well done**\n"); //btw there is no winning point lol
                    builder.Append("Score: ");
                    builder.Append(game.Score);

                    EndGame(userId);
                }

                builder.Append($"\n```");

                await game.Message.ModifyAsync(m => m.Content = builder.ToString());
            }
            catch
            {
                // ignored
            }
        }

        public static string FormatBoard(int[][] board)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    var paddingCount = 4 - board[i][j].ToString().Length;
                    builder.Append(board[i][j]);
                    for (var m = 0; m < paddingCount; m++) builder.Append(" ");
                    builder.Append("|");
                }

                builder.Append("\n");
            }

            return builder.ToString();
        }

        public static void PrintGridToConsole(int[][] grid)
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    Console.Write(grid[i][j]);
                    Console.Write(" ");
                }

                Console.WriteLine();
            }
        }
    }
}