using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Years._2025
{
    public class Day04
    {

        public void Run(string inputPath)
        {
            var rollsOfPaperGrid = File.ReadAllLines(inputPath).ToList();

            int rollsOfPaperPartOne = HowManyRollsOfPaperPartOne(rollsOfPaperGrid);
            int rollsOfPaperPartTwo = HowManyRollsOfPaperPartTwo(rollsOfPaperGrid);

            Console.WriteLine($"Part One: {rollsOfPaperPartOne}");
            Console.WriteLine($"Part Two: {rollsOfPaperPartTwo}");
        }
        public int HowManyRollsOfPaperPartOne(List<string> rollsOfPaperGrid)
        {
            int height = rollsOfPaperGrid.Count;
            int width = rollsOfPaperGrid[0].Length;
            int accessibleRolls = 0;

            // Directions: N, NE, E, SE, S, SW, W, NW
            int[] dx = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (rollsOfPaperGrid[row][col] != '@')
                        continue;

                    int adjacentRolls = 0;
                    for (int dir = 0; dir < 8; dir++)
                    {
                        int newRow = row + dx[dir];
                        int newCol = col + dy[dir];
                        if (newRow >= 0 && newRow < height && newCol >= 0 && newCol < width)
                        {
                            if (rollsOfPaperGrid[newRow][newCol] == '@')
                                adjacentRolls++;
                        }
                    }
                    if (adjacentRolls < 4)
                        accessibleRolls++;
                }
            }

            return accessibleRolls;
        }

        public int HowManyRollsOfPaperPartTwo(List<string> rollsOfPaperGrid)
        {
            int height = rollsOfPaperGrid.Count;
            int width = rollsOfPaperGrid[0].Length;
            char[][] grid = rollsOfPaperGrid.Select(row => row.ToCharArray()).ToArray();

            // Directions: N, NE, E, SE, S, SW, W, NW
            int[] dx = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

            int totalRemoved = 0;
            bool removedAny;

            do
            {
                removedAny = false;
                var toRemove = new List<(int, int)>();

                // Find all accessible rolls in the current grid
                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        if (grid[row][col] != '@')
                            continue;

                        int adjacentRolls = 0;
                        for (int dir = 0; dir < 8; dir++)
                        {
                            int newRow = row + dx[dir];
                            int newCol = col + dy[dir];
                            if (newRow >= 0 && newRow < height && newCol >= 0 && newCol < width)
                            {
                                if (grid[newRow][newCol] == '@')
                                    adjacentRolls++;
                            }
                        }
                        if (adjacentRolls < 4)
                            toRemove.Add((row, col));
                    }
                }

                // Remove all accessible rolls found in this round
                foreach (var (row, col) in toRemove)
                {
                    grid[row][col] = '.'; // Mark as removed
                    totalRemoved++;
                    removedAny = true;
                }
            }
            while (removedAny);

            return totalRemoved;
        }

    }
}
