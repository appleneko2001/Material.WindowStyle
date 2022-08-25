## Welcome to Material.WindowStyle Demo!

## Getting started
To use this window style in your Avalonia Application, please follow below steps!

### Requirements:
* [AvaloniaUI framework](https://github.com/AvaloniaUI/Avalonia "Avalonia is a cross-platform XAML-based UI framework providing a flexible styling system and supporting a wide range of Operating Systems such as Windows via .NET Framework and .NET Core, Linux via Xorg, macOS.") version 11.0.0-preview1 or newer.

### Steps:
1. Update your Avalonia UI framework to latest. Make sure your framework version is newer or equal 11.0.0-preview1.
2. Install Material.WindowStyle package from NuGet. The major and minor package version should be 0.11. Build and revision version should be as newer as possible so you wont miss updates.
3. Add `<StyleInclude Source="avares://Material.WindowStyle/WindowStyleInclude.axaml"/>` to your styles of Application. It should be after theme styles and before your custom styles.
4. Add those attributes to your `Window` view:
```xml
ExtendClientAreaToDecorationsHint="True"
```
5. (Optional) You may want apply fix for Windows Platform. This fix used for correcting margin while the Window is maximized. Windows OS could overfill the window.
To apply this fix, just add `windows-platform-fix` to your `Classes` property of window. This can be done automatically or manually, depending how you create your avalonia application.  
<!-- Windows sucks, but it used for most people, including me. Shit -->
TODO: complete this readme

## Bonus:

### Change colours of title bar
Add this style to your application xaml
```xml
<Style Selector="chrome|MaterialTitleBar">
    <Setter Property="Background" Value="#4C4C4C"/>
    <Setter Property="Foreground" Value="White"/>
</Style>
```
This will set material title bar colours to all windows by default. Background will be gray, and the foreground will be white.