using Mine2d.game.core.data;

namespace Mine2d.game.backend.network.packets;

public class BlockBrokenPacket : Packet
{
    public override PacketType Type => PacketType.BlockBroken;

    public Guid PlayerGuid { get; init; }
    public Vector2 Target { get; init; }
    public STile Tile { get; init; }
}