using Mine2d.engine;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;
using Mine2d.game.core.data;
using Mine2d.game.core.data.entities;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class ItemPhysics
{
    [Interaction(InteractorKind.Hybrid, PacketType.Tick)]
    public static void TickHybrid()
    {
        var gameState = Context.Get().GameState;
        var world = gameState.World;
        foreach (var chunk in world.Chunks)
        {
            foreach (var entity in chunk.Value.Entities)
            {
                if (entity is ItemEntity itemEntity)
                {
                    itemEntity.Velocity += new Vector2(0, 0.1f);
                    itemEntity.Velocity *= new Vector2(0.7f, 1f);
                    var nextPos = itemEntity.Position + itemEntity.Velocity;
                    if (world.HasChunkAt(nextPos) && world.GetChunkAt(nextPos).HasSolidTileAt(nextPos))
                    {
                        itemEntity.Velocity = new Vector2(0, 0);
                        continue;
                    }
                    itemEntity.Position = nextPos;
                }
            }
        }
    }

    [Interaction(InteractorKind.Hybrid, PacketType.Tick)]
    public static void Pickup()
    {
        var gameState = Context.Get().GameState;
        var world = gameState.World;
        foreach (var chunk in world.Chunks)
        {
            foreach (var player in gameState.Players)
            {
                var items = chunk.Value.Entities.Where(e =>
                {
                    return e is ItemEntity itemEntity &&
                    (player.Position + new Vector2(7, 3) - itemEntity.Position).LengthSquared() < 8 * 8 &&
                    player.Inventory.PickupItemStack(new ItemStack { Id = itemEntity.ItemId, Count = 1 });
                }).ToList();
                if (items.Any())
                {
                    Context.Get().GameAudio.Play(Sound.ItemPickup);
                }
                _ = chunk.Value.Entities.RemoveAll(items.Contains);
            }
        }
    }
}
