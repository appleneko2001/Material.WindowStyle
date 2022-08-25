using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Material.WindowStyle.Demo.Views;

namespace Material.WindowStyle.Demo
{
    public class DemoApp : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new DemoWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}