using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4_Web_Project.Models.Players
{
    public class Computer : Player
    {
        public override int MakeMove(int[][] grid)
        {
            return 0;
        }
    }
}