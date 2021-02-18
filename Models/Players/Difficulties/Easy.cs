using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4_Web_Project.Models.Players.Difficulties
{
    public class Easy : Difficulty
    {
        /// <summary>
        /// Will return a random empty column to place a piece in
        /// </summary>
        /// <returns></returns>
        public override int DetermineChoice()
        {
            //Grab the internal grid to search through
            int[,] internalGrid = botPlayer.getInternalGrid();

            //Create Random Object for randomness
            Random rng = new Random();

            //Loop until it randomly rolls a open column
            int choice = 0;
            do
            {
                choice = rng.Next(0, internalGrid.GetLength(1));
            } while (internalGrid[0, choice] == 0);


            return choice;
        }


    }
}