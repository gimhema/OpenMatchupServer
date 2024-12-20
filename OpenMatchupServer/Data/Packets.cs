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
        public string funcIdentifier {get; set;} = "0";
        // JSON -> Packet
        public abstract void Deserialize(string data);

        // Packet -> Json
        public abstract string Serialize();
    }

    class PacketApplyMatchup : Packet
    {
        public int pID {get; set;} = 0; 
        public string name {get; set;} = "";

        public int ratingPoint {get; set;} = 0;

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
                    funcIdentifier = result["id"].ToString();
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
            try
            {
                // JObject를 사용해 JSON 문자열 생성
                var jsonObject = new JObject
                {
                    ["id"] = funcIdentifier,
                    ["pID"] = pID,
                    ["userName"] = name,
                    ["ratingPoint"] = ratingPoint
                };

                // JSON 문자열로 변환
                return jsonObject.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during serialization: {ex.Message}");
                return string.Empty; // 오류 발생 시 빈 문자열 반환
            }
        }

    }
}