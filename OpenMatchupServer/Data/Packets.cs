using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;



namespace OpenMatchupServer.Packets
{
    abstract class Packet
    {

        // JSON -> Packet
        public abstract void Deserialize(string data);

        // Packet -> Json
        public abstract string Serialize();
    }

    class PacketApplyMatchup : Packet
    {
        public override void Deserialize(string data)
        {
            throw new NotImplementedException();
        }

        public override string Serialize()
        {
            throw new NotImplementedException();
        }
    }
}