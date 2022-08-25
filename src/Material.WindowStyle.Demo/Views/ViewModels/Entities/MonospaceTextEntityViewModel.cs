using MinimalMvvm.Extensions;

namespace Material.WindowStyle.Demo.Views.ViewModels.Entities
{
    public class MonospaceTextEntityViewModel : TextEntityViewModel
    {
        public MonospaceTextEntityViewModel(string? text = null) : base(text) { }

        public string ToolTipClickToCopy => IsCopiedToolTipVisible ? "Copied!" : "Click to copy";

        public bool IsPointerPressed;
        
        private bool _isCopiedToolTipVisible;
        public bool IsCopiedToolTipVisible
        {
            get => _isCopiedToolTipVisible;
            set
            {
                this.SetAndUpdateIfChanged(ref _isCopiedToolTipVisible, value);
                OnPropertyChanged(nameof(ToolTipClickToCopy));
            }
        }
    }
}