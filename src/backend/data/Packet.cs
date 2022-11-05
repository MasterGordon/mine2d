using System.Numerics;

readonly struct MovePacket
{
    readonly public string type = "move";
    readonly public string playerName;
    readonly public Vector2 movement;

    public MovePacket(string playerName, Vector2 movement)
    {
        this.playerName = playerName;
        this.movement = movement;
    }
}

readonly struct ConnectPacket
{
    public readonly string type = "connect";
    public readonly string playerName;
    public readonly Guid playerGuid;

    public ConnectPacket(string playerName, Guid playerGuid)
    {
        this.playerName = playerName;
        this.playerGuid = playerGuid;
    }
}

readonly struct TickPacket
{
    public readonly string type = "tick";
    public readonly uint tick;

    public TickPacket(uint tick)
    {
        this.tick = tick;
    }
}

readonly struct SelfMovedPacket
{
    public readonly string type = "selfMoved";
    public readonly Vector2 target;

    public SelfMovedPacket(Vector2 target)
    {
        this.target = target;
    }
}
