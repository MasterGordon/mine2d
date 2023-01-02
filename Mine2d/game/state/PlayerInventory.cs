using Mine2d.game.core;
using Mine2d.game.core.data;
using Mine2d.game.core.items;

namespace Mine2d.game.state;

public class PlayerInventory
{
    public ItemStack[] Hotbar { get; set; } = new ItemStack[9];
    public ItemStack[] Inventory { get; set; } = new ItemStack[5 * 9];
    public ItemStack Pickaxe { get; set; }
    public ItemStack Helmet { get; set; }
    public ItemStack Chestplate { get; set; }
    public ItemStack Leggings { get; set; }
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

    public bool HasItemStack(ItemStack itemStack)
    {
        return HasItemStack(itemStack, this.Hotbar) || HasItemStack(itemStack, this.Inventory);
    }

    public static bool HasItemStack(ItemStack itemStack, ItemStack[] inventory)
    {
        var slot = InventoryUtils.GetFirstMatchingSlot(inventory, itemStack.Id);
        if (slot == -1)
        {
            return false;
        }
        if(inventory[slot] == null) return false;
        return inventory[slot].Count >= itemStack.Count;
    }

    public bool RemoveItemStack(ItemStack itemStack)
    {
        return RemoveItemStack(itemStack, this.Hotbar) || RemoveItemStack(itemStack, this.Inventory);
    }

    public static bool RemoveItemStack(ItemStack itemStack, ItemStack[] inventory)
    {
        var slot = InventoryUtils.GetFirstMatchingSlot(inventory, itemStack.Id);
        if (slot == -1)
        {
            return false;
        }
        if (inventory[slot].Count == itemStack.Count)
        {
            inventory[slot] = null;
        }
        else
        {
            inventory[slot].Count -= itemStack.Count;
        }
        return true;
    }

    public void SwapWithCursor(int slot, ItemStack[] inventory)
    {
        if (this.Cursor != null && inventory[slot] != null && this.Cursor.Id == inventory[slot].Id && this.Cursor.IsStackable())
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
        if (inventory[slot].Count == 1)
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
        else if (inventory[slot].Id == this.Cursor.Id && this.Cursor.IsStackable())
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

    public void SwapPickaxeWithCursor()
    {
        if (this.Cursor != null && this.Cursor?.GetKind() != ItemKind.Pickaxe) return;
        (this.Pickaxe, this.Cursor) = (this.Cursor, this.Pickaxe);
    }

    public void SwapHelmetWithCursor()
    {
        if (this.Cursor != null && this.Cursor?.GetKind() != ItemKind.Helmet) return;
        (this.Helmet, this.Cursor) = (this.Cursor, this.Helmet);
    }

    public void SwapChestplateWithCursor()
    {
        if (this.Cursor != null && this.Cursor?.GetKind() != ItemKind.Chestplate) return;
        (this.Chestplate, this.Cursor) = (this.Cursor, this.Chestplate);
    }

    public void SwapLeggingsWithCursor()
    {
        if (this.Cursor != null && this.Cursor?.GetKind() != ItemKind.Leggings) return;
        (this.Leggings, this.Cursor) = (this.Cursor, this.Leggings);
    }
}
