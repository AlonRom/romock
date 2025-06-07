using System.Reflection;
using System.Text.Json;
using RoMock.Example.App.Models;
using RoMock.Library.Attributes;

namespace RoMock.Example.App.Repositories.ToDoRepository;

public class ToDoRepository : IToDoRepository
{
    [MockableMethod]
    public async Task<IEnumerable<ToDoModel>?> GetToDosAsync()
    {
        var assembly = typeof(ToDoRepository).GetTypeInfo().Assembly;
        var resourceName = GetEmbeddedResourceName(assembly, "todos.json");
        if (resourceName == null)
        {
            return null;
        }
        await using Stream? stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new FileNotFoundException($"Resource not found: {resourceName}");
        }

        using StreamReader reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var toDos = JsonSerializer.Deserialize<IEnumerable<ToDoModel>>(json);
        return toDos;

    }

    private string? GetEmbeddedResourceName(Assembly assembly, string fileName)
    {
        // Get all resource names
        var resourceNames = assembly.GetManifestResourceNames();

        // Find the resource name ending with the specified file name
        foreach (var resourceName in resourceNames)
        {
            if (resourceName.EndsWith(fileName, StringComparison.OrdinalIgnoreCase))
            {
                return resourceName;
            }
        }

        return null;
    }
}