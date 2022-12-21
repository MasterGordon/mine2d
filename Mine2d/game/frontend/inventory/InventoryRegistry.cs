using Mine2d.game.state;

namespace Mine2d.game.frontend.inventory;

public class InventoryRegistry
{
    private readonly Dictionary<InventoryKind, Inventory> inventoryRenderers = new();

    public InventoryRegistry()
    {
        this.inventoryRenderers.Add(InventoryKind.Player, new PlayerInventoryRenderer());
    }

    public Inventory GetInventory(InventoryKind inventory)
    {
        if(!this.inventoryRenderers.ContainsKey(inventory))
            return null;
        return this.inventoryRenderers[inventory];
    }
}
