using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace OpenMatchupServer.Packets
{
    abstract class Packet
    {
        public int funtionId {get; set;} = 0;
        // JSON -> Packet
        public abstract void Deserialize(string data);

        // Packet -> Json
        public abstract string Serialize();
    }

    class PacketApplyMatchup : Packet
    {
        private int pID {get; set;} = 0; 
        private string name {get; set;} = "";

        private int ratingPoint {get; set;} = 0;

        public PacketApplyMatchup()
        {
            
        }

        // data -> message class
        public override void Deserialize(string data)
        {
            try
            {
                var result = JObject.Parse(data);

                if (result["id"] != null &&
                    result["pID"] != null &&
                    result["userName"] != null &&
                    result["ratingPoint"] != null)
                {
                    funtionId = int.Parse(result["id"].ToString());
                    pID = int.Parse(result["pID"].ToString());
                    name = result["userName"].ToString();
                    ratingPoint = int.Parse(result["ratingPoint"].ToString());
                }
                else
                {
                    throw new Exception("The 'id' field is missing in the JSON data.");
                }
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine($"Invalid JSON format: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        // message class -> data
        public override string Serialize()
        {
            throw new NotImplementedException();
        }
    }
}