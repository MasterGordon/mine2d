using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.game.state;

namespace Mine2d.game.frontend.inventory;

public class InventoryRegistry
{
    private readonly Dictionary<Inventory, IInventoryRenderer> inventoryRenderers = new();

    public InventoryRegistry()
    {
        this.inventoryRenderers.Add(Inventory.Player, new PlayerInventoryRenderer());
    }

    public IInventoryRenderer GetRenderer(Inventory inventory)
    {
        return this.inventoryRenderers[inventory];
    }
}
