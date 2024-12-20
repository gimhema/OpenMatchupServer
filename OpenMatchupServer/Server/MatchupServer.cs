using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using OpenMatchupServer.Player;
using OpenMatchupServer.Packets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpenMatchupServer.Server
{
    public class MatchupServer
    {
        private static ManualResetEvent allDone = new ManualResetEvent(false);

        public MatchupServer()
        {

        }

        
        public static string GetFunctionIdByJString(string data)
        {
            try
            {
                // JSON 데이터를 파싱
                var result = JObject.Parse(data);

                // "id" 필드가 존재하는지 확인하고 값을 반환
                if (result["id"] != null)
                {
                    return result["id"].ToString();
                }
                else
                {
                    throw new Exception("The 'id' field is missing in the JSON data.");
                }
            }
            catch (JsonReaderException ex)
            {
                // JSON 파싱 관련 예외 처리
                return $"Invalid JSON format: {ex.Message}";
            }
            catch (Exception ex)
            {
                // 일반적인 예외 처리
                return $"Error: {ex.Message}";
            }
        }

        public static void StartListening()
        {
            // 서버가 바인드할 로컬 엔드포인트 설정
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 11000);

            // TCP 소켓 생성
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                Console.WriteLine("서버가 시작되었습니다. 연결 대기 중...");

                while (true)
                {
                    // 연결 이벤트 초기화
                    allDone.Reset();

                    // 비동기 연결 대기
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    // 연결 완료 대기
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"예외 발생: {e}");
            }
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // 연결 완료 신호 설정
            allDone.Set();

            // 클라이언트 소켓 가져오기
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // 상태 객체 생성
            StateObject state = new StateObject();
            state.workSocket = handler;

            // 데이터 수신 시작
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);

            GamePlayer newPlayer = new GamePlayer("", listener);
            PlayerManager.Instance.AddNewPlayer(newPlayer);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // 수신된 데이터의 바이트 수 가져오기
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // 클라이언트로부터의 데이터를 저장
                state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));

                // 전체 메시지 확인
                string content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    Console.WriteLine($"수신된 데이터: {content}");

                    // 추가적으로 content에서 key 파싱하는 과정 필요함
                    
                    string key = GetFunctionIdByJString(content);

                    ServeEventRouter.Instance.EventExecution(key, content);

                    // 응답 전송
                    Send(handler, "서버에서 받은 메시지: " + content);
                }
                else
                {
                    // 추가 데이터 수신
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private static void Send(Socket handler, string data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);

            // 응답 데이터 전송
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;

                // 데이터 전송 완료
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine($"전송된 바이트 수: {bytesSent}");

                // 소켓 종료
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"예외 발생: {e}");
            }
        }

        public async void Run()
        {
            var asyncTasks = new List<Task>
            {
                MatchupManager.Instance.Search()
            };

            StartListening();

            await Task.WhenAll(asyncTasks);
        }

        public class StateObject
        {
            // 클라이언트 소켓
            public Socket workSocket = null;
            // 수신 버퍼 크기
            public const int BufferSize = 1024;
            // 수신 버퍼
            public byte[] buffer = new byte[BufferSize];
            // 수신된 데이터 저장
            public StringBuilder sb = new StringBuilder();
        }
    }
}