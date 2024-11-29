using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using OpenMatchupServer.Player;
using OpenMatchupServer.Algo;
using System.Collections.Generic;

namespace OpenMatchupServer.Server
{

    public class MatchupManager
    {
        private PriorityQueue<int, GamePlayer> matchWatingQueue = new PriorityQueue<int, GamePlayer>();

        public MatchupManager()
        {

        }
    }
}