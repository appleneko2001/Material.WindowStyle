using Avalonia.Media;
using Material.WindowStyle.Demo.Views.ViewModels.Entities.Interfaces;

namespace Material.WindowStyle.Demo.Views.ViewModels.Entities
{
    public class LinkEntityViewModel : ClickableEntityViewModel, ITextProperty
    {
        private string? _text;

        public string? Text
        {
            get => _text;
            protected set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private FontStyle _fontStyle;
        public FontStyle FontStyle
        {
            get => _fontStyle;
            set
            {
                _fontStyle = value;
                OnPropertyChanged();
            }
        }

        public LinkEntityViewModel(string? text = null)
        {
            _text = text;
        }
    }
}