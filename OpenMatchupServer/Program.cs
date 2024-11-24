// See https://aka.ms/new-console-template for more information
using OpenMatchupServer.Player;
using OpenMatchupServer.Server;
using OpenMatchupServer.Algo;



Console.WriteLine("Server Start . . .");

MatchupServer server = new MatchupServer();

server.Run();
