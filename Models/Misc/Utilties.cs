using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4_Web_Project.Models.Players;
using Connect4_Web_Project.Models.Players.Difficulties;

namespace Connect4_Web_Project.Models.Misc
{
    public static class Utilties
    {

        public static string[] names =
        {
            "Morty",
            "Rick",
            "Zorb",
            "Bob",
            "Cameron",
            "Corey",
            "Jesse",
            "Ethan",
            "Tyler",
            "Hank",
            "York",
            "Terminator"
        };

        public static string RandomName {
            get
            {
                return names[new Random().Next(names.Length)];
            }
        }
        /// <summary>
        /// Searches the column from the bottom up for the first piece of the grid matching the value passed in
        /// </summary>
        /// <param name="grid">The 2d int array iterating through</param>
        /// <param name="value">The value you wish to find</param>
        /// <param name="col">The column you wish to search</param>
        /// <returns>The row index of the value found</returns>
        public static int FindFirstMatchingSpot(int[,] grid, int value, int col)
        {
            //Start from the bottom
            for (int row = grid.GetLength(0) - 1; row > -1; row--)
            {
                if (grid[row, col] == value)
                {
                    return row;
                }
            }

            //return -1 if value does not exist in column
            return -1;
        }

        /// <summary>
        /// Returns the row index of the first 0 it finds in a int 2d array, searching from the bottom up of the given column
        /// </summary>
        /// <param name="grid">2d int array to search through</param>
        /// <param name="col">column of 2d int array</param>
        /// <returns></returns>
        public static int FindEmptySpot(int[,] grid, int col)
        {
            return FindFirstMatchingSpot(grid, 0, col);
        }


        /// <summary>
        /// Find a match within a 2d int array passing in the row and col index of the starting piece, with the value you wish to find a match for. rowShift and colShift CANNOT be both zero.
        /// </summary>
        /// <param name="grid">2d int array to search through</param>
        /// <param name="startRow">Row index of the starting position</param>
        /// <param name="startCol">Column index of the starting position</param>
        /// <param name="value">Value you wish to find a match for</param>
        /// <param name="rowShift">How much the row shifts each check</param>
        /// <param name="colShift">How much the column shifts each check</param>
        /// <param name="matchAmount">How many matched pieces you must find to qualify as a full match</param>
        /// <param name="reverse">Default true, will check the opposite direction</param>
        /// <returns></returns>
        public static bool FindLinearMatch(int[,] grid, int startRow, int startCol, int value, int rowShift, int colShift, int matchAmount, bool reverse = true)
        {
            //variable to count matching values
            int matchCount = 0;

            int row = startRow;
            int col = startCol;

            //for loop that will run twice, flipping the sign on the second loop
            for (int flip = 1; flip >= -1; flip -= 2)
            {
                for (int i = 0; i <= matchAmount; i++)
                {
                    if (flip == -1)
                    {
                        //Set i to 1 if its been flipped and not checking the starter piece
                        i = 1;
                    }

                    //Determine if row or col are out of bounds, break if so
                    if (row >= grid.GetLength(0) || row < 0) break;
                    if (col >= grid.GetLength(1) || col < 0) break;

                    //If piece of grid equals the value passed in, increment count
                    if (grid[row, col] == value)
                    {
                        matchCount++;
                    }
                    else if (i != 0)
                    {
                        break;
                    }

                    // flip: 1 on first loop, -1 on second loop
                    row += rowShift * flip;
                    col += colShift * flip;
                }

                //IF reverse is false, break out of loop
                if (!reverse) break;

                //Restart row and col AND shift so it doesn't count starter piece twice
                row = startRow + (rowShift * flip * -1);
                col = startCol + (colShift * flip * -1);
            }

            return (matchCount >= matchAmount);
        }

        public static bool FindConnect4Win(int[,] grid, int startRow, int startCol, int value)
        {
            // <-> Horizontal <->
            if (FindLinearMatch(grid, startRow, startCol, value, 0, 1, 4)) return true;
            //  | Vertical | 
            if (FindLinearMatch(grid, startRow, startCol, value, 1, 0, 4)) return true;
            //  / Diagnol-up /
            if (FindLinearMatch(grid, startRow, startCol, value, -1, 1, 4)) return true;
            //  \ Diagnol-down \
            if (FindLinearMatch(grid, startRow, startCol, value, 1, 1, 4)) return true;

            return false;
        }

        public static bool FindConnect4Win(int[,] grid, int startRow, int startCol, int value, int matchAmount)
        {
            // <-> Horizontal <->
            if (FindLinearMatch(grid, startRow, startCol, value, 0, 1, matchAmount)) return true;
            //  | Vertical | 
            if (FindLinearMatch(grid, startRow, startCol, value, 1, 0, matchAmount)) return true;
            //  / Diagnol-up /
            if (FindLinearMatch(grid, startRow, startCol, value, -1, 1, matchAmount)) return true;
            //  \ Diagnol-down \
            if (FindLinearMatch(grid, startRow, startCol, value, 1, 1, matchAmount)) return true;

            return false;
        }

    }
}