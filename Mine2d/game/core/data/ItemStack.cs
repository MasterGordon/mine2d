using Mine2d.game.core.items;

namespace Mine2d.game.core.data;

public class ItemStack
{
    public ItemId Id { get; set; }
    public int Count { get; set; }

    public ItemStack()
    {
    }

    public ItemStack(ItemId id, int count)
    {
        this.Id = id;
        this.Count = count;
    }

    public IntPtr GetTexture()
    {
        return Context.Get().ItemRegistry.GetItem(this.Id).GetTexture();
    }

    public string GetName()
    {
        return Context.Get().ItemRegistry.GetItem(this.Id).Name;
    }

    public bool IsStackable()
    {
        return Context.Get().ItemRegistry.GetItem(this.Id).GetKind() == ItemKind.Default;
    }

    public ItemKind GetKind()
    {
        return Context.Get().ItemRegistry.GetItem(this.Id).GetKind();
    }
}
