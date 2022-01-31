#### Welcome to Material.WindowStyle Demo!

To use this window style in your Avalonia Application, please follow below steps!

###### Requirements:
* [Material.Avalonia toolkit](https://github.com/AvaloniaCommunity/Material.Avalonia "This toolkit is a collection of styles to help you customize your Avalonia application theme with Material Design.") version 2.4.0 or newer.
* [AvaloniaUI framework](https://github.com/AvaloniaUI/Avalonia "Avalonia is a cross-platform XAML-based UI framework providing a flexible styling system and supporting a wide range of Operating Systems such as Windows via .NET Framework and .NET Core, Linux via Xorg, macOS.") version 0.10.7 or newer.

###### Steps:
1. Update your Avalonia UI framework to latest. Make sure your framework version is newer or equal 0.10.7.
2. Install Material.Avalonia toolkit or update it to latest. (optional, you should override the default background / foreground brush before use this without this toolkit.)
3. Add next tags to your window:
```xml
        SystemDecorations="BorderOnly"
        ExtendClientAreaToDecorationsHint="True"
```
Your window xaml code will 
TODO: complete this readme