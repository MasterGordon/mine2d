namespace Mine2d.game.backend.network.packets;

public class DisconnectPacket : Packet
{
    public override PacketType Type => PacketType.Disconnect; 
    
    public string PlayerName { get; init; }
    public string PlayerGuid { get; init; }
}