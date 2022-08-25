using Avalonia.Collections;

namespace Material.WindowStyle.Demo.Views.ViewModels.Entities
{
    public class CombinedEntityViewModel : EntityViewModel
    {
        private readonly AvaloniaList<EntityViewModel> _entities = new();
        public AvaloniaList<EntityViewModel> Entities => _entities;
    }
}