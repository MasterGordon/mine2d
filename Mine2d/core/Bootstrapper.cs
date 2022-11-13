using mine2d.core.data;
using mine2d.core.tiles;
using mine2d.core.world;

namespace mine2d.core;

public class Bootstrapper
{
    public static void Bootstrap()
    {
        var ctx = Context.Get();
        ctx.GameState.World = new World();
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateFilledChunk(0, 1, STile.From(Tiles.Stone)));
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateFilledChunk(1, 1, STile.From(Tiles.Stone)));
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateFilledChunk(1, 0, STile.From(Tiles.Stone)));
    }
}
