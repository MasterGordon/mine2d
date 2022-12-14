namespace Mine2d.game.backend.network.packets;

public class PlacePacket : Packet
{
    public override PacketType Type => PacketType.Place;

    public Guid PlayerGuid { get; init; }
    public Vector2 Target { get; init; }
    public int Slot { get; init; }
}
