using Mine2d.engine.system.annotations;
using Mine2d.game.backend.data;
using Mine2d.game.state;

namespace Mine2d.game.backend.interactor;

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
                    Position = new Vector2(512244, 5390),
                    Movement = new Vector2(0, 0)
                }
            );
        }
    }
}
