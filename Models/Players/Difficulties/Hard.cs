using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4_Web_Project.Models.Misc;

namespace Connect4_Web_Project.Models.Players.Difficulties
{
    public class Hard : Difficulty
    {
        int[,] basePriorityGrid;
        int[] columnPriorities;


        public override void Instantiate(Computer newBotPlayer)
        {
            //Instantiate the bot in the base class Instantiate
            base.Instantiate(newBotPlayer);

            //Create a grid the same size as the playable grid for the priorityGrid
            int[,] grid = botPlayer.getInternalGrid();
            int height = grid.GetLength(0);
            int width = grid.GetLength(1);
            basePriorityGrid = CreateBasePriorityGrid(height,width);
            columnPriorities = new int[width];
        }

        public override int DetermineChoice()
        {
            //Update column priorities
            UpdateColumnPriority();

            //Collect all columns that have max priority
            List<int> choices = new List<int>();
            int highestPriority = int.MinValue;

            for (int col = 0; col < columnPriorities.Length; col++)
            {
                if (columnPriorities[col] > highestPriority)
                {
                    highestPriority = columnPriorities[col];
                    choices.Clear();
                    choices.Add(col);
                } else if (columnPriorities[col] == highestPriority)
                {
                    choices.Add(col);
                }
            }

            //Return random spot if multiple share max priority, return -1 if it somehow got now choices
            if (choices.Count > 0)
            {
                Random rng = new Random();
                return choices[rng.Next(choices.Count)];
            } else
            {
                return -1;
            }
        }

        private int[,] CreateBasePriorityGrid(int baseHeight, int baseWidth)
        {
            //Create a base grid to hold all basePriorities
            int[,] newGrid = new int[baseHeight, baseWidth];

            //Add priorities, lower spots have higher priority
                //Lower spots have higher priority
                //Middle spots have higher priority
            for (int col = 0; col < baseWidth; col++)
            {
                for (int row = 0; row < baseHeight; row++)
                {
                    //Adds [0, 1, 2, 3, 4, 5] to each column on a traditional board
                    newGrid[row, col] += row;

                    //Middle value of row rounded down
                    int middle = (int) Math.Floor(baseWidth / 2d);
                    //Applies [0, 1, 2, 3, 2, 1, 0] to each row on a traditional board
                    newGrid[row, col] += (int)(middle - Math.Abs(middle - col));

                    //Resulting board on a traditional board
                    //0 1 2 3 2 1 0
                    //1 2 3 4 3 2 1
                    //2 3 4 5 4 3 2 
                    //3 4 5 6 5 4 3
                    //4 5 6 7 6 5 4
                    //5 6 7 8 7 6 5
                }
            }

            return newGrid;
        }

        private void UpdateColumnPriority()
        {
            //Reset all Priorities
            columnPriorities = new int[columnPriorities.Length];

            //Grab grid for easier reference
            int[,] grid = botPlayer.getInternalGrid();

            //Add base priority grid
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                int row = Utilties.FindEmptySpot(grid, col);
                if (row == -1) continue;

                columnPriorities[col] += basePriorityGrid[row, col];
            }

            //Check for wins in each column (100 value if win)
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                int row = Utilties.FindEmptySpot(grid, col);
                if (row == -1) continue;

                if (Utilties.FindConnect4Win(grid, row, col, botPlayer.pieceKey,3))
                {
                    columnPriorities[col] += 100;
                }
            }

            //Check for opponent wins (50 value each match)
            for (int player = 1; player <= 4; player++)
            {
                if (player == botPlayer.pieceKey) continue;

                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    int row = Utilties.FindEmptySpot(grid, col);
                    if (row == -1) continue;

                    if (Utilties.FindConnect4Win(grid, row, col, player, 3))
                    {
                        columnPriorities[col] += 50;
                    }
                }
            }

            //Determine the priority by finding nearby spaces of same color (1 each)
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                int newRow, newCol;

                int startRow = Utilties.FindEmptySpot(grid, col);
                if (startRow == -1) continue;
                int[] coords = { startRow, col };

                for (int g = 0; g < 8; g++)
                {
                    //Skip this loop if checking straight up
                    if (g == 6) continue;

                    //Check direction based on loop (0-7 goes to each major direction)
                    double radian = g * Math.PI * .25;
                    newRow = coords[0] + (int)Math.Round(Math.Sin(radian));
                    newCol = coords[1] + (int)Math.Round(Math.Cos(radian));
                    if (newRow < 0 || newRow >= grid.GetLength(0)) continue;
                    if (newCol < 0 || newCol >= grid.GetLength(1)) continue;

                    if (grid[newRow,newCol] == botPlayer.pieceKey)
                    {
                        columnPriorities[col]++;
                    }
                }
            }

            //Check for threat spaces (3+ for spots that have 3+ opponent pieces near that spot)
            for (int player = 1; player <= 4; player++)
            {
                if (player == botPlayer.pieceKey) continue;

                //Counts pieces of current player
                int pieceCount = 0;

                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    int newRow, newCol;

                    int startRow = Utilties.FindEmptySpot(grid, col);
                    if (startRow == -1) continue;
                    int[] coords = { startRow, col };

                    for (int g = 0; g < 8; g++)
                    {
                        //Skip this loop if checking straight up
                        if (g == 6) continue;

                        //Check direction based on loop (0-7 goes to each major direction)
                        double radian = g * Math.PI * .25;
                        newRow = coords[0] + (int)Math.Round(Math.Sin(radian));
                        newCol = coords[1] + (int)Math.Round(Math.Cos(radian));
                        if (newRow < 0 || newRow >= grid.GetLength(0)) continue;
                        if (newCol < 0 || newCol >= grid.GetLength(1)) continue;

                        if (grid[newRow, newCol] == player)
                        {
                            pieceCount++;
                        }
                    }

                    if (pieceCount >= 3)
                    {
                        columnPriorities[col] += pieceCount;
                    }
                }
            }
        }

    }
}