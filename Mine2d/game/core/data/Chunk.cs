namespace Mine2d.game.core.data;

public class Chunk
{
    public STile[,] Tiles { get; set; } = new STile[Constants.ChunkSize, Constants.ChunkSize];
    public int X { get; set; }
    public int Y { get; set; }
    public List<Entity> Entities { get; set; } = new();
    public Dictionary<(int, int), Entity> TileEntities { get; set; } = new();

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

    public bool HasTileAt(Vector2 pos)
    {
        return this.HasTileAt((int)pos.X, (int)pos.Y);
    }

    public bool HasTileAt(int x, int y)
    {
        var posInChunk = this.GetPositionInChunk(new Vector2(x, y));
        var tileX = (int)Math.Floor(posInChunk.X / Constants.TileSize);
        var tileY = (int)Math.Floor(posInChunk.Y / Constants.TileSize);
        return this.HasTile(tileX, tileY);
    }

    public bool HasSolidTileAt(Vector2 pos)
    {
        return this.HasSolidTileAt((int)pos.X, (int)pos.Y);
    }

    public bool HasSolidTileAt(int x, int y)
    {
        var posInChunk = this.GetPositionInChunk(new Vector2(x, y));
        var tileX = (int)Math.Floor(posInChunk.X / Constants.TileSize);
        var tileY = (int)Math.Floor(posInChunk.Y / Constants.TileSize);
        if (!this.HasTile(tileX, tileY)) return false;
        return Context.Get().TileRegistry.GetTile(this.GetTile(tileX, tileY).Id).IsSolid();
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

    public bool HasTile(int x, int y)
    {
        return x >= 0 && x < Constants.ChunkSize && y >= 0 && y < Constants.ChunkSize && this.Tiles[x, y].Id != 0;
    }

    public bool HasTile(Vector2 pos)
    {
        return this.HasTile((int)pos.X, (int)pos.Y);
    }

    public Vector2 GetPositionInChunk(Vector2 pos)
    {
        return pos - new Vector2(this.X * Constants.ChunkSize * Constants.TileSize,
            this.Y * Constants.ChunkSize * Constants.TileSize);
    }

    public void AddEntity(Entity entity)
    {
        this.Entities.Add(entity);
    }
}
