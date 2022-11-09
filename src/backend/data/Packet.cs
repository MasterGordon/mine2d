namespace mine2d.backend.data;

public interface IPacket
{
    string Type { get; }
}

readonly struct MovePacket : IPacket
{
    public string Type => "move";

    readonly public string PlayerName;
    readonly public Vector2 Movement;

    public MovePacket(string playerName, Vector2 movement)
    {
        this.PlayerName = playerName;
        this.Movement = movement;
    }
}

readonly struct ConnectPacket
{
    public readonly string Type = "connect";
    public readonly string PlayerName;
    public readonly Guid PlayerGuid;

    public ConnectPacket(string playerName, Guid playerGuid)
    {
        this.PlayerName = playerName;
        this.PlayerGuid = playerGuid;
    }
}

readonly struct TickPacket
{
    public readonly string Type = "tick";
    public readonly uint Tick;

    public TickPacket(uint tick)
    {
        this.Tick = tick;
    }
}

readonly struct SelfMovedPacket
{
    public readonly string Type = "selfMoved";
    public readonly Vector2 Target;

    public SelfMovedPacket(Vector2 target)
    {
        this.Target = target;
    }
}