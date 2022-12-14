namespace Mine2d.game.backend.network.packets;

public class TickPacket : Packet
{
    public override PacketType Type => PacketType.Tick;

    public uint Tick { get; init; }
}