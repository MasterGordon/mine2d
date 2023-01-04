using Mine2d.engine.lib;
using Mine2d.game.core.data;
using Mine2d.game.core.tiles;

namespace Mine2d.game.core.world;

public class ChunkGenerator
{
    private static readonly WorldGenerator WG = new();
    public static Chunk CreateFilledChunk(int x, int y, STile fill)
    {
        var chunk = new Chunk(x, y);
        for (var i = 0; i < Constants.ChunkSize; i++)
        {
            for (var j = 0; j < Constants.ChunkSize; j++)
            {
                chunk.SetTile(i, j, fill);
            }
        }
        return chunk;
    }

    public static readonly OpenSimplexNoise Noise = new();
    public static readonly OpenSimplexNoise Noise2 = new();
    public static Chunk CreateChunk(int x, int y)
    {
        var fill = new STile
        {
            Id = (int)Tiles.Stone,
        };
        var chunk = new Chunk(x, y);
        for (var i = 0; i < Constants.ChunkSize; i++)
        {
            for (var j = 0; j < Constants.ChunkSize; j++)
            {
                var n = Noise.coherentNoise((int)((i + (x * 32)) * 0.6), (int)((j + (y * 32)) * 1), 0, 1, 25, 0.5f, 0.9f);
                var n2 = Noise2.coherentNoise((int)((i + (x * 32)) * 0.6), (int)((j + (y * 32)) * 1), 0, 1, 25, 0.5f, 0.9f);
                // Console.WriteLine(i * (x * 32) + "  "+ j * (y * 32));
                if (n > 0.08 || n2 > 0.08) continue;
                chunk.SetTile(i, j, fill);
            }
        }

        for (var i = 0; i < Constants.ChunkSize; i++)
        {
            for (var j = 0; j < Constants.ChunkSize; j++)
            {
                if (!chunk.HasTile(i, j))
                {
                    if (chunk.HasTile(i, j + 1) && chunk.GetTile(i, j + 1).Id == (int)Tiles.Stone)
                    {
                        if (new Random().NextInt64(0, 100) < 5)
                        {
                            chunk.SetTile(i, j, STile.From((int)Tiles.DripstoneUp));
                        }
                    }
                    if (chunk.HasTile(i, j - 1) && chunk.GetTile(i, j - 1).Id == (int)Tiles.Stone)
                    {
                        if (new Random().NextInt64(0, 100) < 25)
                        {
                            chunk.SetTile(i, j, STile.From((int)Tiles.DripstoneDown));
                        }
                    }
                }
                else
                {
                    fill.Id = (int)WG.GetRandomOreAt(j + (y * 32));
                    if (new Random().NextInt64(0, 100) < 1)
                    {
                        fill.Id = (int)Tiles.CoalOre;
                    }
                    if (fill.Id != 1)
                    {
                        var pX = i;
                        var pY = j;
                        var maxCount = new Random().NextInt64(fill.Id == (int)Tiles.CoalOre ? 3 : 1, fill.Id == (int)Tiles.CoalOre ? 10 : 6);
                        for (var count = 0; count < maxCount; count++)
                        {
                            var delta = new Random().NextInt64(-1, 2);
                            var dir = new Random().NextInt64(0, 2);
                            pX += (int)(dir == 0 ? delta : 0);
                            pY += (int)(dir == 1 ? delta : 0);
                            Console.WriteLine("Try Adding ader");
                            Console.WriteLine(pX + " " + pY);
                            if (chunk.HasTile(pX, pY))
                            {
                                chunk.SetTile(pX, pY, STile.From(fill.Id));
                                Console.WriteLine("Adding ader");
                            }
                        }
                    }
                    chunk.SetTile(i, j, fill);
                }
            }
        }
        return chunk;
    }

    public static void CreateSpawnChunk(int x, int y)
    {
        var chunk = CreateChunk(x, y);
        chunk.SetTile(16, 16, STile.From(0));
        chunk.SetTile(15, 16, STile.From(0));
        chunk.SetTile(16, 15, STile.From(0));
        chunk.SetTile(15, 15, STile.From(0));
        chunk.SetTile(17, 15, STile.From(0));
        chunk.SetTile(14, 15, STile.From(0));
        chunk.SetTile(17, 16, STile.From(0));
        chunk.SetTile(14, 16, STile.From(0));
        chunk.SetTile(16, 14, STile.From(0));
        chunk.SetTile(15, 14, STile.From(0));
        chunk.SetTile(17, 16, STile.From((int)Tiles.Workbench));
        chunk.SetTile(14, 17, STile.From((int)Tiles.Stone));
        chunk.SetTile(15, 17, STile.From((int)Tiles.Stone));
        chunk.SetTile(16, 17, STile.From((int)Tiles.Stone));
        chunk.SetTile(17, 17, STile.From((int)Tiles.Stone));
        Context.Get().GameState.World.AddChunk(chunk);
    }
}
