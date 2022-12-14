using System.Text;
using Mine2d.game.backend.network.packets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConnectPacket = Mine2d.game.backend.network.packets.ConnectPacket;
using TickPacket = Mine2d.game.backend.network.packets.TickPacket;

namespace Mine2d.game.backend.network;

public class Converter
{
    public static Packet ParsePacket(byte[] bytes)
    {
        var jsonString = Encoding.UTF8.GetString(bytes);
        return ParsePacket(jsonString);
    }

    public static Packet ParsePacket(string jsonString)
    {
        var parsedRaw = JObject.Parse(jsonString);
        var packetType = parsedRaw
            .GetValue("type")?
            .Value<PacketType>();
        
        if (packetType == null)
            throw new PacketException("Packet has no type");
        
        Console.WriteLine("Packet type: " + packetType);
        Packet packet = packetType switch
        {
            PacketType.Connect => parsedRaw.ToObject<ConnectPacket>(),
            PacketType.Disconnect => parsedRaw.ToObject<DisconnectPacket>(),
            PacketType.Tick => parsedRaw.ToObject<TickPacket>(),
            _ => throw new PacketException($"Unknown packet type: {packetType}")
        };
        
        if (packet is null)
            throw new PacketException($"Failed to parse packet of type: {packetType}");

        return packet;
    }

    public static byte[] SerializePacket(ValueType packet)
    {
        var jsonString = JsonConvert.SerializeObject(packet);
        return Encoding.UTF8.GetBytes(jsonString);
    }
}
