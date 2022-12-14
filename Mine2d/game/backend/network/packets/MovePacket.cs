namespace Mine2d.game.backend.network.packets;

public class MovePacket : Packet
{
    public override PacketType Type => PacketType.Move;

    public string PlayerName { get; init; }
    public Vector2 Movement { get; init; }
}