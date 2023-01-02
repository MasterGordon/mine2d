namespace Mine2d.game.state;

public class Player
{
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Guid Id { get; set; }
    public Vector2 Mining { get; set; }
    public int MiningCooldown { get; set; }
    public PlayerInventory Inventory { get; set; } = new();
    public PlayerMovementState PlayerMovementState { get; set; } = new();

    public Vector2 GetCenter()
    {
        return this.Position + new Vector2(7, -14);
    }

    public int GetMiningSpeed() {
        return 10;
    }

    public int GetHarvestLevel() {
        return 1;
    }
}
