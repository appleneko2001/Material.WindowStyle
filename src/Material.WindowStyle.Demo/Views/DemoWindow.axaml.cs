using System.Collections.Immutable;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Material.WindowStyle.Chrome;
using Material.WindowStyle.Demo.Interop;

namespace Material.WindowStyle.Demo.Views;

public class DemoWindow : Window
{
    public DemoWindow()
    {
        InitializeComponent();
        
        this.AttachDevTools();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        var titleBar = e.NameScope.Find<MaterialTitleBar>("PART_TitleBar");
        
        titleBar.TemplateApplied += OnTitleBarTemplateApplied;
    }

    private void OnTitleBarTemplateApplied(object? sender, TemplateAppliedEventArgs e)
    {
        if (sender is not MaterialTitleBar titleBar)
            return;

        var search = titleBar.GetTemplateChildren().Where(control =>
            control is Border && control.Name == "PART_AppTitleBorder");

        var result = search.ToImmutableArray();

        if (result.Length > 0)
        {
            var appbar = result.First();
        
            appbar?.AddHandler(ContextRequestedEvent, Handler);
        }
        
        titleBar.TemplateApplied -= OnTitleBarTemplateApplied;
    }

    // Show system menu on context menu requested by main appbar
    private void Handler(object? sender, ContextRequestedEventArgs e)
    {
        var handle = PlatformImpl.Handle.Handle;
        
        // Works on windows platform only.
        // Also it requires window should have full system decorations (SystemDecorations="Full")
        // otherwise the GetSystemMenu method will always return null pointer.
        
        // You can hide border by ExtendClientAreaToDecorationsHint="True".
        // credit: https://stackoverflow.com/questions/15129218/show-system-menu-from-another-process-using-winforms-c
        ShowContextMenu(handle, handle);
    }

    private static void ShowContextMenu(IntPtr appWindow, IntPtr myWindow)
    {
        WinApiMethods.GetCursorPos(out var point);
        
        var wMenu = WinApiMethods.GetSystemMenu(appWindow, false);
        // Display the menu
        var command = WinApiMethods.TrackPopupMenuEx(wMenu, 0x0100, point.X, point.Y, myWindow, IntPtr.Zero);

        // 0x0112 - WM_SYSCOMMAND
        WinApiMethods.PostMessage(appWindow, 0x0112, new IntPtr(command), IntPtr.Zero);
    }
}