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

        public Board.Board GetBoardInstance()
        {
            return board;
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
            bool quit = false;
            while (!quit)
            {
                board.GetBoard();

                //human1 = new Human();
                computer1 = new Computer();
                players.Add(human1);
                players.Add(computer1);

                PlayGame();
            }
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