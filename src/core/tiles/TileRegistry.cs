enum Tiles : int
{
    stone = 1,
}

class TileRegistry
{
    public Dictionary<int, Tile> Tiles { get; set; } = new Dictionary<int, Tile>();

    public void RegisterTile()
    {
        this.Tiles.Add(1, new Tile("stone", "stone"));
    }

    public Tile GetTile(int id)
    {
        return this.Tiles[id];
    }
}
