using Mine2d.core.data;
using Mine2d.core.tiles;
using Mine2d.core.world;

namespace Mine2d.core;

public class Bootstrapper
{
    public static void Bootstrap()
    {
        var ctx = Context.Get();
        ctx.GameState.World = new World();
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateFilledChunk(0, 1, STile.From(Tiles.Stone)));
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateSpawnChunk(1000, 10));
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateFilledChunk(1, 0, STile.From(Tiles.Stone)));
    }
}
