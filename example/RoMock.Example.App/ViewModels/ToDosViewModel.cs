using System.Collections.ObjectModel;
using RoMock.Example.App.Models;
using RoMock.Example.App.Services.ToDoService;
using RoMock.Example.App.ViewModels.Base;

namespace RoMock.Example.App.ViewModels;

public class ToDosViewModel : ViewModelBase
{
    private readonly IToDoService _toDoService;
    public ObservableCollection<ToDoModel>? ToDos { get; set; } = [];

    public ToDosViewModel(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }
    public override async Task LoadAsync()
    {
        await Loading(
            async () =>
            {
                await GetToDos();
            });
    }

    private async Task GetToDos()
    {
        var toDos = await _toDoService.GetToDosAsync();
        if (toDos != null)
        {
            ToDos?.Clear();
            foreach (var toDo in toDos)
            {
                ToDos?.Add(toDo);
            }
        }
    }
}