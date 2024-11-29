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

    public class MatchupManager
    {
        private Dictionary<int, GamePlayer> matchWaitingContainer = new Dictionary<int, GamePlayer>();

        public MatchupManager()
        {
            
        }


    }
}