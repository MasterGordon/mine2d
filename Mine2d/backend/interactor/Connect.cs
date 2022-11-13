using mine2d.backend.data;
using mine2d.engine.system.annotations;
using mine2d.state;

namespace mine2d.backend.interactor;

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
                    Position = new Vector2(20, 16 * 16),
                    Movement = new Vector2(0, 0)
                }
            );
        }
    }
}
