using MinimalMvvm.ViewModels;

namespace Material.WindowStyle.Demo.Views.Models.Entities
{
    public class EntityViewModel : ViewModelBase
    {
        private string? _toolTip;

        public string? ToolTip
        {
            get => _toolTip;
            set
            {
                _toolTip = value;
                OnPropertyChanged();
            }
        }
    }
}