using RoMock.Example.App.Models;
using RoMock.Example.App.Repositories.ToDoRepository;

namespace RoMock.Example.App.Services.ToDoService;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _toDoRepository;

    public ToDoService(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public async Task<IEnumerable<ToDoModel>?> GetToDosAsync()
    {
        return await _toDoRepository.GetToDosAsync();
    }
}