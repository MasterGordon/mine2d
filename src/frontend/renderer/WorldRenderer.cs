using mine2d.core;

namespace mine2d.frontend.renderer;

class WorldRenderer : IRenderer
{
    public void Render()
    {
        var ctx = Context.Get();
        var world = ctx.GameState.World;
        var tileRegistry = ctx.TileRegistry;
        foreach (var (_, chunk) in world.Chunks)
        {
            for (var y = 0; y < Constants.ChunkSize; y++)
            {
                for (var x = 0; x < Constants.ChunkSize; x++)
                {
                    var stile = chunk.GetTile(x, y);
                    if (stile.Id == 0)
                    {
                        continue;
                    }

                    var tile = tileRegistry.GetTile(stile.Id);
                    var chunkOffsetX = chunk.X * Constants.TileSize * Constants.ChunkSize;
                    var chunkOffsetY = chunk.Y * Constants.TileSize * Constants.ChunkSize;
                    tile.Render(x * 16 + chunkOffsetX, y * 16 + chunkOffsetY, stile);
                }
            }
        }
    }
}