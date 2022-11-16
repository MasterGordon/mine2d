using Mine2d.backend.data;
using Mine2d.core;
using Mine2d.engine.system.annotations;

namespace Mine2d.backend.interactor;

[Interactor]
public class Move
{
    [Interaction(InteractorKind.Hybrid, "move")]
    public static void MoveHybrid(MovePacket packet)
    {
        var ctx = Context.Get();
        var player = ctx.GameState.Players.Find(p => p.Name == packet.PlayerName);
        if (player != null)
        {
            player.Movement = packet.Movement * 4;
        }
    }

    [Interaction(InteractorKind.Hybrid, "tick")]
    public static void TickHybrid()
    {
        var ctx = Context.Get();
        var fromPositions = new Dictionary<Guid, Vector2>();
        foreach (var player in ctx.GameState.Players)
        {
            fromPositions.Add(player.Id, player.Position);
        }
        ctx.GameState.Players.ForEach(PlayerEntity.Move);
        ctx.GameState.Players.ForEach(PlayerEntity.Collide);
        foreach (var player in ctx.GameState.Players)
        {
            if (player.Position != fromPositions[player.Id])
            {
                ctx.Backend.ProcessPacket(new PlayerMovedPacket(player.Id, player.Position));
            }
        }
    }

    [Interaction(InteractorKind.Client, "tick")]
    public static void SelfMovedClient()
    {
        var camera = Context.Get().FrontendGameState.Camera;
        camera.CenterOn(PlayerEntity.GetSelf().Position);
    }
}
