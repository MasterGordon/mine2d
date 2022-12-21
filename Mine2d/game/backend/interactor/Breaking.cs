using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core;
using Mine2d.game.core.data;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class Breaking
{
    [Interaction(InteractorKind.Hybrid, PacketType.Tick)]
    public static void TickHybrid()
    {
        var ctx = Context.Get();
        foreach (var player in ctx.GameState.Players)
        {
            player.MiningCooldown = Math.Max(0, player.MiningCooldown - 1);
            if (player.Mining == Vector2.Zero)
            {
                continue;
            }

            if (player.MiningCooldown > 0)
            {
                continue;
            }

            if ((player.GetCenter() - player.Mining).LengthSquared() > Constants.BreakDistance * Constants.BreakDistance)
            {
                continue;
            }

            if (ctx.GameState.World.HasChunkAt(player.Mining))
            {
                var chunk = ctx.GameState.World.GetChunkAt(player.Mining);
                var tile = chunk.GetTileAt(player.Mining);
                var tileId = tile.Id;
                if (tileId != 0)
                {
                    player.MiningCooldown = 10;
                    var tileRegistry = ctx.TileRegistry;
                    var hardness = tileRegistry.GetTile(tileId).Hardness;
                    if(tile.Hits == 0) {
                        ctx.GameState.World.Cracks.Enqueue(new CrackQueueEntry
                        {
                            Pos = player.Mining,
                            ResetTime = DateTime.Now.AddSeconds(5)
                        });
                    }
                    chunk.SetTileAt(player.Mining, tile with { Hits = tile.Hits + 1 });
                    if (tile.Hits >= hardness)
                    {
                        var blockPos = new Vector2((int)Math.Floor(player.Mining.X / 16) * 16, (int)Math.Floor(player.Mining.Y / 16) * 16);
                        ctx.Backend.ProcessPacket(new BlockBrokenPacket
                        {
                            PlayerGuid = player.Id,
                            Target = blockPos,
                            Tile = tile
                        });
                        chunk.SetTileAt(player.Mining, STile.From(0));
                    }
                }
            }
        }
        Context.Get().GameState.World.ProcessCrackQueue();
    }

    [Interaction(InteractorKind.Server, PacketType.Break)]
    public static void BreakServer(BreakPacket packet)
    {
        var ctx = Context.Get();
        var player = ctx.GameState.Players.Find(p => p.Id == packet.PlayerGuid);
        if (player == null)
        {
            return;
        }
        if (packet.Source == BreakSource.Move && player.Mining == Vector2.Zero)
        {
            return;
        }
        player.Mining = packet.Target;
    }

    [Interaction(InteractorKind.Server, PacketType.BlockBroken)]
    public static void BreakServer(BlockBrokenPacket packet)
    {
        var ctx = Context.Get();
        var tile = ctx.TileRegistry.GetTile(packet.Tile.Id);
        tile.DropItem(packet.Target);
    }
}
