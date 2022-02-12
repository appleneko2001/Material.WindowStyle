using System;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Material.WindowStyle.Chrome
{
    [PseudoClasses(":minimized", ":normal", ":maximized", ":fullscreen")]
    public class MaterialTitleBarButtons : TemplatedControl
    {
        private const string T_CloseButton = "PART_CloseButton";
        private const string T_RestoreButton = "PART_RestoreButton";
        private const string T_MinimizeButton = "PART_MinimizeButton";
        private const string T_FullscreenButton = "PART_FullScreenButton";
        
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

            var basicButtonsIds = new []{T_CloseButton, T_RestoreButton, T_MinimizeButton, T_FullscreenButton};

            foreach (var id in basicButtonsIds)
            {
                var control = e.NameScope.Get<TemplatedControl>(id);
                control.TemplateApplied += OnButtonContainerTemplateApplied;
            }
        }

        private void OnButtonContainerTemplateApplied(object sender, TemplateAppliedEventArgs e)
        {
            if (sender is not TemplatedControl c)
                return;

            var b = e.NameScope.Find<Button>("PART_Button");
            if (b != null)
            {
                switch (c.Name)
                {
                    case T_CloseButton:
                    {
                        var d = b.AddDisposableHandler(Button.ClickEvent, OnCloseButtonClicked);
                        _disposables?.Add(d);
                    } break;
                    
                    case T_RestoreButton:
                    {
                        var d = b.AddDisposableHandler(Button.ClickEvent, OnRestoreButtonClicked);
                        _disposables?.Add(d);
                    } break;
                    
                    case T_MinimizeButton:
                    {
                        var d = b.AddDisposableHandler(Button.ClickEvent, OnMinimiseButtonClicked);
                        _disposables?.Add(d);
                    } break;
                    
                    case T_FullscreenButton:
                    {
                        var d = b.AddDisposableHandler(Button.ClickEvent, OnFullScreenButtonClicked);
                        _disposables?.Add(d);
                    } break;
                }
            }

            c.TemplateApplied -= OnButtonContainerTemplateApplied;
        }

        private void OnFullScreenButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_hostWindow != null)
            {
                _hostWindow!.WindowState = _hostWindow.WindowState == WindowState.FullScreen
                    ? WindowState.Normal
                    : WindowState.FullScreen;
            }
        }

        private void OnMinimiseButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_hostWindow != null)
            {
                _hostWindow!.WindowState = WindowState.Minimized;
            }
        }

        private void OnRestoreButtonClicked(object sender, RoutedEventArgs e)
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

        private void OnCloseButtonClicked(object sender, RoutedEventArgs e)
        {
            _hostWindow?.Close();
        }
    }
}