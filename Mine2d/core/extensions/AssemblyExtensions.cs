using System.Reflection;

namespace Mine2d.core.extensions;

public static class AssemblyExtensions
{
    public static Type[] GetTypesSafe(this Assembly assembly)
    {
        try
        {
#pragma warning disable IL2026
            return assembly.GetTypes();
#pragma warning restore IL2026
        }
        catch (ReflectionTypeLoadException ex)
        {
            return ex.Types.Where(t => t != null).ToArray();
        }
    }
}