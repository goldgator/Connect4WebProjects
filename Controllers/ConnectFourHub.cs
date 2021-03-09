using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Connect4_Web_Project.Models.Players;
using Connect4_Web_Project.Models.Board;
using Connect4_Web_Project.Models.Players.Difficulties;
using Connect4_Web_Project.Models.Misc;
using static Connect4_Web_Project.Controllers.GroupManager;


namespace Connect4_Web_Project.Controllers
{
    public class ConnectFourHub : Hub
    {
        public void Send(string colString, string pieceValue)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(colString, pieceValue);
            
        }

        public void SendChatMessage(string message)
        {
            string connectionID = Context.ConnectionId;
            GroupManager.Lobby lobby = GroupManager.FindLobbyViaConnectionID(connectionID);
            string groupName = lobby.lobbyName;

            string name = lobby.game.GetPlayerUsingID(connectionID).Name;

            Clients.Group(groupName).addNewMessageToPage(name, message);
        }

        public void SendChatMessage(string name, string message)
        {
            string connectionID = Context.ConnectionId;
            GroupManager.Lobby lobby = GroupManager.FindLobbyViaConnectionID(connectionID);
            string groupName = lobby.lobbyName;

            Clients.Group(groupName).addNewMessageToPage(name, message);
        }

        public void SendColGroup(string colString, string pieceValue)
        {
            string connectionID = Context.ConnectionId;
            GroupManager.Lobby lobby = GroupManager.FindLobbyViaConnectionID(connectionID);
            string groupName = lobby.lobbyName;

            //Check if it is this player's turn
            bool isTurn = lobby.game.IsPlayersTurn(connectionID);
            //IF it is their turn, continue, otherwise return early
            if (!isTurn) return;

            Board board = lobby.game.GetBoardInstance();

            bool colParse = int.TryParse(colString, out int colInput);
            bool pieceParse = int.TryParse(pieceValue, out int pieceKey);

            //Check if column is full, IF it is, send message saying that column is full and return early
            if (Utilties.FindEmptySpot(board.GetBoard(), colInput) == -1)
            {
                Clients.Caller.message("That column is full, please give a different column");
                return;
            } else if (colInput < 0 || colInput >= board.GetBoard().GetLength(1))
            {
                //Will ask for another input if column is out of bounds
                Clients.Caller.message("That column is out of bounds, please give a different column");
                return;
            }

            bool win = board.PlacePiece(colInput, pieceKey);
            lobby.game.NextTurn();

            Clients.Caller.takeInputAccess();


            if (win)
            {
                //Call win board
                Clients.Caller.getWin();
                //Call lose board, new page?
                Clients.OthersInGroup(groupName).getLose(lobby.game.GetPlayer(lobby.game.TurnInt).Name);
            }

            //Do computer turns if there
            while (!(lobby.game.GetPlayer(lobby.game.TurnInt) is Human) && !win)
            {
                Player player = lobby.game.GetPlayer(lobby.game.TurnInt);
                colInput = player.MakeMove(lobby.game.GetBoardInstance().GetBoard());
                pieceKey = player.PlayerNum;
                win = board.PlacePiece(colInput, pieceKey);
                SendChatMessage("Place a piece on column " + (colInput + 1));
                

                if (win)
                {
                    //Call lose board, new page?
                    Clients.Group(groupName).getLose(lobby.game.GetPlayer(lobby.game.TurnInt).Name);
                    //Leave loop
                    break;
                }

                lobby.game.NextTurn();
            }; 

            Clients.Group(groupName).updateBoard();
            
            if (!win)
            {
                //Clients.OthersInGroup(groupName).giveInputAccess();
                Clients.Client(lobby.game.GetPlayer(lobby.game.TurnInt).connectionID).giveInputAccess();
                
            }
        }

        public override Task OnConnected()
        {
            /*GroupManager.Lobby lobby = GroupManager.FindOpenLobby();
            JoinRoom(lobby.lobbyName);

            int pieceKey = lobby.game.GetPlayerSize() + 1;
            string pieceString = pieceKey + "";
            Clients.Caller.setData(pieceString, Context.ConnectionId);
            lobby.game.AddPlayer(new Human(Context.ConnectionId, pieceKey, Context.ConnectionId));
            int pieceKey = lobby.game.GetPlayerSize() + 1;
            string pieceString = pieceKey + "";
            Clients.Caller.setData(pieceString, Context.ConnectionId);
            lobby.game.AddPlayer(new Human(pieceKey, Context.ConnectionId));
            
            if (lobby.game.GetPlayerSize() == 2)
            {
                Clients.OthersInGroup(lobby.lobbyName).giveInputAccess();
            }*/
            


            return base.OnConnected();
        }

