namespace Mine2d.game.backend.network.packets;

public enum PacketType
{
    Connect,
    Disconnect,
    Tick,
    Move,
    SelfMoved,
    Break,
    PlayerMoved,
    BlockBroken,
    Place
}
