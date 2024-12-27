using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using OpenMatchupServer.Packets;
using OpenMatchupServer.Player;

namespace OpenMatchupServer.Server
{

    public class ServeEventRouter
    {
        private static ServeEventRouter _instance;

        private static readonly object _lock = new object();

        public delegate void ServeEventHandler(string message);
        private readonly Dictionary<string, ServeEventHandler> _eventHandlers = new();

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

        public void RegisterHandler(string message, ServeEventHandler handler)
        {
            if (_eventHandlers.ContainsKey(message))
            {
                _eventHandlers[message] += handler; // 기존 델리게이트에 추가
            }
            else
            {
                _eventHandlers[message] = handler; // 새로 등록
            }
        }

        public void HandleMessage(string eventKey, string message)
        {
            if (_eventHandlers.TryGetValue(eventKey, out var handler))
            {
                handler?.Invoke(message); // 델리게이트 호출
            }
            else
            {
                Console.WriteLine($"No handler registered for message: {message}");
            }
        }

        public void InitEventHandler()
        {
            // ServeEventHandler helloHandler = (msg) => Console.WriteLine($"Hello Handler: {msg}");
            // ServeEventHandler goodbyeHandler = (msg) => Console.WriteLine($"Goodbye Handler: {msg}");
            // RegisterHandler("hello", helloHandler);
            // RegisterHandler("goodbye", goodbyeHandler);
            RegisterHandler("ApplyMatchup", Event_ApplyMatchup);
        }

        public void EventExecution(string eventKey, string message)
        {
            HandleMessage(eventKey, message);
        }

        // Server Events . . .
        public void Event_ApplyMatchup(string msg)
        {
            PacketApplyMatchup packetApplyMatchup = new PacketApplyMatchup();

            packetApplyMatchup.Deserialize(msg);

            // Add to waiting container . . .
            int _pId = packetApplyMatchup.pID;

            GamePlayer applyer = PlayerManager.Instance.FindPlayerById(_pId);
            

            
        }


    }

}

