using RoMock.Example.App.ViewModels.Base;

namespace RoMock.Example.App.Views.Base;

public class ContentPageBase : ContentPage
{
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            if (BindingContext is not IViewModelBase viewModelBase)
            {
                return;
            }

            await viewModelBase.InitializeAsyncCommand.ExecuteAsync(null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}