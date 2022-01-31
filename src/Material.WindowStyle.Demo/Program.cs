// See https://aka.ms/new-console-template for more information

using Avalonia;
using Material.WindowStyle.Demo;

var builder = AppBuilder.Configure<DemoApp>().UsePlatformDetect();
builder.StartWithClassicDesktopLifetime(Array.Empty<string>());