using Mine2d.game.core.items;

namespace Mine2d.game.state;

public class Player
{
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 PrettyPosition { get => (this.Position - new Vector2(512244, 5390)) / 16; }
    public Guid Id { get; set; }
    public Vector2 Mining { get; set; }
    public int MiningCooldown { get; set; }
    public PlayerInventory Inventory { get; set; } = new();
    public PlayerMovementState PlayerMovementState { get; set; } = new();

    public Vector2 GetCenter()
    {
        return this.Position + new Vector2(7, -14);
    }

    private PickaxeItem getPickaxe()
    {
        if (this.Inventory.Pickaxe == null)
        {
            return null;
        }

        var item = Context.Get().ItemRegistry.GetItem(this.Inventory.Pickaxe.Id);
        return item as PickaxeItem;
    }

    public int GetMiningSpeed()
    {
        return this.getPickaxe() == null ? 1 : this.getPickaxe().GetMiningSpeed();
    }

    public int GetHarvestLevel()
    {
        return this.getPickaxe() == null ? 1 : this.getPickaxe().GetHarvestLevel();
    }
}
