using Mine2d.game.core.data;
using Mine2d.game.core.tiles;
using Mine2d.game.core.world;

namespace Mine2d.game.core;

public class Bootstrapper
{
    public static void Bootstrap()
    {
        var ctx = Context.Get();
        ctx.GameState.World = new World();
        ChunkGenerator.CreateSpawnChunk(1000, 10);
    }
}
