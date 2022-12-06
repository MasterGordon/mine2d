using Mine2d.engine.system;
using Mine2d.engine.system.annotations;
using Mine2d.game.core;
using Mine2d.game.core.data;
using Mine2d.game.core.data.entities;

namespace Mine2d.game.frontend.events;

public class DebugInput
{
    [EventListener(EventType.KeyDown)]
    public static void OnKeyDown(SDL_Event e)
    {
        if (e.key.keysym.scancode == SDL_Scancode.SDL_SCANCODE_K)
        {
            var gameState = Context.Get().GameState;
            var player = PlayerEntity.GetSelf();
            var chunk = gameState.World.GetChunkAt(player.Position);
            var item = new ItemEntity
            {
                Position = player.Position,
                ItemId = ItemId.Stone
            };
            chunk.AddEntity(item);
        }
    }
}
