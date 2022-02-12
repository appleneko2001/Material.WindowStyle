using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Material.WindowStyle.Assists
{
    public static class MaterialTitleBarButtonAssist
    {
        public static readonly AvaloniaProperty<IBrush> OnHoverProperty = AvaloniaProperty.RegisterAttached<Button, IBrush>(
            "OnHover", typeof(MaterialTitleBarButtonAssist));

        public static IBrush GetOnHover(AvaloniaObject element) {
            return (IBrush) element.GetValue(OnHoverProperty);
        }

        public static void SetOnHover(AvaloniaObject element, IBrush value) {
            element.SetValue(OnHoverProperty, value);
        }
    }
}