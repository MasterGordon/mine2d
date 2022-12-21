using Mine2d.game.core;
using Mine2d.game.core.data;

namespace Mine2d.game.state;

public class PlayerInventory
{
    public ItemStack[] Hotbar { get; set; } = new ItemStack[9];
    public ItemStack[] Inventory { get; set; } = new ItemStack[5 * 9];
    public ItemStack Cursor { get; set; }

    public bool PickupItemStack(ItemStack itemStack)
    {
        return PickupItemStack(itemStack, this.Hotbar) || PickupItemStack(itemStack, this.Inventory);
    }

    public static bool PickupItemStack(ItemStack itemStack, ItemStack[] inventory)
    {
        var slot = InventoryUtils.GetFirstMatchingSlot(inventory, itemStack.Id);
        if (slot == -1)
        {
            return false;
        }
        if (inventory[slot] == null)
        {
            inventory[slot] = itemStack;
        }
        else
        {
            inventory[slot].Count += itemStack.Count;
        }
        return true;
    }

    public void SwapWithCursor(int slot, ItemStack[] inventory)
    {
        (inventory[slot], this.Cursor) = (this.Cursor, inventory[slot]);
    }
}
