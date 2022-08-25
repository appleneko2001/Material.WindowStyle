namespace Material.WindowStyle.Demo.Views.ViewModels.Entities
{
    public class ClickableEntityViewModel : EntityViewModel
    {
        private string? _href;
        public string? Href
        {
            get => _href;
            set
            {
                _href = value;
                OnPropertyChanged();
            }
        }
    
        private string? _altText;
        public string? AltText
        {
            get => _altText;
            set
            {
                _altText = value;
                OnPropertyChanged();
            }
        }
    }
}