namespace mine2d.backend.data;

public interface IPacket
{
    public string Type { get; }
}

public struct MovePacket : IPacket
{
    public readonly string Type => "move";

    public readonly string PlayerName;
    public readonly Vector2 Movement;

    public MovePacket(string playerName, Vector2 movement)
    {
        this.PlayerName = playerName;
        this.Movement = movement;
    }
}

public readonly struct ConnectPacket : IPacket
{
    public readonly string Type => "connect";
    public readonly string PlayerName;
    public readonly Guid PlayerGuid;

    public ConnectPacket(string playerName, Guid playerGuid)
    {
        this.PlayerName = playerName;
        this.PlayerGuid = playerGuid;
    }
}

readonly struct TickPacket : IPacket
{
    public readonly string Type => "tick";
    public readonly uint Tick;

    public TickPacket(uint tick)
    {
        this.Tick = tick;
    }
}

readonly struct SelfMovedPacket : IPacket
{
    public readonly string Type => "selfMoved";
    public readonly Vector2 Target;

    public SelfMovedPacket(Vector2 target)
    {
        this.Target = target;
    }
}
