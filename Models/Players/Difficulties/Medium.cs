using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4_Web_Project.Models.Misc;

namespace Connect4_Web_Project.Models.Players.Difficulties
{
    public class Medium : Difficulty
    {
        int[] columnPriorities;


        public override void Instantiate(Computer newBotPlayer)
        {
            //Instantiate the bot in the base class Instantiate
            base.Instantiate(newBotPlayer);

            //Create a grid the same size as the playable grid for the priorityGrid
            int[,] grid = botPlayer.getInternalGrid();
            int width = grid.GetLength(1);
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
                }
                else if (columnPriorities[col] == highestPriority)
                {
                    choices.Add(col);
                }
            }

            //DEBUG, PRINT COLUMNPRIORITIES
            string debug = "";
            for (int col = 0; col < columnPriorities.Length; col++)
            {
                debug += columnPriorities[col] + ", ";
            }
            Console.WriteLine(debug);

            //Return random spot if multiple share max priority, return -1 if it somehow got now choices
            if (choices.Count > 0)
            {
                Random rng = new Random();
                return choices[rng.Next(choices.Count)];
            }
            else
            {
                return -1;
            }
        }

        

        private void UpdateColumnPriority()
        {
            //Reset all Priorities
            columnPriorities = new int[columnPriorities.Length];

            //Grab grid for easier reference
            int[,] grid = botPlayer.getInternalGrid();

            //Check for wins in each column (100 value if win)
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                //DEBUG: CATCHES COL AT CERTAIN VALUE
                if (col == 5)
                {
                    int blah = 0;
                }

                int row = Utilties.FindEmptySpot(grid, col);
                if (row == -1) continue;

                if (Utilties.FindConnect4Win(grid, row, col, botPlayer.pieceKey, 3))
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
                    //DEBUG: CATCHES COL AT CERTAIN VALUE
                    if (col == 6)
                    {
                        int blah = 0;
                    }

                    int row = Utilties.FindEmptySpot(grid, col);
                    if (row == -1) continue;

                    if (Utilties.FindConnect4Win(grid, row, col, player, 3))
                    {
                        columnPriorities[col] += 50;
                    }
                }
            }
        }
    }
}