namespace Mine2d.game.backend.network.packets;

public class ConnectPacket : Packet
{
    public override PacketType Type => PacketType.Connect;

    public string PlayerName { get; init; }
    public Guid PlayerGuid { get; init; }
}