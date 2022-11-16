using Mine2d.backend.data;
using Mine2d.engine.system.annotations;
using Mine2d.state;

namespace Mine2d.backend.interactor;

[Interactor]
public class Connect
{
    [Interaction(InteractorKind.Server, "connect")]
    public static void ConnectServer(ConnectPacket packet)
    {
        var ctx = Context.Get();
        var player = ctx.GameState.Players.Find(p => p.Name == packet.PlayerName);
        if (player == null)
        {
            ctx.GameState.Players.Add(
                new Player
                {
                    Name = packet.PlayerName,
                    Id = packet.PlayerGuid,
                    Position = new Vector2(32 * 16 * 1000 + 16 * 14.5f, 32 * 16 * 10 + 16 * 14.5f),
                    Movement = new Vector2(0, 0)
                }
            );
        }
    }
}
