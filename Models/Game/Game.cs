using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connect4_Web_Project.Models.Board;
using Connect4_Web_Project.Models.Players;
using Connect4_Web_Project.Models.Misc;

namespace Connect4_Web_Project.Models.Game
{
    public class Game
    {
        private List<Player> players = new List<Player>();
        private Board.Board board = new Board.Board();
        public int TurnInt { get; set; } = 0;

        public Board.Board GetBoardInstance()
        {
            return board;
        }

        /// <summary>
        /// Removes the player with the current connectionID and returns it
        /// </summary>
        /// <param name="connectionID">The connectionID of the player being replaced</param>
        /// <returns>The player removed</returns>
        public Player RemovePlayerUsingID(string connectionID)
        {
            return RemovePlayerUsingID(connectionID, out int newIndex);
        }

        public bool IsPlayersTurn(string connectionID)
        {
            return connectionID == players[TurnInt].connectionID;
        }

        public void NextTurn()
        {
            TurnInt = (TurnInt + 1) % players.Count;
        }

        public Player GetPlayerUsingID(string connectionID)
        {
            return players.Find(ctx => ctx.connectionID == connectionID);
        }

        public Player RemovePlayerUsingID(string connectionID, out int removeIndex)
        {
            removeIndex = players.FindIndex(ctx => ctx.connectionID == connectionID);
            Player p = players[removeIndex];
            players.RemoveAt(removeIndex);
            return p;
        }

        public void InsertPlayer(Player newPlayer, int index)
        {
            players.Insert(index, newPlayer);
        }

        public bool hasPlayer(string connectionID)
        {
            foreach (Player p in players)
            {
                if (p.connectionID == connectionID)
                {
                    return true;
                }
            }

            return false;
        }
      
        
        Player human1, human2, computer1, computer2, currentPlayer;


        Guid ID { get; set; }

        int turnIndex = 0;

        public Game() { }

        public int GetPlayerSize()
        {
            return players.Count;
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }


        public Player GetPlayer(int index)
        {
            return players[index];
        }

        public void RunGame()
        {
            //bool quit = false;
            //while (!quit)
            //{
            //    board.GetBoard();

            //    human1 = new Human();
            //    computer1 = new Computer();
            //    players.Add(human1);
            //    players.Add(computer1);

            //    PlayGame();
            //}
        }

        public void PlayGame()

        {
            currentPlayer = players[0];
            bool gameOver = false;
            while (!gameOver)
            {
                currentPlayer.MakeMove(board.GetBoard());
                
                //instead of 0, 0.. need to get player input from webpage or start from first filled row/col
                gameOver = Utilties.FindConnect4Win(board.GetBoard(), 0, 0, currentPlayer.PlayerNum);

                GameTurn();
            }
            System.Console.WriteLine("I won");
        }

        public void GameTurn()
        {
            int turn = 0;

            int choice = players[turn].MakeMove(board.GetBoard());
            if (choice >= 1 && choice <= 7)
            {
                board.PlacePiece(choice, players[turn].PlayerNum);

                turn = (turn + 1) % players.Count;
            }
        }
    }
}