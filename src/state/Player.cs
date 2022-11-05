class Player
{
    public string Name;
    public Vector2 Position;
    public Vector2 Movement;
    public Guid Guid;

    public Line GetBottomCollisionLine()
    {
        return new Line(Position, Position + new Vector2(16, 0));
    }
}
