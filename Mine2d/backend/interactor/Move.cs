using mine2d.backend.data;
using mine2d.core;
using mine2d.engine.system.annotations;

namespace mine2d.backend.interactor;

[Interactor]
class Move
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
    public static void TickHybrid(TickPacket packet)
    {
        var ctx = Context.Get();
        ctx.GameState.Players.ForEach(PlayerEntity.Move);
        ctx.GameState.Players.ForEach(PlayerEntity.Collide);
    }

    [Interaction(InteractorKind.Client, "tick")]
    public static void SelfMovedClient(TickPacket packet)
    {
        var camera = Context.Get().FrontendGameState.Camera;
        camera.CenterOn(PlayerEntity.GetSelf().Position);
    }
}
