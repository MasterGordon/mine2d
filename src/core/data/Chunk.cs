class Chunk
{
    public STile[,] Tiles { get; set; } = new STile[Constants.ChunkSize, Constants.ChunkSize];
    public int X { get; set; }
    public int Y { get; set; }

    public Chunk(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public void SetTile(int x, int y, STile tile)
    {
        this.Tiles[x, y] = tile;
    }

    public STile GetTile(int x, int y)
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

    public STile GetTileAt(Vector2 pos)
    {
        return this.GetTileAt((int)pos.X, (int)pos.Y);
    }

    public STile GetTileAt(int x, int y)
    {
        var posInChunk = this.GetPositionInChunk(new Vector2(x, y));
        var tileX = (int)Math.Floor(posInChunk.X / Constants.TileSize);
        var tileY = (int)Math.Floor(posInChunk.Y / Constants.TileSize);
        return this.GetTile(tileX, tileY);
    }

    public void SetTileAt(Vector2 pos, STile tile)
    {
        this.SetTileAt((int)pos.X, (int)pos.Y, tile);
    }

    public void SetTileAt(int x, int y, STile tile)
    {
        var posInChunk = this.GetPositionInChunk(new Vector2(x, y));
        var tileX = (int)Math.Floor(posInChunk.X / Constants.TileSize);
        var tileY = (int)Math.Floor(posInChunk.Y / Constants.TileSize);
        this.SetTile(tileX, tileY, tile);
    }

    public bool hasTile(int x, int y)
    {
        return x >= 0 && x < this.Tiles.Length && y >= 0 && y < this.Tiles.Length && this.Tiles[x, y].Id != 0;
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
