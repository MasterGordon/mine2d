using Mine2d.game.core.data;

namespace Mine2d.game.core.items;

public enum ItemKind
{
    Default,
    Pickaxe,
    Helmet,
    Chestplate,
    Leggings,
}

public class ItemRegistry
{
    private readonly Dictionary<ItemId, Item> items = new();

    public void RegisterItems()
    {
        this.Register(ItemId.Stone, new BlockItem(ItemId.Stone, "Stone", new[] { "stone" }));
        this.Register(ItemId.Workbench, new BlockItem(ItemId.Workbench, "Workbench", new[] { "workbench" }));
        this.Register(ItemId.RawIron, new Item(ItemId.RawCobalt, "Raw Iron", "items.raw-iron" ));
        this.Register(ItemId.RawCopper, new Item(ItemId.RawCopper, "Raw Copper", "items.raw-copper" ));
        this.Register(ItemId.RawTin, new Item(ItemId.RawTin, "Raw Tin", "items.raw-tin" ));
        this.Register(ItemId.RawSilver, new Item(ItemId.RawSilver, "Raw Silver", "items.raw-silver" ));
        this.Register(ItemId.RawGold, new Item(ItemId.RawGold, "Raw Gold", "items.raw-gold" ));
        this.Register(ItemId.RawLead, new Item(ItemId.RawLead, "Raw Lead", "items.raw-lead" ));
        this.Register(ItemId.RawPlatinum, new Item(ItemId.RawPlatinum, "Raw Platinum", "items.raw-platinum" ));
        this.Register(ItemId.RawCobalt, new Item(ItemId.RawCobalt, "Raw Cobalt", "items.raw-cobalt" ));
        this.Register(ItemId.RawTungsten, new Item(ItemId.RawTungsten, "Raw Tungsten", "items.raw-tungsten" ));
        this.Register(ItemId.RawUranium, new Item(ItemId.RawUranium, "Raw Uranium", "items.raw-uranium" ));
        this.Register(ItemId.Diamond, new Item(ItemId.Diamond, "Diamond", "items.diamond" ));
        this.Register(ItemId.Coal, new Item(ItemId.Coal, "Coal", "items.coal" ));
        // Ingots
        this.Register(ItemId.IronIngot, new Item(ItemId.IronIngot, "Iron Ingot", "items.ingot-iron" ));
        this.Register(ItemId.GoldIngot, new Item(ItemId.GoldIngot, "Gold Ingot", "items.ingot-gold" ));
        this.Register(ItemId.CopperIngot, new Item(ItemId.CopperIngot, "Copper Ingot", "items.ingot-copper" ));
        this.Register(ItemId.SilverIngot, new Item(ItemId.SilverIngot, "Silver Ingot", "items.ingot-silver" ));
        // Pickaxes
        this.Register(ItemId.PickaxeBasic, new PickaxeItem(ItemId.PickaxeBasic, "Basic Pickaxe", "items.pickaxe-basic", 15, 4));
        this.Register(ItemId.PickaxeStone, new PickaxeItem(ItemId.PickaxeStone, "Stone Pickaxe", "items.pickaxe-stone", 25, 6));
        this.Register(ItemId.PickaxeIron, new PickaxeItem(ItemId.PickaxeIron, "Iron Pickaxe", "items.pickaxe-iron", 30, 7));
        this.Register(ItemId.PickaxeGold, new PickaxeItem(ItemId.PickaxeGold, "Gold Pickaxe", "items.pickaxe-gold", 35, 9));
        // Materials
        this.Register(ItemId.CopperWire, new Item(ItemId.CopperWire, "Copper Wire", "items.copper-wire" ));
        this.Register(ItemId.Silicon, new Item(ItemId.Silicon, "Silicon", "items.silicon" ));
        this.Register(ItemId.CircuitBoard, new Item(ItemId.CircuitBoard, "Circuit Board", "items.circuit-board" ));
        this.Register(ItemId.ElectricCircuit, new Item(ItemId.ElectricCircuit, "Electric Circuit", "items.electric-circuit" ));
        this.Register(ItemId.Fuse, new Item(ItemId.Fuse, "Fuse", "items.fuse" ));
        // Utilities
        this.Register(ItemId.Gps, new Item(ItemId.Gps, "GPS", "items.gps" ));
    }

    public void Register(ItemId id, Item item)
    {
        this.items.Add(id, item);
    }

    public Item GetItem(ItemId id)
    {
        return this.items[id];
    }
}
