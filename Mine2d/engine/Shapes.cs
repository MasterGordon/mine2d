namespace mine2d.engine;

public struct Line
{
    public Vector2 Start { get; set; }
    public Vector2 End { get; set; }

    public Line(Vector2 start, Vector2 end)
    {
        this.Start = start;
        this.End = end;
    }
}
