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

            //Set colChoice to the index of the highest priority
            int colChoice = -1;

            //return colChoice
            return colChoice;
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
            //Check for wins

            //Check for opponent wins

            //Determine the priority of all the open spaces
        }
    }
}