using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4_Web_Project.Models.Players
{
    public class Human : Player
    {
        public Human(string name, int pieceKey, string newConnectionID)
        {
            Name = name;
            PlayerNum = pieceKey;
            connectionID = newConnectionID;
        }

        public string Name { get; set; }

        //public Human() { }
        public Human(string name)
        {
            Name = name;
        }

        public override int MakeMove(int[,] grid)
        {
            Console.WriteLine("Pick Column to Place Piece");
            bool success = int.TryParse(Console.ReadLine(), out int col);
            //Loop to keep the input going?

            if (col - 1 < 0)
            {
                col = 0;
                return col;
            }
            return col - 1;
        }
    }
}