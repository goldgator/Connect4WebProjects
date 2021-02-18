using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4_Web_Project.Models.Players
{
    public abstract class Player
    {
        //Make a property/field that resembles a profile

        public int PlayerNum { get; set; }

        /// <summary>
        /// Returns the column this player chose
        /// </summary>
        /// <param name="grid">The game grid representing the board</param>
        /// <returns></returns>
        public abstract int MakeMove(int[,] grid);

    }
}