using Avalonia.Media;

namespace Material.WindowStyle.Demo.Views.Models.Entities
{
    public class HeaderTextEntityViewModel : TextEntityViewModel
    {
        private int _kind;

        public int Kind
        {
            get => _kind;
            protected set
            {
                if (_kind is < 0 or > 6)
                    throw new ArgumentOutOfRangeException(nameof(value));
            
                _kind = value;
                OnPropertyChanged();

                FontSize = _kind switch
                {
                    1 => 32.0,
                    2 => 24.0,
                    3 => 18.72,
                    4 => 16,
                    5 => 13.28,
                    6 => 10.72,
                    _ => 11
                };
                OnPropertyChanged(nameof(FontSize));
            }
        }
    
        public double FontSize { get; private set; }

        public FontWeight FontWeight => FontWeight.Bold;
    }
}