namespace Mine2d.game.core.data;

public struct CrackQueueEntry
{
    public Vector2 Pos { get; set; }
    public DateTime ResetTime { get; set; }
}

public class World
{
    public Dictionary<string, Chunk> Chunks { get; set; } = new Dictionary<string, Chunk>();
    public Queue<CrackQueueEntry> Cracks { get; set; } = new();

    public void AddChunk(Chunk chunk)
    {
        this.Chunks.Add(chunk.X + "," + chunk.Y, chunk);
    }

    public Chunk GetChunk(Vector2 pos)
    {
        return this.GetChunk((int)pos.X, (int)pos.Y);
    }

    public Chunk GetChunk(int x, int y)
    {
        return this.Chunks[x + "," + y];
    }

    public bool HasChunk(Vector2 pos)
    {
        return this.HasChunk((int)pos.X, (int)pos.Y);
    }

    public bool HasChunk(int x, int y)
    {
        return this.Chunks.ContainsKey(x + "," + y);
    }

    public Chunk GetChunkAt(Vector2 pos)
    {
        return this.GetChunkAt((int)pos.X, (int)pos.Y);
    }

    public Chunk GetChunkAt(int x, int y)
    {
        var chunkPos = ToChunkPos(new Vector2(x, y));
        return this.Chunks[chunkPos.X + "," + chunkPos.Y];
    }

    public bool HasChunkAt(Vector2 pos)
    {
        return this.HasChunkAt((int)pos.X, (int)pos.Y);
    }

    public bool HasChunkAt(int x, int y)
    {
        var chunkX = Math.Floor(x / (float)(Constants.ChunkSize * Constants.TileSize));
        var chunkY = Math.Floor(y / (float)(Constants.ChunkSize * Constants.TileSize));

        return this.Chunks.ContainsKey(chunkX + "," + chunkY);
    }

    public bool ChunkExists(int x, int y)
    {
        return this.Chunks.ContainsKey(x + "," + y);
    }

    public static Vector2 ToChunkPos(Vector2 pos)
    {
        var chunkX = Math.Floor(pos.X / (Constants.ChunkSize * Constants.TileSize));
        var chunkY = Math.Floor(pos.Y / (Constants.ChunkSize * Constants.TileSize));
        return new Vector2((float)chunkX, (float)chunkY);
    }

    public STile GetTileAt(Vector2 pos)
    {
        return this.GetTileAt((int)pos.X, (int)pos.Y);
    }

    public STile GetTileAt(int x, int y)
    {
        return this.GetChunkAt(x, y).GetTileAt(x, y);
    }

    public void SetTileAt(int x, int y, STile tile)
    {
        this.GetChunkAt(x, y).SetTileAt(x, y, tile);
    }

    public bool HasTileAt(Vector2 pos)
    {
        return this.HasTileAt((int)pos.X, (int)pos.Y);
    }

    public bool HasTileAt(int x, int y)
    {
        return this.HasChunkAt(x, y) && this.GetChunkAt(x, y).HasTileAt(new Vector2(x, y));
    }

    public void ProcessCrackQueue()
    {
        var now = DateTime.Now;
        var needsReorder = false;
        while (this.Cracks.Count > 0 && this.Cracks.Peek().ResetTime < now)
        {
            var head = this.Cracks.Dequeue();
            var stile = this.GetTileAt((int)head.Pos.X, (int)head.Pos.Y);
            this.SetTileAt((int)head.Pos.X, (int)head.Pos.Y, stile with { Hits = stile.Hits - 1 });
            if (stile.Hits >= 1)
            {
                this.Cracks.Enqueue(new CrackQueueEntry { Pos = head.Pos, ResetTime = now.AddSeconds(0.5) });
                needsReorder = true;
            }
        }
        if (needsReorder)
        {
            this.Cracks = new(this.Cracks.OrderBy(x => x.ResetTime));
        }
    }
}
