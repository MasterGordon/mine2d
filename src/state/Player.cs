class Player
{
    public string Name;
    public Vector2 Position;
    public Vector2 Movement;
    public Guid Guid;
    public Vector2 Mining;
    public int MiningCooldown;

    public Line GetBottomCollisionLine()
    {
        return new Line(this.Position, this.Position + new Vector2(16, 0));
    }
}
