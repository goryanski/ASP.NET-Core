using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Practice_16.Models;
using Practice_16.Utils;

namespace Practice_16.Hubs
{
    public class GameHub: Hub
    {
        // all clients
        static List<ConnectedClient> connectedClients = new List<ConnectedClient>();
        // only 2 clients who will play
        static List<ConnectedClient> gamers = new List<ConnectedClient>();

        static List<int> field = new List<int>
        {
            // 0 - empty, 1 - Gamer_1, 2 - Gamer_2
            0, 0, 0,
            0, 0, 0,
            0, 0, 0
        };

        #region Connect
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            
            ConnectedClient newClient = new ConnectedClient
            {
                 ConnectionId = Context.ConnectionId,
                 Type = GetClientType()
            };
            AddClientToLists(newClient);

            switch (connectedClients.Count)
            {
                case 1:
                    await Clients.Caller.SendAsync("firstClientConnected", newClient);
                    break;
                case 2:
                    await Clients.Caller.SendAsync("secondClientConnected", newClient);
                    // there are 2 players - now the game starts
                    await Clients.Client(gamers[0].ConnectionId).SendAsync("beginWithMove", gamers[0]);
                    await Clients.Client(gamers[1].ConnectionId).SendAsync("beginWithoutMove", gamers[1]);
                    break;
                default:
                    // rest clients will be spectators
                    await Clients.Caller.SendAsync("spectatorConnected", newClient);
                    break;
            }
        }
        private string GetClientType()
        {
            switch (connectedClients.Count)
            {
                case 0:
                    return ClientType.Gamer_1;
                case 1:
                    return ClientType.Gamer_2;
                default:
                    return ClientType.Spectator;
            }
        }
        private void AddClientToLists(ConnectedClient newClient)
        {
            switch (connectedClients.Count)
            {
                case 0:
                case 1:
                    gamers.Add(newClient);
                    break;
            }
            connectedClients.Add(newClient);
        }
        #endregion

