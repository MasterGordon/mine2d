using Mine2d.game.core;
using Mine2d.game.core.data;

namespace Mine2d.game.state;

public class PlayerInventory
{
    public ItemStack[] Hotbar { get; set; } = new ItemStack[9];

    public bool PickupItemStack(ItemStack itemStack)
    {
        var slot = InventoryUtils.GetFirstMatchingSlot(this.Hotbar, itemStack.Id);
        if (slot == -1)
        {
            return false;
        }
        if (this.Hotbar[slot] == null)
        {
            this.Hotbar[slot] = itemStack;
        }
        else
        {
            this.Hotbar[slot].Count += itemStack.Count;
        }
        return true;
    }
}
