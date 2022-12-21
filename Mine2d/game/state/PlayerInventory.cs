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
        if (this.Cursor != null && inventory[slot] != null && this.Cursor.Id == inventory[slot].Id)
        {
            inventory[slot].Count += this.Cursor.Count;
            this.Cursor = null;
        }
        else
        {
            (inventory[slot], this.Cursor) = (this.Cursor, inventory[slot]);
        }
    }

    public void TakeHalf(int slot, ItemStack[] inventory)
    {
        if (inventory[slot] == null)
            return;
        if(inventory[slot].Count == 1)
        {
            this.Cursor = inventory[slot];
            inventory[slot] = null;
            return;
        }
        this.Cursor = new ItemStack(inventory[slot].Id, inventory[slot].Count / 2);
        inventory[slot].Count -= this.Cursor.Count;
    }

    public void DropOne(int slot, ItemStack[] inventory)
    {
        if (inventory[slot] == null)
        {
            inventory[slot] = new ItemStack(this.Cursor.Id, 1);
        }
        else if (inventory[slot].Id == this.Cursor.Id)
        {
            inventory[slot].Count++;
        }
        else
        {
            return;
        }
        this.Cursor.Count--;
        if (this.Cursor.Count == 0)
        {
            this.Cursor = null;
        }
    }
}
