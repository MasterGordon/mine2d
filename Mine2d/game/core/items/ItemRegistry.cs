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
        this.Register(ItemId.PickaxeBasic, new PickaxeItem(ItemId.PickaxeBasic, "Basic Pickaxe", "items.pickaxe-basic"));
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
