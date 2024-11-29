using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using OpenMatchupServer.Player;
using OpenMatchupServer.Algo;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace OpenMatchupServer.Server
{
    public struct Team
    {
        List<GamePlayer> members;
    }

    public class GameMatch
    {
        List<Team> competitions;
    }

    public class MatchupManager
    {
        public Dictionary<int, MatchMakingInfo> matchContainer = new Dictionary<int, MatchMakingInfo>();

        private static MatchupManager _instance;

        private static readonly object _lock = new object();

        private MatchupManager()
        {
            
        }

        public static MatchupManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MatchupManager();
                        }
                    }
                }
                return _instance;
            }
        }


    }
}