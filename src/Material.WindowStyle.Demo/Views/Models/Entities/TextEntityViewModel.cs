using Avalonia.Media;
using Material.WindowStyle.Demo.Views.Models.Entities.Interfaces;

namespace Material.WindowStyle.Demo.Views.Models.Entities
{
    public class TextEntityViewModel : EntityViewModel, ITextProperty
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

        public TextEntityViewModel(string? text = null)
        {
            _text = text;
        }
    }
}