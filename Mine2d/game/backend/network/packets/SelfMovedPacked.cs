namespace Mine2d.game.backend.network.packets;

public class SelfMovedPacked : Packet
{
    public override PacketType Type => PacketType.SelfMoved;

    public Vector2 Target { get; init; }
}