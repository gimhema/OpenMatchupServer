using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OpenMatchupServer.Player
{
    public class PlayerManager
    {
        private static PlayerManager _instance;
        private static readonly object _lock = new object();


        public Dictionary<int, GamePlayer> playerContainer = new Dictionary<int, GamePlayer>();
        private int playerKey = 0;

        private PlayerManager() 
        {

        }

        public static PlayerManager Instance
        {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new PlayerManager();
                    }
                }
            }
            return _instance;
        }
    }

        public int TopKey()
        {
            return playerKey;
        }

        public void IncreaseKey()
        {
            playerKey += 1;
        }

        public void AddNewPlayer(GamePlayer newPlayer)
        {
            int _top = TopKey();
            newPlayer.SetId(_top);
            playerContainer.Add(_top, newPlayer);
            IncreaseKey();
        }

        public GamePlayer FindPlayerById(int id) 
        {
            return playerContainer[id];
        }
    }
}