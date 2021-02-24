using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4_Web_Project.Models.Players.Difficulties
{
    public class Impossible : Difficulty
    {
        public override int DetermineChoice()
        {
            return 0;
        }
    }
}