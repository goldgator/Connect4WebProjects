using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4_Web_Project.Models.Players.Difficulties
{
    public abstract class Difficulty
    {
        protected Computer botPlayer;

        /// <summary>
        /// Determines the column based what difficulty is added to the computer player
        /// </summary>
        /// <returns>The index of the column chosen</returns>
        public abstract int DetermineChoice();

        /// <summary>
        /// Sets the botPlayer you pass in to be it's designated computer
        /// </summary>
        /// <param name="newBotPlayer"></param>
        public virtual void Instantiate(Computer newBotPlayer) {
            botPlayer = newBotPlayer;
        }
    }
}