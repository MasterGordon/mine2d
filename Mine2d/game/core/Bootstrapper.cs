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
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateFilledChunk(0, 1, STile.From(Tiles.Stone)));
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateSpawnChunk(1000, 10));
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateFilledChunk(1, 0, STile.From(Tiles.Stone)));
    }
}
