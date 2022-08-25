using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Platform;
using Material.WindowStyle.Demo.Views.Models.Entities;
using MinimalMvvm.ViewModels;

namespace Material.WindowStyle.Demo.Views.Models
{
    public class DemoWindowViewModel : ViewModelBase
    {
        public AvaloniaList<EntityViewModel> Requirements { get; } = new()
        {
            new BulletItemEntityViewModel
            {
                Entities =
                {
                    new LinkEntityViewModel("Material.Avalonia toolkit")
                    {
                        Href = "https://github.com/AvaloniaCommunity/Material.Avalonia",
                        ToolTip = "This toolkit is a collection of styles to help you customize your Avalonia application theme with Material Design."
                    },
                    new TextEntityViewModel(" version 2.4.0 or newer.")
                }
            },
            new BulletItemEntityViewModel
            {
                Entities =
                {
                    new LinkEntityViewModel("AvaloniaUI framework")
                    {
                        Href = "https://github.com/AvaloniaUI/Avalonia",
                        ToolTip = "Avalonia is a cross-platform XAML-based UI framework providing a flexible styling system and supporting a wide range of Operating Systems such as Windows via .NET Framework and .NET Core, Linux via Xorg, macOS."
                    },
                    new TextEntityViewModel(" version 0.10.7 or newer.")
                }
            }
        };
        public AvaloniaList<EntityViewModel> Steps { get; } = new()
        {
            new TextEntityViewModel("This area is not ready!")
            {
                FontStyle = FontStyle.Italic
            }
        };

        private string _source;
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