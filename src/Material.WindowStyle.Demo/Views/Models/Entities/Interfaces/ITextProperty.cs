using Avalonia.Media;

namespace Material.WindowStyle.Demo.Views.Models.Entities.Interfaces;

public interface ITextProperty
{
    string Text { get; }
    
    FontStyle FontStyle { get; }
}