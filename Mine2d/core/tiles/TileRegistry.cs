namespace Mine2d.core.tiles;

public enum Tiles
{
    Stone = 1,
    Ore1 = 2,
    Ore2 = 3,
    Ore3 = 4,
    Ore4 = 5,
}

public class TileRegistry
{
    public Dictionary<int, Tile> Tiles { get; set; } = new();

    public void RegisterTile()
    {
        this.Tiles.Add(1, new Tile("stone", "stone", 5));
        this.Tiles.Add(2, new OreTile("ore1", new string[] { "stone", "ore1" }, 5));
        this.Tiles.Add(3, new OreTile("ore2", new string[] { "stone", "ore2" }, 7));
        this.Tiles.Add(4, new OreTile("ore3", new string[] { "stone", "ore3" }, 8));
        this.Tiles.Add(5, new OreTile("ore4", new string[] { "stone", "ore4" }, 10));
    }

    public Tile GetTile(int id)
    {
        return this.Tiles[id];
    }
}
