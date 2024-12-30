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
    enum MatchSetting
    {
        MaxNumTeamMembers = 5,
        MaxNumCompetition = 2
    }

    public class Team
    {
        List<GamePlayer> members;

        public Team()
        {  
            members= new List<GamePlayer>();
            members.Clear();
        }

        public void Push(GamePlayer newPlayer)
        {
            if(members.Count >= (int)MatchSetting.MaxNumTeamMembers) {
                return;
            }
            members.Add(newPlayer);
        }

        public void Clear()
        {
            members.Clear();
        }

    }

    public class GameMatch
    {
        List<Team> competition;

        public GameMatch()
        {
            competition = new List<Team>();
            competition.Clear();
        }

        public void Push(Team newTeam)
        {
            if (competition.Count >= (int)MatchSetting.MaxNumCompetition){
                return;
            }
            competition.Add(newTeam);

        }

        public void Clear()
        {
            competition.Clear();
        }
    }

    public class MatchupManager
    {
        // public Dictionary<int, GamePlayer> waitingPlayers = new Dictionary<int, GamePlayer>();
        public Queue<GamePlayer> waitingQueue = new Queue<GamePlayer>();

        private static MatchupManager _instance;

        private static readonly object _lock = new object();

        private AlgorithmSelector algoSelector = new AlgorithmSelector();

        private MatchupManager()
        {
            
        }

        public void Init()
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

        public async Task Search()
        {
            while(true)
            {
                // Search Loop . . . .
                await Task.Delay(1000);
            }
        }

        public void AddNewApply(GamePlayer applyer)
        {
            waitingQueue.Enqueue(applyer);
        }


    }
}