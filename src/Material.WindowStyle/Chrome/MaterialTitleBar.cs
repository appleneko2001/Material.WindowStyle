using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Platform;
using Avalonia.Reactive;

namespace Material.WindowStyle.Chrome
{
    [PseudoClasses(":minimized", ":normal", ":maximized", ":fullscreen", ":left-aligned-buttons",
        ":right-aligned-buttons")]
    public class MaterialTitleBar : TemplatedControl
    {
        private MaterialTitleBarButtons? _captionButtons;
        private IReadOnlyList<IDisposable>? _disposablesList;
        private Border? _dragZone;
        private DateTime _prevClickDragZone;

        public static readonly StyledProperty<string?> TitleProperty =
            Window.TitleProperty.AddOwner<TitleBar>();

        public string? Title
        {
            get => GetValue(TitleProperty);
            private set => SetValue(TitleProperty, value);
        }

        public static readonly StyledProperty<TitleButtonAlignment> ButtonsAlignProperty =
            AvaloniaProperty.Register<TitleBar, TitleButtonAlignment>(nameof(ButtonsAlign));

        public TitleButtonAlignment ButtonsAlign
        {
            get => GetValue(ButtonsAlignProperty);
            set => SetValue(ButtonsAlignProperty, value);
        }

        private void UpdateState(Window? window)
        {
            if (window is not null)
            {
                var fullDecor = window.SystemDecorations == SystemDecorations.Full;
                
                var result = fullDecor && window.ExtendClientAreaChromeHints == ExtendClientAreaChromeHints.NoChrome;
                result = result && window.ExtendClientAreaChromeHints == ExtendClientAreaChromeHints.PreferSystemChrome;
                //result = result && (window.PlatformImpl?.NeedsManagedDecorations ?? false);

                IsVisible = !result;
            }
            else
            {
                IsVisible = true;
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _captionButtons?.Detach();

            _captionButtons = e.NameScope.Find<MaterialTitleBarButtons>("PART_CaptionButtons");

            if (VisualRoot is Window window)
            {
                _captionButtons?.Attach(window);

                UpdateState(window);
            }

            _dragZone = e.NameScope.Find<Border>("PART_WindowDragZone");

            if (_dragZone != null)
            {
                _dragZone.PointerPressed += DragZonePointerPressed;
                _dragZone.PointerReleased += DragZonePointerReleased;
            }
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);

            if (VisualRoot is not Window window)
                return;

            _disposablesList = new []
            {
                window.GetObservable(Window.WindowDecorationMarginProperty)
                    .Subscribe(new AnonymousObserver<Thickness>(_ => UpdateState(window))),
                window.GetObservable(Window.ExtendClientAreaTitleBarHeightHintProperty)
                    .Subscribe(new AnonymousObserver<double>(_ => UpdateState(window))),
                window.GetObservable(Window.OffScreenMarginProperty)
                    .Subscribe(new AnonymousObserver<Thickness>(_ => UpdateState(window))),
                window.GetObservable(Window.ExtendClientAreaChromeHintsProperty)
                    .Subscribe(new AnonymousObserver<ExtendClientAreaChromeHints>(_ => UpdateState(window))),
                window.GetObservable(Window.WindowStateProperty)
                    .Subscribe(new AnonymousObserver<WindowState>(delegate(WindowState x)
                    {
                        PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                        PseudoClasses.Set(":normal", x == WindowState.Normal);
                        PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                        PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                    })),
                window.GetObservable(Window.IsExtendedIntoWindowDecorationsProperty)
                    .Subscribe(new AnonymousObserver<bool>(_ => UpdateState(window))),
                window.GetObservable(Window.TitleProperty)
                    .Subscribe(new AnonymousObserver<string?>(s => Title = s)),

                this.GetObservable(ButtonsAlignProperty).Subscribe(new AnonymousObserver<TitleButtonAlignment>(
                    delegate(TitleButtonAlignment alignment)
                    {
                        PseudoClasses.Set(":left-aligned-buttons", alignment == TitleButtonAlignment.Left);
                        PseudoClasses.Set(":right-aligned-buttons", alignment == TitleButtonAlignment.Right);
                    }))
            };
        }

        private void DragZonePointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (VisualRoot is not Window window)
                return;

            if (window.WindowState == WindowState.FullScreen)
                return;

            window.BeginMoveDrag(e);
        }

        private void DragZonePointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (VisualRoot is not Window window)
                return;

            var delta = DateTime.Now - _prevClickDragZone;

            // If delta is less than 300ms
            if (delta.TotalSeconds < 0.3)
            {
                window.WindowState = window.WindowState == WindowState.Normal
                    ? WindowState.Maximized
                    : WindowState.Normal;
            }

            _prevClickDragZone = DateTime.Now;
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            if (_dragZone != null)
            {
                _dragZone.PointerPressed -= DragZonePointerPressed;
                _dragZone.PointerReleased -= DragZonePointerReleased;
                _dragZone = null;
            }

            base.OnDetachedFromVisualTree(e);

            if (_disposablesList != null)
                foreach (var disposable in _disposablesList)
                    disposable.Dispose();

            _captionButtons?.Detach();
            _captionButtons = null;
        }
    }
}