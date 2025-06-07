using CommunityToolkit.Mvvm.Input;

namespace RoMock.Example.App.ViewModels.Base;

public interface IViewModelBase
{
    public IAsyncRelayCommand InitializeAsyncCommand { get; }
}