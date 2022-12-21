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

    // [Interaction(InteractorKind.Hybrid, PacketType.Tick)]
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
                ctx.Backend.ProcessPacket(new PlayerMovedPacket
                {
                    PlayerGuid = player.Id,
                    Target = player.Position
                });
            }
        }
    }

    [Interaction(InteractorKind.Client, PacketType.Tick)]
    public static void SelfMovedClient()
    {
        var camera = Context.Get().FrontendGameState.Camera;
        camera.CenterOn(PlayerEntity.GetSelf().Position);
    }
}
