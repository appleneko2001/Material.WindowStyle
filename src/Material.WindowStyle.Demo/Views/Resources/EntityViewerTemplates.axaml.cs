using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Material.WindowStyle.Demo.Views.ViewModels.Entities;

namespace Material.WindowStyle.Demo.Views.Resources
{
    public class EntityViewerTemplates : ResourceDictionary
    {
        private void OnEntityElementTemplateSelectTemplateKey(object? sender, SelectTemplateEventArgs e)
        {
            e.TemplateKey = e.DataContext switch
            {
                LinkEntityViewModel => "LinkText",
                HeaderTextEntityViewModel => "Header",
                MonospaceTextEntityViewModel => "MonospaceText",
                TextEntityViewModel => "Text",
                BulletItemEntityViewModel => "BulletItem",
                CombinedEntityViewModel => "Combination",
                PictureEntityViewModel => "Picture",
                EntityViewModel => "Base",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void MonospacedTextElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (sender is not IControl control)
                return;

            if (control.DataContext is not MonospaceTextEntityViewModel vm)
                return;

            vm.IsPointerPressed = true;
        }

        private void MonospacedTextElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (sender is not IControl control)
                return;

            if (control.DataContext is not MonospaceTextEntityViewModel vm)
                return;

            if (!vm.IsPointerPressed)
                return;
            
            vm.IsPointerPressed = false;
            Application.Current?
                .Clipboard?
                .SetTextAsync(vm.Text ?? string.Empty)
                .ContinueWith(delegate(Task task)
                {
                    if (!task.IsCompleted)
                        return;

                    vm.IsCopiedToolTipVisible = true;
                    control.PointerExited += OnPointerExitedMonospaceTextZoneAfterClick;
                });
        }

        private void OnPointerExitedMonospaceTextZoneAfterClick(object? sender, PointerEventArgs e)
        {
            if (sender is not IControl control)
                return;

            control.PointerExited -= OnPointerExitedMonospaceTextZoneAfterClick;
            
            if (control.DataContext is not MonospaceTextEntityViewModel vm)
                return;

            vm.IsCopiedToolTipVisible = false;
        }
    }
}