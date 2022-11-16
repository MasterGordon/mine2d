using Mine2d.backend.data;
using Mine2d.core.data;
using Mine2d.engine.system.annotations;

namespace Mine2d.backend.interactor;

[Interactor]
public class Breaking
{
    [Interaction(InteractorKind.Hybrid, "tick")]
    public static void TickHybrid()
    {
        var ctx = Context.Get();
        ctx.GameState.Players.ForEach(player =>
            {
                player.MiningCooldown = Math.Max(0, player.MiningCooldown - 1);
                if (player.Mining != Vector2.Zero && player.MiningCooldown == 0 && ctx.GameState.World.HasChunkAt(player.Mining))
                {
                    var chunk = ctx.GameState.World.GetChunkAt(player.Mining);
                    var tile = chunk.GetTileAt(player.Mining);
                    var tileId = tile.Id;
                    if (tileId != 0)
                    {
                        player.MiningCooldown = 10;
                        var tileRegistry = ctx.TileRegistry;
                        var hardness = tileRegistry.GetTile(tileId).Hardness;
                        chunk.SetTileAt(player.Mining, tile with { Hits = tile.Hits + 1 });
                        if (tile.Hits >= hardness)
                        {
                            chunk.SetTileAt(player.Mining, STile.From(0));
                        }
                    }
                }
            }
        );
    }

    [Interaction(InteractorKind.Server, "break")]
    public static void BreakServer(BreakPacket packet)
    {
        var ctx = Context.Get();
        var player = ctx.GameState.Players.Find(p => p.Id == packet.PlayerGuid);
        if (player == null)
        {
            return;
        }
        player.Mining = packet.Target;
    }
}
