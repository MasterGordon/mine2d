class Chunk
{
    public int[,] Tiles { get; set; } = new int[Constants.ChunkSize, Constants.ChunkSize];
    public int X { get; set; }
    public int Y { get; set; }

    public Chunk(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public void SetTile(int x, int y, int tile)
    {
        this.Tiles[x, y] = tile;
    }

    public int GetTile(int x, int y)
    {
        return this.Tiles[x, y];
    }

    public bool hasTileAt(Vector2 pos)
    {
        return this.hasTileAt((int)pos.X, (int)pos.Y);
    }

    public bool hasTileAt(int x, int y)
    {
        var posInChunk = this.GetPositionInChunk(new Vector2(x, y));
        var tileX = (int)Math.Floor(posInChunk.X / Constants.TileSize);
        var tileY = (int)Math.Floor(posInChunk.Y / Constants.TileSize);
        return this.hasTile(tileX, tileY);
    }

    public int GetTileAt(Vector2 pos)
    {
        return this.GetTileAt((int)pos.X, (int)pos.Y);
    }

    public int GetTileAt(int x, int y)
    {
        var tileX = (int)Math.Floor(x / (float)Constants.TileSize);
        var tileY = (int)Math.Floor(y / (float)Constants.TileSize);
        return this.GetTile(tileX, tileY);
    }

    public bool hasTile(int x, int y)
    {
        return x >= 0 && x < this.Tiles.Length && y >= 0 && y < this.Tiles.Length;
    }

    public bool hasTile(Vector2 pos)
    {
        return this.hasTile((int)pos.X, (int)pos.Y);
    }

    public Vector2 GetPositionInChunk(Vector2 pos)
    {
        return pos - new Vector2(this.X * Constants.ChunkSize * Constants.TileSize,
            this.Y * Constants.ChunkSize * Constants.TileSize);
    }
}
