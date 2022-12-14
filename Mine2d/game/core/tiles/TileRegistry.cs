using Mine2d.game.core.data;

namespace Mine2d.game.core.tiles;

public enum Tiles
{
    Stone = 1,
    Ore1 = 2,
    Ore2 = 3,
    Ore3 = 4,
    Ore4 = 5,
    Workbench = 6,
    IronOre = 7,
    CopperOre = 8,
    TinOre = 9,
    SilverOre = 10,
    GoldOre = 11,
    LeadOre = 12,
    PlatinumOre = 13,
    CobaltOre = 14,
    TungstenOre = 15,
    UraniumOre = 16,
    DiamondOre = 17,
    DripstoneUp = 18,
    DripstoneDown = 19,
    CoalOre = 20,
}

public class TileRegistry
{
    public Dictionary<int, Tile> Tiles { get; set; } = new();

    public void RegisterTile()
    {
        this.Tiles.Add(1, new Tile("stone", "stone", 5, ItemId.Stone));
        this.Tiles.Add(6, new Workbench("workbench", "workbench", 10));
        this.Tiles.Add(7, new OreTile("iron-ore", new[] { "stone", "iron-ore" }, 6, ItemId.RawIron));
        this.Tiles.Add(8, new OreTile("copper-ore", new[] { "stone", "copper-ore" }, 6, ItemId.RawCopper));
        this.Tiles.Add(9, new OreTile("tin-ore", new[] { "stone", "tin-ore" }, 7, ItemId.RawTin));
        this.Tiles.Add(10, new OreTile("silver-ore", new[] { "stone", "silver-ore" }, 7, ItemId.RawSilver));
        this.Tiles.Add(12, new OreTile("lead-ore", new[] { "stone", "lead-ore" }, 8, ItemId.RawLead));
        this.Tiles.Add(11, new OreTile("gold-ore", new[] { "stone", "gold-ore" }, 9, ItemId.RawGold));
        this.Tiles.Add(13, new OreTile("platinum-ore", new[] { "stone", "platinum-ore" }, 10, ItemId.RawPlatinum));
        this.Tiles.Add(14, new OreTile("cobalt-ore", new[] { "stone", "cobalt-ore" }, 11, ItemId.RawCobalt));
        this.Tiles.Add(15, new OreTile("tungsten-ore", new[] { "stone", "tungsten-ore" }, 15, ItemId.RawTungsten));
        this.Tiles.Add(16, new OreTile("uranium-ore", new[] { "stone", "uranium-ore" }, 15, ItemId.RawUranium));
        this.Tiles.Add(17, new OreTile("diamond-ore", new[] { "stone", "diamond-ore" }, 10, ItemId.Diamond));
        this.Tiles.Add(18, new DecoTile("dripstone-up", "dripstone-up", 5, ItemId.Air));
        this.Tiles.Add(19, new DecoTile("dripstone-down", "dripstone-down", 5, ItemId.Air));
        this.Tiles.Add(20, new OreTile("coal-ore", new[] { "stone", "coal-ore" }, 4, ItemId.Coal));
    }

    public Tile GetTile(int id)
    {
        return this.Tiles[id];
    }

    public int GetTileIdByItemId(ItemId itemId)
    {
        return this.Tiles.FirstOrDefault(x => x.Value.Drop == itemId).Key;
    }
}
