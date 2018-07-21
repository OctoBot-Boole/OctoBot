/*
This logic is made by https://github.com/petrspelos 
I just changed it a little bit to fit my need and fix some problems
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace OctoBot.Games.Game2048
{
    public static class GameWork
    {
        public static int[] SlideArray(int[] arr)
        {
            var nonZero = arr.Where(v => v != 0).ToList();
            var numberOfZeros = 4 - nonZero.Count;
            var zeroes = new int[numberOfZeros].ToList();
            zeroes.AddRange(nonZero);

            return zeroes.ToArray();
        }

        public struct CollapseRowResult
        {
            public int[] NewRow { get; }

            public int GainedScore { get; }

            public CollapseRowResult(int[] newRow, int gainedScore)
            {
                NewRow = newRow;
                GainedScore = gainedScore;
            }
        }

        public static CollapseRowResult CollapseSameNeighbours(int[] arr)
        {
            var resultGrid = (int[]) arr.Clone();
            var gainedScore = 0;

            for (var i = 3; i > 0; i--)
            {
                var rightElement = resultGrid[i];
                var leftElement = resultGrid[i - 1];
                if (rightElement == 0 || rightElement != leftElement) continue;
                resultGrid[i] = rightElement + leftElement;
                resultGrid[i - 1] = 0;
                gainedScore += resultGrid[i];
            }

            return new CollapseRowResult(resultGrid, gainedScore);
        }

        public static int[][] SlideGrid(int[][] grid)
        {
            var result = CloneGrid(grid);

            for (var i = 0; i < 4; i++) result[i] = SlideArray(result[i]);

            return result;
        }

        public struct CollapseGridResult
        {
            public int[][] NewBoard { get; }

            public int GainedScore { get; }

            public CollapseGridResult(int[][] newBoard, int gainedScore)
            {
                NewBoard = newBoard;
                GainedScore = gainedScore;
            }
        }

        public static CollapseGridResult CollapseGrid(int[][] grid)
        {
            var newGrid = CloneGrid(grid);
            var gainedScore = 0;

            for (var i = 0; i < 4; i++)
            {
                var rowResult = CollapseSameNeighbours(newGrid[i]);
                newGrid[i] = rowResult.NewRow;
                gainedScore += rowResult.GainedScore;
            }

            return new CollapseGridResult(newGrid, gainedScore);
        }

        public static int[][] TransposeGrid(int[][] grid)
        {
            int[][] tGrid =
            {
                new[] {0, 0, 0, 0},
                new[] {0, 0, 0, 0},
                new[] {0, 0, 0, 0},
                new[] {0, 0, 0, 0}
            };

            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                tGrid[i][j] = grid[j][i];

            return tGrid;
        }

        public static bool GameIsWon(int[][] grid)
        {
            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                if (grid[i][j] == 2147483647)
                    return true;

            return false;
        }

        public static int ZeroCount(int[][] grid)
        {
            var count = 0;
            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                if (grid[i][j] == 0)
                    count++;

            return count;
        }

        public static bool GameIsLost(int[][] grid)
        {
            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
            {
                if (grid[i][j] == 0) return false;
                if (TileHasSameNeighbour(grid, i, j))
                    return false;
            }

            return true;
        }

        private static bool TileHasSameNeighbour(int[][] grid, int x, int y)
        {
            if (x > 0 && grid[x - 1][y] == grid[x][y]) return true;
            if (y > 0 && grid[x][y - 1] == grid[x][y]) return true;
            if (x < 3 && grid[x][y] == grid[x + 1][y]) return true;
            return y < 3 && grid[x][y] == grid[x][y + 1];
        }

        public static int[][] MirrorGrid(int[][] grid)
        {
            var result = CloneGrid(grid);

            for (var i = 0; i < 4; i++) result[i] = result[i].Reverse().ToArray();

            return result;
        }

        public struct PositionPoint
        {
            public int X { get; }
            public int Y { get; }

            public PositionPoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public static int[][] AddRandomTile(int[][] grid)
        {
            var result = CloneGrid(grid);

            var rnd = new Random();

            var tileToAdd = rnd.Next(0, 2) == 1 ? 2 : 4;

            var validPositions = new List<PositionPoint>();

            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                if (result[i][j] == 0)
                    validPositions.Add(new PositionPoint(i, j));

            if (validPositions.Count == 0) return result;

            var randPos = validPositions[rnd.Next(0, validPositions.Count)];

            result[randPos.X][randPos.Y] = tileToAdd;

            return CloneGrid(result);
        }

        public enum MoveDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public enum GameState
        {
            Playing,
            Lost,
            Won
        }

        public struct MoveResults
        {
            public GameState State { get; }

            public int[][] Board { get; }

            public int GainedScore { get; }

            public MoveResults(GameState state, int[][] board, int gainedScore)
            {
                State = state;
                Board = board;
                GainedScore = gainedScore;
            }
        }

        public static MoveResults MakeMove(int[][] grid, MoveDirection direction)
        {
            var workingGrid = CloneGrid(grid);
            var transposed = false;
            var mirrored = false;

            if (direction == MoveDirection.Down)
            {
                workingGrid = TransposeGrid(workingGrid);
                transposed = true;
            }
            else if (direction == MoveDirection.Left)
            {
                workingGrid = MirrorGrid(workingGrid);
                mirrored = true;
            }
            else if (direction == MoveDirection.Up)
            {
                workingGrid = TransposeGrid(workingGrid);
                workingGrid = MirrorGrid(workingGrid);
                mirrored = true;
                transposed = true;
            }
            else if (direction == MoveDirection.Right)
            {
                // default  
            }

            workingGrid = SlideGrid(workingGrid);
            var collapseResult = CollapseGrid(workingGrid);
            workingGrid = collapseResult.NewBoard;
            workingGrid = SlideGrid(workingGrid);

            if (mirrored) workingGrid = MirrorGrid(workingGrid);

            if (transposed) workingGrid = TransposeGrid(workingGrid);

            // Check for win state

            var resultGrid = CloneGrid(workingGrid);

            if (GameIsWon(resultGrid)) return new MoveResults(GameState.Won, resultGrid, collapseResult.GainedScore);

            if (GameIsLost(resultGrid)) return new MoveResults(GameState.Lost, resultGrid, collapseResult.GainedScore);

            resultGrid = AddRandomTile(resultGrid);

            return new MoveResults(GameState.Playing, resultGrid, collapseResult.GainedScore);
        }

        public static int[][] GetNewGameBoard()
        {
            int[][] grid =
            {
                new[] {0, 0, 0, 0},
                new[] {0, 0, 0, 0},
                new[] {0, 0, 0, 0},
                new[] {0, 0, 0, 0}
            };

            grid = AddRandomTile(grid);
            grid = AddRandomTile(grid);

            return CloneGrid(grid);
        }

        public static int[][] CloneGrid(int[][] grid)
        {
            var newGrid = new int[4][];
            for (var i = 0; i < 4; i++) newGrid[i] = (int[]) grid[i].Clone();

            return newGrid;
        }
    }
}