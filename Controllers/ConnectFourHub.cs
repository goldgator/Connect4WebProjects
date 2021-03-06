using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Connect4_Web_Project.Models.Players;
using Connect4_Web_Project.Models.Board;

namespace Connect4_Web_Project.Controllers
{
    public class ConnectFourHub : Hub
    {
        public void Send(string colString, string pieceValue)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(colString, pieceValue);
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

            Board board = lobby.game.GetBoardInstance();

            bool colParse = int.TryParse(colString, out int colInput);
            bool pieceParse = int.TryParse(pieceValue, out int pieceKey);

            bool win = board.PlacePiece(colInput - 1, pieceKey);

            if (win)
            {
                //Call win board
                Clients.Caller.getWin();

                //Call lose board
                Clients.OthersInGroup(groupName).getLose(lobby.game.GetPlayer(pieceKey-1).connectionID);
            }

            Clients.Group(groupName).broadcastMessage(colString, pieceValue);            
        }

        public override Task OnConnected()
        {
            GroupManager.Lobby lobby = GroupManager.FindOpenLobby();
            JoinRoom(lobby.lobbyName);

            int pieceKey = lobby.game.GetPlayerSize() + 1;
            string pieceString = pieceKey + "";
            Clients.Caller.setData(pieceString, Context.ConnectionId);
            lobby.game.AddPlayer(new Human(Context.ConnectionId, pieceKey, Context.ConnectionId));

            return base.OnConnected();
        }

        public Task JoinRoom(string lobbyName)
        {
            return Groups.Add(Context.ConnectionId, lobbyName);
        }
    }
}