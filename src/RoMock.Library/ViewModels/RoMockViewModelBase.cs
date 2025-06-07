using CommunityToolkit.Mvvm.ComponentModel;

namespace RoMock.Library.ViewModels;

public partial class RoMockViewModelBase : ObservableObject
{
    [ObservableProperty]
    private bool? _isLoading;
}