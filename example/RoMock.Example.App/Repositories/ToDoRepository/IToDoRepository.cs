using RoMock.Example.App.Models;
using RoMock.Library.Interfaces;

namespace RoMock.Example.App.Repositories.ToDoRepository;

public interface IToDoRepository : IMockable
{
    public Task<IEnumerable<ToDoModel>?> GetToDosAsync();
}