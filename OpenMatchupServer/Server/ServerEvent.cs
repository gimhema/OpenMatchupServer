using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

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
        }

        public void EventExecution(string eventKey, string message)
        {
            HandleMessage(eventKey, message);
        }


    }

}


/*

string jsonString1 = @"
        {
          ""id"": 1,
          ""name"": ""Alice"",
          ""age"": 25
        }";

        string jsonString2 = @"
        {
          ""id"": 1,
          ""title"": ""Engineer"",
          ""department"": ""Development""
        }";

        // JSON 문자열을 JObject로 파싱
        var jsonObject1 = JObject.Parse(jsonString1);
        var jsonObject2 = JObject.Parse(jsonString2);

        // 공통 ID 확인
        if ((int)jsonObject1["id"] == (int)jsonObject2["id"])
        {
            Console.WriteLine($"ID: {jsonObject1["id"]}");
            Console.WriteLine($"Name: {jsonObject1["name"]}, Age: {jsonObject1["age"]}");
            Console.WriteLine($"Title: {jsonObject2["title"]}, Department: {jsonObject2["department"]}");
        }
        else
        {
            Console.WriteLine("IDs do not match.");
        }

*/