using Mine2d.engine;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.data;
using Mine2d.game.core.data;
using Mine2d.game.core.data.entities;

namespace Mine2d.game.backend.interactor;

[Interactor]
public class ItemPhysics
{

    [Interaction(InteractorKind.Hybrid, "tick")]
    public static void TickHybrid()
    {
        var gameState = Context.Get().GameState;
        var world = gameState.World;
        foreach (var chunk in world.Chunks)
        {
            var entities = chunk.Value.Entities;
            foreach (var entity in entities)
            {
                if (entity is ItemEntity itemEntity)
                {
                    itemEntity.Velocity += new Vector2(0, 0.1f);
                    itemEntity.Velocity *= new Vector2(0.7f, 1f);
                    var nextPos = itemEntity.Position + itemEntity.Velocity;
                    if (world.HasChunkAt(nextPos) && world.GetChunkAt(nextPos).HasTileAt(nextPos))
                    {
                        itemEntity.Velocity = new Vector2(0, 0);
                        continue;
                    }
                    itemEntity.Position = nextPos;
                }
            }
        }
    }

    [Interaction(InteractorKind.Hybrid, "tick")]
    public static void Pickup(TickPacket tickPacket)
    {
        var gameState = Context.Get().GameState;
        var world = gameState.World;
        foreach (var chunk in world.Chunks)
        {
            foreach (var player in gameState.Players)
            {

                var items = chunk.Value.Entities.Where(e =>
                {
                    Console.WriteLine("Where");
                    return e is ItemEntity itemEntity &&
                    (player.Position + new Vector2(7, 3) - itemEntity.Position).LengthSquared() < 8 * 8 &&
                    player.inventory.PickupItemStack(new ItemStack { Id = itemEntity.ItemId, Count = 1 });
                });
                if (items.Any())
                {
                    Context.Get().GameAudio.Play(Sound.ItemPickup);
                    Console.WriteLine(tickPacket.Tick + "  " + items.Count());
                }
                _ = chunk.Value.Entities.RemoveAll(e => items.Contains(e));
            }
        }
    }
}
