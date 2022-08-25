using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Platform;
using Material.WindowStyle.Demo.Views.ViewModels.Entities;
using MinimalMvvm.ViewModels;

namespace Material.WindowStyle.Demo.Views.ViewModels
{
    public class DemoWindowViewModel : ViewModelBase
    {
        public AvaloniaList<EntityViewModel> Requirements { get; } = new()
        {
            new BulletItemEntityViewModel
            {
                Entities =
                {
                    new LinkEntityViewModel("AvaloniaUI framework")
                    {
                        Href = "https://github.com/AvaloniaUI/Avalonia",
                        ToolTip = "Avalonia is a cross-platform XAML-based UI framework providing a flexible styling system and supporting a wide range of Operating Systems such as Windows via .NET Framework and .NET Core, Linux via Xorg, macOS."
                    },
                    new TextEntityViewModel(" version 11.0.0-preview1 or newer.")
                }
            }
        };
        public AvaloniaList<EntityViewModel> OptionalSupportedLibs { get; } = new()
        {
            new BulletItemEntityViewModel(new EntityViewModel []
            {
                new LinkEntityViewModel("Material.Avalonia toolkit")
                {
                    Href = "https://github.com/AvaloniaCommunity/Material.Avalonia",
                    ToolTip = "This toolkit is a collection of styles to help you customize your Avalonia application theme with Material Design."
                },
                new TextEntityViewModel(" version 2.4.0 or newer.")
            })
        };
        public AvaloniaList<EntityViewModel> Steps { get; } = new()
        {
            new BulletItemEntityViewModel(new []
            {
                new TextEntityViewModel("Update your Avalonia UI framework to latest. Make sure your framework version is newer or equal 11.0.0-preview1.")
            }),
            new BulletItemEntityViewModel(new []
            {
                new TextEntityViewModel("Install Material.WindowStyle package from NuGet. The major and minor package version should be 0.11. Build and revision version should be as newer as possible so you wont miss updates.")
            }),
            new BulletItemEntityViewModel(new []
            {
                new TextEntityViewModel("Add "),
                new MonospaceTextEntityViewModel("<StyleInclude Source=\"avares://Material.WindowStyle/WindowStyleInclude.axaml\"/>"),
                new TextEntityViewModel(" to your styles of Application. It should be after theme styles and before your custom styles.")
            }),
            new BulletItemEntityViewModel(new []
            {
                new TextEntityViewModel("Add those attributes to your "),
                new MonospaceTextEntityViewModel("Window"),
                new TextEntityViewModel(" view:")
            }),
            new MonospaceTextEntityViewModel("ExtendClientAreaToDecorationsHint=\"True\""),
            new BulletItemEntityViewModel(new []
            {
                new TextEntityViewModel("(Optional) You may want apply fix for Windows Platform. This fix used for correcting margin while the Window is maximized. Windows OS could overfill the window."),
                new TextEntityViewModel("To apply this fix, just add "),
                new MonospaceTextEntityViewModel("windows-platform-fix"),
                new TextEntityViewModel(" to your "),
                new MonospaceTextEntityViewModel("Classes"),
                new TextEntityViewModel(" property of window. This can be done automatically or manually, depending how you create your avalonia application.  ")
            }),
            
            new TextEntityViewModel("This area is not ready!")
            {
                FontStyle = FontStyle.Italic
            }
        };

        private string _source = string.Empty;
        public string Source
        {
            get => _source;
            set
            {
                _source = value;

                if (!value.ToLower().StartsWith("avares://"))
                    return;

                var service = AvaloniaLocator.Current.GetService(typeof(IAssetLoader));
                if (service is not IAssetLoader assetLoader)
                    return;
            
                using (var stream = assetLoader.Open(new Uri(value)))
                {
                
                }
            }
        }
    }
}