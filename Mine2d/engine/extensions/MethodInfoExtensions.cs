using System.Reflection;

namespace Mine2d.engine.extensions;

public static class MethodInfoExtensions
{
    public static bool HasAttribute<T>(this MethodInfo methodInfo) where T : Attribute
        => methodInfo.GetCustomAttributes(typeof(T), false).Length > 0;
}