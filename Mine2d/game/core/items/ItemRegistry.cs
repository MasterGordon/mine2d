using Mine2d.game.core.data;

namespace Mine2d.game.core.items;

public class ItemRegistry
{
    private readonly Dictionary<ItemId, Item> items = new();

    public void RegisterItems()
    {
        this.Register(ItemId.Stone, new Item(ItemId.Stone, "Stone", new[] { "stone" }));
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
