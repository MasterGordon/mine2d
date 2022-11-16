namespace Mine2d.engine.utils;

public static class Neighbor
{
    public static IEnumerable<Vector2> Get4Neighbors(this Vector2 pos)
    {
        return new[]
        {
        new Vector2(pos.X - 1, pos.Y),
        new Vector2(pos.X + 1, pos.Y),
        new Vector2(pos.X, pos.Y - 1),
        new Vector2(pos.X, pos.Y + 1)
    };
    }

    public static IEnumerable<Vector2> Get8Neighbors(this Vector2 pos)
    {
        return new[]
        {
        new Vector2(pos.X - 1, pos.Y - 1),
        new Vector2(pos.X + 1, pos.Y - 1),
        new Vector2(pos.X - 1, pos.Y + 1),
        new Vector2(pos.X + 1, pos.Y + 1)
    };
    }

    public static IEnumerable<Vector2> Get20Neighbors(this Vector2 pos)
    {
        return new[]
        {
        new Vector2(pos.X - 2, pos.Y),
        new Vector2(pos.X + 2, pos.Y),
        new Vector2(pos.X, pos.Y - 2),
        new Vector2(pos.X, pos.Y + 2),
        new Vector2(pos.X - 2, pos.Y +1),
        new Vector2(pos.X + 2, pos.Y +1),
        new Vector2(pos.X - 2, pos.Y -1),
        new Vector2(pos.X + 2, pos.Y -1),
        new Vector2(pos.X - 1, pos.Y +2),
        new Vector2(pos.X + 1, pos.Y +2),
        new Vector2(pos.X - 1, pos.Y -2),
        new Vector2(pos.X + 1, pos.Y -2),
    };
    }
}
