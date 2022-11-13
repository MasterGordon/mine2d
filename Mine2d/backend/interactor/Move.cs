using mine2d.backend.data;
using mine2d.core;
using mine2d.engine.system.annotations;

namespace mine2d.backend.interactor;

[InteractorAttribute]
public class Move
{
    [InteractionAttribute(InteractorKind.Hybrid, "move")]
    public static void MoveHybrid(MovePacket packet)
    {
        var ctx = Context.Get();
        var player = ctx.GameState.Players.Find(p => p.Name == packet.PlayerName);
        if (player != null)
        {
            player.Movement = packet.Movement * 4;
        }
    }

    [InteractionAttribute(InteractorKind.Hybrid, "tick")]
    public static void TickHybrid()
    {
        var ctx = Context.Get();
        ctx.GameState.Players.ForEach(PlayerEntity.Move);
        ctx.GameState.Players.ForEach(PlayerEntity.Collide);
    }

    [InteractionAttribute(InteractorKind.Client, "tick")]
    public static void SelfMovedClient()
    {
        var camera = Context.Get().FrontendGameState.Camera;
        camera.CenterOn(PlayerEntity.GetSelf().Position);
    }
}
