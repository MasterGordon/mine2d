using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class Move
{
    // [Interaction(InteractorKind.Hybrid, PacketType.Move)]
    // public static void MoveHybrid(MovePacket packet)
    // {
    //     var ctx = Context.Get();
    //     var player = ctx.GameState.Players.Find(p => p.Name == packet.PlayerName);
    //     if (player != null)
    //     {
    //         player.Movement += packet.Movement * 2;
    //     }
    // }

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
