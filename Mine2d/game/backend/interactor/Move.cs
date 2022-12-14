using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class Move
{
    [Interaction(InteractorKind.Hybrid, PacketType.Jump)]
    public static void MoveHybrid(JumpPacket packet)
    {
        var ctx = Context.Get();
        var player = ctx.GameState.Players.Find(p => p.Id == packet.PlayerId);
        if (player != null)
        {
            PlayerEntity.Jump(player);
        }
    }

    [Interaction(InteractorKind.Client, PacketType.Tick)]
    public static void SelfMovedClient()
    {
        var camera = Context.Get().FrontendGameState.Camera;
        camera.CenterOn(PlayerEntity.GetSelf().Position);
    }

    [Interaction(InteractorKind.Hybrid, PacketType.Tick)]
    public static void OnHybridTick()
    {
        var context = Context.Get();
        var gameState = context.GameState;

        gameState.Players.ForEach(player =>
        {
            var position = player.Position;
            PlayerEntity.Move(player);
            PlayerEntity.Collide(player);

            if (position != player.Position)
            {
                context.Backend.ProcessPacket(new PlayerMovedPacket
                {
                    PlayerGuid = player.Id,
                    Target = player.Position
                });
            }
        });
    }
}
