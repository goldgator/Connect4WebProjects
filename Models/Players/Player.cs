using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4_Web_Project.Models.Players
{
    public abstract class Player
    {
        //Make a property/field that resembles a profile

        public abstract int MakeMove(int[][] grid);

    }
}