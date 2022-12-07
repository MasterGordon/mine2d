using Mine2d.game.core.data;

namespace Mine2d.game.core;

public static class InventoryUtils
{
    public static int GetFirstMatchingSlot(ItemStack[] inventory, ItemId id)
    {
        for (var i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && inventory[i].Id == id)
            {
                return i;
            }
        }
        for (var i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}
