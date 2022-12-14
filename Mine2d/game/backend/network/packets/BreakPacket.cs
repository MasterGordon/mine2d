namespace Mine2d.game.backend.network.packets;

public class BreakPacket : Packet
{
    public override PacketType Type => PacketType.Break;

    public Guid PlayerGuid { get; init; }
    public Vector2 Target { get; init; }
}