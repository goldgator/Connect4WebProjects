using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4_Web_Project.Models.Players.Difficulties;

namespace Connect4_Web_Project.Models.Players
{
    public class Computer : Player
    {

        private int[,] internalGrid;
        private Difficulty difficulty;

        public override int MakeMove(int[,] grid)
        {
            //Update internal grid
            internalGrid = grid;
            //call Difficulty DetermineChoice()
            int choice = difficulty.DetermineChoice();
            //return the column chosen
            return choice;
        }

        /// <summary>
        /// Returns the internalGrid that the computer has
        /// </summary>
        /// <returns></returns>
        public int[,] getInternalGrid()
        {
            return internalGrid;
        }


    }
}