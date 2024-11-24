using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OpenMatchupServer.Player
{
    public class PlayerManager
    {
        public Dictionary<int, Player> playerContainer = new Dictionary<int, Player>();

        public PlayerManager() 
        {

        }

        public void AddNewPlayer(int id, Player newPlayer)
        {
            playerContainer.Add(id, newPlayer);
        }

        public Player FindPlayerById(int id) 
        {
            return playerContainer[id];
        }
    }
}