namespace Mine2d.game.core.data.entities;

public class ItemEntity : Entity
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public ItemId ItemId { get; set; }
}
