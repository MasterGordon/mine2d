using Mine2d.engine;
using Mine2d.engine.system.annotations;
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
    public static void Pickup()
    {
        var gameState = Context.Get().GameState;
        var world = gameState.World;
        foreach (var chunk in world.Chunks)
        {
            foreach (var player in gameState.Players)
            {

                var items = chunk.Value.Entities.RemoveAll(e =>
                {
                    return e is ItemEntity itemEntity && (player.Position + new Vector2(7, 3) - itemEntity.Position).LengthSquared() < 8 * 8;
                });
                if (items > 0)
                {
                    Context.Get().GameAudio.Play(Sound.ItemPickup);
                }
            }
        }
    }
}
