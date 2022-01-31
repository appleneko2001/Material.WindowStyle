using System;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace Material.WindowStyle.Chrome
{
    [PseudoClasses(":minimized", ":normal", ":maximized", ":fullscreen")]
    public class MaterialTitleBarButtons : TemplatedControl
    {
        public static readonly StyledProperty<bool> IsReversedProperty =
            AvaloniaProperty.Register<MaterialTitleBarButtons, bool>(nameof(IsReversed));

        public bool IsReversed
        {
            get => GetValue(IsReversedProperty);
            set => SetValue(IsReversedProperty, value);
        }

        private CompositeDisposable? _disposables;
        private Window? _hostWindow;

        public void Attach(Window hostWindow)
        {
            if (_disposables != null)
            {
                if (!_disposables.IsDisposed)
                    _disposables.Dispose();

                _disposables = null;
            }

            _hostWindow = hostWindow;

            _disposables = new CompositeDisposable
            {
                _hostWindow.GetObservable(Window.WindowStateProperty)
                    .Subscribe(x =>
                    {
                        PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                        PseudoClasses.Set(":normal", x == WindowState.Normal);
                        PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                        PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                    })
            };
        }

        public void Detach()
        {
            if (_disposables == null)
                return;

            _disposables.Dispose();
            _disposables = null;

            _hostWindow = null;
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            var closeButton = e.NameScope.Get<TemplatedControl>("PART_CloseButton");
            var restoreButton = e.NameScope.Get<TemplatedControl>("PART_RestoreButton");
            var minimizeButton = e.NameScope.Get<TemplatedControl>("PART_MinimizeButton");
            var fullScreenButton = e.NameScope.Get<TemplatedControl>("PART_FullScreenButton");

            closeButton.PointerReleased += OnCloseButtonOnPointerReleased;

            restoreButton.PointerReleased += OnRestoreButtonOnPointerReleased;

            minimizeButton.PointerReleased += OnMinimiseButtonOnPointerReleased;

            fullScreenButton.PointerReleased += OnFullScreenButtonOnPointerReleased;
        }

        private void OnFullScreenButtonOnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (_hostWindow != null)
            {
                _hostWindow!.WindowState = _hostWindow.WindowState == WindowState.FullScreen
                    ? WindowState.Normal
                    : WindowState.FullScreen;
            }
        }

        private void OnMinimiseButtonOnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (_hostWindow != null)
            {
                _hostWindow!.WindowState = WindowState.Minimized;
            }
        }

        private void OnRestoreButtonOnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            SwitchMaximizeWindowState();
        }

        internal void SwitchMaximizeWindowState()
        {
            if (_hostWindow != null)
            {
                _hostWindow!.WindowState = _hostWindow.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
        }

        private void OnCloseButtonOnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            _hostWindow?.Close();
        }
    }
}