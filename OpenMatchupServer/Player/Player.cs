using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace OpenMatchupServer.Player
{
    public class GamePlayer
    {
        // 식별자
        public int id = 0;
        
        // 이름
        public string name = "";

        // 소켓
        public Socket playerSocket = null;


        public GamePlayer(string _name, Socket _socket) 
        {
            name = _name;
            playerSocket = _socket;
        }

        public void SetId(int _id)
        {
            id = _id;
        }

    }


}