        public void JoinMatchPlayer(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = Utilties.RandomName;
            }

            GroupManager.Lobby lobby = GroupManager.FindOpenLobby();
            JoinRoom(lobby.lobbyName);


            int pieceKey = lobby.game.GetPlayerSize() + 1;
            string pieceString = pieceKey + "";
            Clients.Caller.setData(pieceString, Context.ConnectionId);
            lobby.game.AddPlayer(new Human(name, pieceKey, Context.ConnectionId));

            if (lobby.game.GetPlayerSize() == 2)
            {
                Clients.OthersInGroup(lobby.lobbyName).giveInputAccess();
                Clients.Group(lobby.lobbyName).updateBoard();
            } else
            {
                Clients.Caller.setLoading();
            }
            
        }

        public void JoinMatchAI(string type, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = Utilties.RandomName;
            }

            Player newPlayer = new Human(name, 1, Context.ConnectionId);
            Player opponent = null;

            Lobby lobby = GroupManager.CreateLobbyWithPlayer(newPlayer);
            JoinRoom(lobby.lobbyName);

            Clients.Caller.setData("1", Context.ConnectionId);

            switch (type)
            {
                case "Easy":
                    Easy easy = new Easy();
                    opponent = new Computer(2, easy, lobby.game.GetBoardInstance().GetBoard());
                    break;
                case "Medium":
                    Medium medium = new Medium();
                    opponent = new Computer(2, medium, lobby.game.GetBoardInstance().GetBoard());
                    break;
                case "Hard":
                    Hard hard = new Hard();
                    opponent = new Computer(2, hard, lobby.game.GetBoardInstance().GetBoard());
                    break;
            }

            lobby.game.AddPlayer(opponent);
            Clients.Caller.giveInputAccess();
            Clients.Caller.updateBoard();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            string connectionID = Context.ConnectionId;
            GroupManager.Lobby lobby = GroupManager.FindLobbyViaConnectionID(connectionID);

            

            if (lobby.game.GetPlayerSize() == 2)
            {
                //Replace disconnecting player with a Medium Computer
                Player removedPlayer = lobby.game.RemovePlayerUsingID(connectionID, out int removedIndex);

                int pieceKey = removedPlayer.PlayerNum;
                Player newComp = new Computer(pieceKey, new Medium(), lobby.game.GetBoardInstance().GetBoard());
                lobby.game.InsertPlayer(newComp, removedIndex);

                //Call a method to announce opponent has disconnected
                Clients.OthersInGroup(lobby.lobbyName).message(removedPlayer.Name + " Disconnected, they will be replaced with a Medium Computer: " + newComp.Name);


                //Check if it is their turn
                if (removedIndex == lobby.game.TurnInt)
                {
                    //IF it is, do the turn and any computers after
                    //Do computer turns if there
                    string groupName = lobby.lobbyName;
                    Board board = lobby.game.GetBoardInstance();
                    bool win = false;
                    while (!(lobby.game.GetPlayer(lobby.game.TurnInt) is Human) && !win)
                    {
                        Player player = lobby.game.GetPlayer(lobby.game.TurnInt);
                        int colInput = player.MakeMove(lobby.game.GetBoardInstance().GetBoard());
                        pieceKey = player.PlayerNum;
                        win = board.PlacePiece(colInput, pieceKey);

                        if (win)
                        {
                            //Call win board
                            Clients.Caller.getWin();
                            //Call lose board
                            Clients.OthersInGroup(groupName).getLose(lobby.game.GetPlayer(lobby.game.TurnInt).Name);
                            //Leave loop
                            break;
                        }

                        lobby.game.NextTurn();
                    };

                    Clients.Group(groupName).updateBoard();
                }

                
            } else if (lobby.game.GetPlayerSize() == 1)
            {
                GroupManager.lobbies.Remove(lobby);
            }

            //Check if all computers in lobby
            bool allComputers = true;
            for (int i = 0; i < lobby.game.GetPlayerSize(); i++)
            {
                allComputers = (lobby.game.GetPlayer(i) is Human);

                if (!allComputers) break;
            }

            //Remove if all Computers in lobby
            if (allComputers)
            {
                GroupManager.lobbies.Remove(lobby);
            }


            return base.OnDisconnected(stopCalled);
        }
        public Task JoinRoom(string lobbyName)
        {
            return Groups.Add(Context.ConnectionId, lobbyName);
        }
    }
}