using Mine2d.engine;

namespace Mine2d.state;

public class Player
{
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Movement { get; set; }
    public Guid Id { get; set; }
    public Vector2 Mining { get; set; }
    public int MiningCooldown { get; set; }

    public Line GetBottomCollisionLine()
    {
        return new Line(this.Position, this.Position + new Vector2(16, 0));
    }
}
