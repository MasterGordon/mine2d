using mine2d.core.data;

namespace mine2d.core.world;

class ChunkGenerator
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
}