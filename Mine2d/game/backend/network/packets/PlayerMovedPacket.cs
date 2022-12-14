namespace Mine2d.game.backend.network.packets;

public class PlayerMovedPacket : Packet
{
    public override PacketType Type => PacketType.PlayerMoved;

    public Guid PlayerGuid { get; init; }
    public Vector2 Target { get; init; }
}