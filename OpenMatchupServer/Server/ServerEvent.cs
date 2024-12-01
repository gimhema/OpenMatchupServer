using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace OpenMatchupServer.Server
{

    public class ServeEventRouter
    {
        private static ServeEventRouter _instance;

        private static readonly object _lock = new object();

        public delegate void ServeEventHandler(string message);

        private ServeEventRouter()
        {
            
        }

        public static ServeEventRouter Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServeEventRouter();
                        }
                    }
                }
                return _instance;
            }
        }
    }

}