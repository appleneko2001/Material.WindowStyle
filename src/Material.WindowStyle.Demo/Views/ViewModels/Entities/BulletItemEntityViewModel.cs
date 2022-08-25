namespace Material.WindowStyle.Demo.Views.ViewModels.Entities
{
    public class BulletItemEntityViewModel : CombinedEntityViewModel
    {
        public BulletItemEntityViewModel(){}
        public BulletItemEntityViewModel(IEnumerable<EntityViewModel> entities) : base(entities){}
    }
}