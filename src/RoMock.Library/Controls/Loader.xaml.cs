namespace RoMock.Library.Controls;

public partial class Loader : ContentView
{
	public Loader()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(Loader),
            false,
            BindingMode.TwoWay);

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }
}