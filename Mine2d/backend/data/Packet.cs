using Mine2d.core.data;

namespace Mine2d.backend.data;

public interface IPacket
{
    public string Type { get; }
}

public struct MovePacket : IPacket
{
    public readonly string Type => "move";

    public readonly string PlayerName { get; }
    public readonly Vector2 Movement { get; }

    public MovePacket(string playerName, Vector2 movement)
    {
        this.PlayerName = playerName;
        this.Movement = movement;
    }
}

public readonly struct ConnectPacket : IPacket
{
    public readonly string Type => "connect";
    public readonly string PlayerName { get; }
    public readonly Guid PlayerGuid { get; }

    public ConnectPacket(string playerName, Guid playerGuid)
    {
        this.PlayerName = playerName;
        this.PlayerGuid = playerGuid;
    }
}

public readonly struct TickPacket : IPacket
{
    public readonly string Type => "tick";
    public readonly uint Tick { get; }

    public TickPacket(uint tick)
    {
        this.Tick = tick;
    }
}

public readonly struct SelfMovedPacket : IPacket
{
    public readonly string Type => "selfMoved";
    public readonly Vector2 Target { get; }

    public SelfMovedPacket(Vector2 target)
    {
        this.Target = target;
    }
}

public readonly struct BreakPacket : IPacket
{
    public readonly string Type => "break";
    public readonly Guid PlayerGuid { get; }
    public readonly Vector2 Target { get; }

    public BreakPacket(Guid playerGuid, Vector2 target)
    {
        this.PlayerGuid = playerGuid;
        this.Target = target;
    }
}

public readonly struct PlayerMovedPacket : IPacket
{
    public readonly string Type => "playerMoved";
    public readonly Guid PlayerId { get; }
    public readonly Vector2 Target { get; }

    public PlayerMovedPacket(Guid playerId, Vector2 target)
    {
        this.PlayerId = playerId;
        this.Target = target;
    }
}

public readonly struct BlockBrokenPacket : IPacket
{
    public readonly string Type => "blockBroken";
    public readonly Guid PlayerId { get; }
    public readonly Vector2 Target { get; }
    public readonly STile Tile { get; }

    public BlockBrokenPacket(Guid playerId, Vector2 target, STile tile)
    {
        this.PlayerId = playerId;
        this.Target = target;
        this.Tile = tile;
    }
}
