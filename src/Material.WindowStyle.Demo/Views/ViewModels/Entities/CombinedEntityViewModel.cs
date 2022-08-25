using Avalonia.Collections;

namespace Material.WindowStyle.Demo.Views.ViewModels.Entities
{
    public class CombinedEntityViewModel : EntityViewModel
    {
        public CombinedEntityViewModel()
        {
            _entities = new AvaloniaList<EntityViewModel>();
        }
        
        public CombinedEntityViewModel(IEnumerable<EntityViewModel> entities)
        {
            _entities = new AvaloniaList<EntityViewModel>(entities);
        }
        
        private readonly AvaloniaList<EntityViewModel> _entities;
        public AvaloniaList<EntityViewModel> Entities => _entities;
    }
}