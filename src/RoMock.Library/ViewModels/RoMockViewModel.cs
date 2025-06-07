using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoMock.Library.Extensions;
using RoMock.Library.Interfaces;
using RoMock.Library.Model;
using RoMock.Library.Services;

namespace RoMock.Library.ViewModels;

public partial class RoMockViewModel : RoMockViewModelBase
{
    private readonly IRoMockService _roMockService;

    private readonly List<MockableMethodModel> _allMethods = new();

    [ObservableProperty] 
    private ObservableCollection<MockableMethodModel> _searchResultMethods = new();
    public static Dictionary<string, bool> MockMethods { get; } = new();

    private string? _searchMethodsString;
    public string? SearchMethodsString
    {
        get => _searchMethodsString;
        set
        {
            if (value != null && !value.Equals(_searchMethodsString))
            {
                _searchMethodsString = value;
                FilterMethodsList(_searchMethodsString);
            }
        }
    }

    private bool _useRoMock;
    public bool UseRoMock
    {
        get => _useRoMock;
        set
        {
            if (!value.Equals(_useRoMock))
            {

                _useRoMock = value;
                OnPropertyChanged();
                ToggleUseRoMock();
            }
        }
    }

    public RoMockViewModel(IRoMockService roMockService)
    {
        _roMockService = roMockService;
    }

    private async void ToggleUseRoMock()
    {
        try
        {
            IsLoading = true;

            await Task.Run(() =>
            {
                if (_useRoMock)
                {
                    // Get all currently loaded assemblies
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                    // Find all methods with the MockableMethodAttribute in classes implementing IMockable using the extension method
                    var methodsWithAttribute = assemblies.FindMethodsWithMockableAttribute(typeof(IMockable));

                    // Display the results
                    foreach (var method in methodsWithAttribute)
                    {
                        _allMethods.Add(new MockableMethodModel
                        {
                            MethodName = method.Name,
                            Method = method,
                            IsToggled = false,
                            ToggleMethodAction = OnToggleMethodAction
                        });
                    }

                    SearchResultMethods = new ObservableCollection<MockableMethodModel>(_allMethods);
                }
                else
                {
                    MockMethods.Clear();
                    SearchResultMethods.Clear();
                    _allMethods.Clear();
                }
            });

            IsLoading = false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            IsLoading = true;

            foreach (var methodModel in SearchResultMethods)
            {
                if (methodModel.IsToggled)
                {
                    // Register mock methods into RoMockService
                    _roMockService.RegisterMockMethod(methodModel.MethodName, methodModel.Method);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            // Navigate back to the previous page
            await Shell.Current.GoToAsync("..");
            IsLoading = false;
        }
    }

    private void OnToggleMethodAction(MockableMethodModel mockableMethodModel)
    {
        if (mockableMethodModel.MethodName != null)
        {
            MockMethods[mockableMethodModel.MethodName] = mockableMethodModel.IsToggled;
        }
    }


    private void FilterMethodsList(string searchMethodsString)
    {
        SearchResultMethods.Clear();
        SearchResultMethods = new ObservableCollection<MockableMethodModel>(searchMethodsString != string.Empty
            ? _allMethods.Where(s => s.MethodName != null && s.MethodName.ToLower().Contains(searchMethodsString.ToLower()))
            : _allMethods);
    }
}

