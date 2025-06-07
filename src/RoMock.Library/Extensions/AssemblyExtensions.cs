using System.Reflection;
using RoMock.Library.Attributes;

namespace RoMock.Library.Extensions;

public static class AssemblyExtensions
{
    public static Type? FindImplementationType(this IEnumerable<Assembly> assemblies, Type interfaceType)
    {
        return assemblies.SelectMany(asm => asm.GetTypes())
            .FirstOrDefault(t => interfaceType.IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });
    }

    public static IEnumerable<Type> FindMockableInterfaces(this IEnumerable<Assembly> assemblies, Type targetType)
    {
        return assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsInterface && targetType.IsAssignableFrom(type) && type != targetType)
            .ToArray();
    }

    public static IEnumerable<MethodInfo> FindMethodsWithMockableAttribute(this IEnumerable<Assembly> assemblies, Type targetType)
    {
        var methodsWithAttribute = new List<MethodInfo>();

        Parallel.ForEach(assemblies, assembly =>
        {
            try
            {
                var methods = assembly.GetTypes()
                    .Where(t => t.GetInterfaces().Contains(targetType)) // Filter by the target type (interface)
                    .SelectMany(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    .Where(m => m.GetCustomAttributes(typeof(MockableMethodAttribute), false).Any())
                    .ToList();

                lock (methodsWithAttribute)
                {
                    methodsWithAttribute.AddRange(methods);
                }
            }
            catch (ReflectionTypeLoadException e)
            {
                // Handle exception if the assembly cannot be loaded or types cannot be reflected
                Console.WriteLine(e.Message);
            }
        });

        return methodsWithAttribute;
    }
}