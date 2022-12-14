namespace Mine2d.engine.extensions;

public static class TypeExtensions
{
    public static bool HasAttribute<T>(this Type type) where T : Attribute
        => type.GetCustomAttributes(typeof(T), false).Length > 0;
}