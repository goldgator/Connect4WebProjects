using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4_Web_Project.Models.Players
{
    public class Online : Player
    {
        public override int MakeMove(int[,] grid)
        {
            return 0;
        }
    }
}