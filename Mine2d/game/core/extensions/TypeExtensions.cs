using System.Reflection;

namespace Mine2d.game.core.extensions;

public static class TypeExtensions
{
    public static Type MakeGenericTypeSafely(this Type type, params Type[] typeArguments)
    {
        try
        {
#pragma warning disable IL2026
            return type.MakeGenericType(typeArguments);
#pragma warning restore IL2026
        }
        catch (ReflectionTypeLoadException e)
        {
            var missingTypes = e.Types
                .Where(t => typeArguments.Contains(t) && t != null);

            throw new ArgumentException($"Failed to make generic type {type} with arguments {string.Join(", ", missingTypes)}", e);
        }
    }
}