        #region Disconnect
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            ConnectedClient client = connectedClients.First(c => c.ConnectionId == Context.ConnectionId);
            await DisconnectClient(client);
        }

        private async Task DisconnectClient(ConnectedClient client)
        {
            switch (client.Type)
            {
                case ClientType.Gamer_1:
                    ClearField();
                    // 1. exit when Gamer_1 is only one player (before start game)
                    if (gamers.Count == 1)
                    {
                        connectedClients.Remove(connectedClients[0]);
                        gamers.Remove(gamers[0]);
                    }
                    // 2. exit when Gamer_1 is now playing with Gamer_2
                    if (gamers.Count == 2)
                    {
                        // delete Gamer_1
                        connectedClients.Remove(connectedClients[0]);
                        gamers.Remove(gamers[0]);
                        // change Gamer_2 type
                        connectedClients[0].Type = ClientType.Gamer_1;
                        gamers[0].Type = ClientType.Gamer_1;

                        await ChangeGamers();
                    }
                    break;
                case ClientType.Gamer_2:
                    ClearField();
                    // 2. exit when Gamer_2 is now playing with Gamer_1
                    if (gamers.Count == 2)
                    {
                        // delete Gamer_2
                        connectedClients.Remove(connectedClients[1]);
                        gamers.Remove(gamers[1]);

                        await ChangeGamers();
                    }
                    break;
                case ClientType.Spectator:
                    // just remove from connectedClients list
                    connectedClients.Remove(connectedClients.First(c => c.ConnectionId == client.ConnectionId));
                    break;
            }
        }

        private async Task ChangeGamers()
        {
            // if there was a Spectator (third connected client) - make him as Gamer_2
            if (connectedClients.Count >= 2)
            {
                // set next spectator from queue as Gamer_2
                connectedClients[1].Type = ClientType.Gamer_2;
                var newGamer = connectedClients[1];
                gamers.Add(newGamer);

                // notify Gamer_1 and new Gamer_2 about changes in UI and start new game
                await StartNewGameAfterDisconnection();
            }
            else
            {
                // make Gamer_1 wait for new opponent in UI
                await WaitForNewOpponent();
            }
        }

        public async Task StartNewGameAfterDisconnection()
        {
            await Clients.Client(gamers[0].ConnectionId).SendAsync("beginAfterDisconnection", gamers[0]);
            await Clients.Client(gamers[1].ConnectionId).SendAsync("beginWithoutMove", gamers[1]);
        }

        private async Task WaitForNewOpponent()
        {
            await Clients.Client(gamers[0].ConnectionId).SendAsync("waitForNewOpponent", gamers[0]);
        }
        #endregion

        #region Game
        public async Task PlayerMadeMove(string playerType, string selectedCell)
        {
            int cellNumber = int.Parse(selectedCell);
            int playerNumber = GetPlayerNumber(playerType);
            field[cellNumber] = playerNumber;
            if(PlayerWon())
            {
                // send notify + restart
                await Clients.Caller.SendAsync("playerWon");
                // to show last move of winner (to opponent and spectators)
                await Clients.Client(GetOpponentId(playerType)).SendAsync("passMove", selectedCell);
                await ShowMoveToSpectators(playerType, selectedCell);
                await EndOfViewingForSpectators();

                await Clients.Client(GetOpponentId(playerType)).SendAsync("playerLost");
                // restart
                ClearField();
            }
            else
            {
                if(NoWaysLeft())
                {
                    await Clients.Client(gamers[0].ConnectionId).SendAsync("noWaysLeft");
                    await Clients.Client(gamers[1].ConnectionId).SendAsync("noWaysLeft");
                    await EndOfViewingForSpectators();
                    // restart
                    ClearField();
                }
                else
                {
                    // pass Move to another player
                    await Clients.Client(GetOpponentId(playerType)).SendAsync("passMove", selectedCell);

                    await ShowMoveToSpectators(playerType, selectedCell);
                }
            }
        }

        #region Show game for spectators
        private async Task ShowMoveToSpectators(string playerType, string selectedCell)
        {
            List<string> spectators = GetSpectators();
            if (spectators.Count != 0)
            {
                await Clients.Clients(spectators).SendAsync("watchTheGame", playerType, selectedCell);
            }
        }
        private async Task EndOfViewingForSpectators()
        {
            List<string> spectators = GetSpectators();
            if (spectators.Count != 0)
            {
                await Clients.Clients(spectators).SendAsync("endOfViewing");
            }
        }

        private List<string> GetSpectators()
        {
            List<string> spectators = new List<string>();
            if (connectedClients.Count > 2)
            {
                // take only spectators Ids
                for (int i = 0; i < connectedClients.Count; i++)
                {
                    if (i == 0 || i == 1)
                    {
                        continue;
                    }
                    spectators.Add(connectedClients[i].ConnectionId);
                }
            }
            return spectators;
        }
        #endregion

        private bool NoWaysLeft()
        {
            foreach (var cell in field)
            {
                if (cell == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void ClearField()
        {
            for (int i = 0; i < field.Count; i++)
            {
                field[i] = 0;
            }
        }

        private string GetOpponentId(string type)
        {
            if(gamers[0].Type == type)
            {
                return gamers[1].ConnectionId;
            }
            else
            {
                return gamers[0].ConnectionId;
            }
        }

        private bool PlayerWon()
        {
            if (field[0] == field[1] && field[1] == field[2] && field[2] != 0 // first horizontal
                || field[3] == field[4] && field[4] == field[5] && field[5] != 0 // second horizontal
                || field[6] == field[7] && field[7] == field[8] && field[8] != 0 // third horizontal
                || field[0] == field[3] && field[3] == field[6] && field[6] != 0 // first vertical
                || field[1] == field[4] && field[4] == field[7] && field[7] != 0 // second vertical
                || field[2] == field[5] && field[5] == field[8] && field[8] != 0 // third vertical
                || field[0] == field[4] && field[4] == field[8] && field[8] != 0 // first diagonal
                || field[2] == field[4] && field[4] == field[6] && field[6] != 0) // second diagonal
            {
                return true;
            }
            return false;
        }

        private int GetPlayerNumber(string playerType)
        {
            return (playerType == ClientType.Gamer_1) ? 1 : 2;
        }

        public async Task CheckNoOneLeft()
        {
            if(gamers.Count == 2)
            {
                // ok
                await Clients.Client(gamers[0].ConnectionId).SendAsync("noOneLeftGame");
                await Clients.Client(gamers[1].ConnectionId).SendAsync("noOneLeftGame");
            }
            else
            {
                // someone left game and there are no another clients waiting for game
                await Clients.Client(gamers[0].ConnectionId).SendAsync("opponentLeftGame", gamers[0]);
            }
        }
        #endregion
    }
}
