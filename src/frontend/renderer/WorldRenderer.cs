class WorldRenderer : IRenderer
{
    public void Render()
    {
        var ctx = Context.Get();
        var world = ctx.GameState.World;
        var renderer = ctx.Renderer;
        var tileRegistry = ctx.TileRegistry;
        foreach (var (_, chunk) in world.Chunks)
        {
            for (int y = 0; y < Constants.ChunkSize; y++)
            {
                for (int x = 0; x < Constants.ChunkSize; x++)
                {
                    var tileId = chunk.GetTile(x, y);
                    var tile = tileRegistry.GetTile(tileId);
                    var chunkOffsetX = chunk.X * Constants.TileSize * Constants.ChunkSize;
                    var chunkOffsetY = chunk.Y * Constants.TileSize * Constants.ChunkSize;
                    tile.Render(x * 16 + chunkOffsetX, y * 16 + chunkOffsetY);
                }
            }
        }
    }
}
