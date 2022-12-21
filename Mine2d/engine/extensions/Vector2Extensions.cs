namespace Mine2d.engine.extensions;

public static class Vector2Extensions
{
    public static Vector2 Clamp(this Vector2 vector, Vector2 min, Vector2 max)
    {
        return new Vector2(Math.Clamp(vector.X, min.X, max.X), Math.Clamp(vector.Y, min.Y, max.Y));
    }
}