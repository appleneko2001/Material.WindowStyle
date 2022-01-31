﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Material.WindowStyle.Demo.Views;

public class DemoWindow : Window
{
    public DemoWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}