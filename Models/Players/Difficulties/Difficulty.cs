using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4_Web_Project.Models.Players.Difficulties
{
    public abstract class Difficulty
    {
        public abstract int DetermineChoice();
    }
}