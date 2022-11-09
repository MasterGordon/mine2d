class Bootstrapper
{
    public static void Bootstrap()
    {
        var ctx = Context.Get();
        ctx.GameState.World = new World();
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateFilledChunk(0, 1, STile.From(Tiles.stone)));
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateFilledChunk(1, 1, STile.From(Tiles.stone)));
        ctx.GameState.World.AddChunk(ChunkGenerator.CreateFilledChunk(1, 0, STile.From(Tiles.stone)));
    }
}
