using Avalonia.Controls;
using Material.WindowStyle.Demo.Views.ViewModels.Entities;

namespace Material.WindowStyle.Demo.Views.Resources
{
    public class EntityViewerTemplates : ResourceDictionary
    {
        private void OnEntityElementTemplateSelectTemplateKey(object? sender, SelectTemplateEventArgs e)
        {
            e.TemplateKey = e.DataContext switch
            {
                LinkEntityViewModel => "LinkText",
                HeaderTextEntityViewModel => "Header",
                TextEntityViewModel => "Text",
                BulletItemEntityViewModel => "BulletItem",
                CombinedEntityViewModel => "Combination",
                PictureEntityViewModel => "Picture",
                EntityViewModel => "Base",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}