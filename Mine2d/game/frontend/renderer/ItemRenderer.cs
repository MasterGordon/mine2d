using Mine2d.engine;
using Mine2d.game.core.data;
using Mine2d.game.core.data.entities;

namespace Mine2d.game.frontend.renderer;

public class ItemRenderer : IRenderer
{
    public void Render()
    {
        var gameState = Context.Get().GameState;
        var world = gameState.World;
        foreach (var chunk in world.Chunks)
        {
            renderChunk(chunk.Value);
        }
    }

    private static void renderChunk(Chunk chunk)
    {
        var entities = chunk.Entities;
        foreach (var entity in entities)
        {
            if (entity is ItemEntity itemEntity)
            {
                renderItem(itemEntity);
            }
        }
    }

    private static void renderItem(ItemEntity itemEntity)
    {
        var item = itemEntity.ItemId;
        var position = itemEntity.Position;
        Context.Get().ItemRegistry.GetItem(item).Render(position);
    }
}
