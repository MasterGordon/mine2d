using Mine2d.game.core.data;
using Mine2d.game.core.tiles;

namespace Mine2d.game.core.world;

public class ChunkGenerator
{
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
                if (new Random().Next(0, 100) < 10)
                {
                    fill.Id = (int)Tiles.Ore1;
                }
                else if (new Random().Next(0, 100) < 10)
                {
                    fill.Id = (int)Tiles.Ore2;
                }
                else if (new Random().Next(0, 100) < 10)
                {
                    fill.Id = (int)Tiles.Ore3;
                }
                else if (new Random().Next(0, 100) < 10)
                {
                    fill.Id = (int)Tiles.Ore4;
                }
                else
                {
                    fill.Id = (int)Tiles.Stone;
                }
                chunk.SetTile(i, j, fill);
            }
        }
        return chunk;
    }

    public static Chunk CreateSpawnChunk(int x, int y)
    {
        var chunk = new Chunk(x, y);
        for (var i = 0; i < Constants.ChunkSize; i++)
        {
            for (var j = 0; j < Constants.ChunkSize; j++)
            {
                chunk.SetTile(i, j, STile.From(Tiles.Stone));
            }
        }
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
        return chunk;
    }
}
