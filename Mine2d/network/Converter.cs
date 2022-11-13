using System.Text;
using mine2d.backend.data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mine2d.network;

class Converter
{
    public static ValueType ParsePacket(byte[] bytes)
    {
        var jsonString = Encoding.UTF8.GetString(bytes);
        return ParsePacket(jsonString);
    }

    public static ValueType ParsePacket(string jsonString)
    {
        var parsedRaw = JObject.Parse(jsonString);
        var type = parsedRaw.GetValue("type");
        if (type == null)
        {
            throw new Exception("Packet has no type");
        }
        var packetType = type.Value<string>();
        Console.WriteLine("Packet type: " + packetType);
        return packetType switch
        {
            "move" => parsedRaw.ToObject<MovePacket>(),
            "connect" => parsedRaw.ToObject<ConnectPacket>(),
            _ => throw new Exception("Unknown packet type")
        };
    }

    public static byte[] SerializePacket(ValueType packet)
    {
        var jsonString = JsonConvert.SerializeObject(packet);
        return Encoding.UTF8.GetBytes(jsonString);
    }
}