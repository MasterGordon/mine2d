namespace Mine2d.core.tiles;

public enum Tiles
{
    Stone = 1,
}

public class TileRegistry
{
    public Dictionary<int, Tile> Tiles { get; set; } = new();

    public void RegisterTile()
    {
        this.Tiles.Add(1, new Tile("stone", "stone", 5));
    }

    public Tile GetTile(int id)
    {
        return this.Tiles[id];
    }
}
