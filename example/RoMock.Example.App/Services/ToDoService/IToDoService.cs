using RoMock.Example.App.Models;

namespace RoMock.Example.App.Services.ToDoService;

public interface IToDoService
{
    public Task<IEnumerable<ToDoModel>?> GetToDosAsync();
}