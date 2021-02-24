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

        //The key used to determine it's piece on the board
        public int pieceKey { get; set; } = 2;



        public Computer(int newPieceKey, Difficulty newDifficulty, int[,] newInternalGrid)
        {
            pieceKey = newPieceKey;
            difficulty = newDifficulty;
            internalGrid = newInternalGrid;

            difficulty.Instantiate(this);
        }

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