using mine2d.backend.data;
using mine2d.engine.system.annotations;

namespace mine2d.backend.interactor;

[Interactor]
class Breaking
{
    [Interaction(InteractorKind.Hybrid, "tick")]
    public static void TickHybrid(TickPacket packet)
    {
        var ctx = Context.Get();
        ctx.GameState.Players.ForEach(player =>
            {
                player.MiningCooldown = Math.Max(0, player.MiningCooldown - 1);
                if (player.Mining != Vector2.Zero && player.MiningCooldown == 0)
                {
                    var chunk = ctx.GameState.World.GetChunkAt(player.Mining);
                    // chunk.SetTileAt(player.Mining, tile with { Hits = tile.Hits + 1 });
                    // var tile = chunk.GetTileAt(amp);
                    // if (tile.Hits >= hardness)
                    // {
                    //     chunk.SetTileAt(amp, STile.From(0));
                    // }

                }
            }
        );
    }
}