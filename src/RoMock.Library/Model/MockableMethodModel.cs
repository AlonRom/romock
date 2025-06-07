using System.Reflection;

namespace RoMock.Library.Model;

public class MockableMethodModel
{
    public string? MethodName { get; set; }
    public Action<MockableMethodModel>? ToggleMethodAction { get; set; }
    public MethodInfo? Method { get; set; }

    private bool _isToggled;
    public bool IsToggled
    {
        get => _isToggled;
        set
        {
            if (!value.Equals(_isToggled))
            {
                _isToggled = value;
                ToggleMethodAction?.Invoke(this);
            }
        }
    }

}