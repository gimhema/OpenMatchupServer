using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace OpenMatchupServer.Server
{

    public class ServeEventHandler
    {
        private static ServeEventHandler _instance;

        private static readonly object _lock = new object();

        private ServeEventHandler()
        {
            
        }

        public static ServeEventHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServeEventHandler();
                        }
                    }
                }
                return _instance;
            }
        }
    }

}