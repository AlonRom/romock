using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RoMock.Example.App.ViewModels.Base;

public partial class ViewModelBase : ObservableObject, IViewModelBase
{
    [ObservableProperty]
    private bool _isLoading;

    public IAsyncRelayCommand InitializeAsyncCommand { get; }

    public ViewModelBase()
    {
        InitializeAsyncCommand = new AsyncRelayCommand(
            async () =>
            {
                IsLoading = true;
                await Loading(LoadAsync);
                IsLoading = false;
            });
    }

    public virtual Task LoadAsync()
    {
        return Task.CompletedTask;
    }

    protected async Task Loading(Func<Task> unitOfWork)
    {
        try
        {
            await unitOfWork();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Log error
        }
    }
}