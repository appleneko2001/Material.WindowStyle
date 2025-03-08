using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Reactive;

namespace Material.WindowStyle.Chrome
{
    [PseudoClasses(":minimized", ":normal", ":maximized", ":fullscreen")]
    public class MaterialTitleBarButtons : TemplatedControl
    {
        private const string PartNameCloseButton = "PART_CloseButton";
        private const string PartNameRestoreButton = "PART_RestoreButton";
        private const string PartNameMinimizeButton = "PART_MinimizeButton";
        private const string PartNameFullscreenButton = "PART_FullScreenButton";
        
        public static readonly StyledProperty<bool> IsReversedProperty =
            AvaloniaProperty.Register<MaterialTitleBarButtons, bool>(nameof(IsReversed));

        public bool IsReversed
        {
            get => GetValue(IsReversedProperty);
            set => SetValue(IsReversedProperty, value);
        }

        private List<IDisposable>? _disposablesList;
        private Window? _hostWindow;

        public void Attach(Window hostWindow)
        {
            DisposeObservables();

            _hostWindow = hostWindow;

            _disposablesList = new List<IDisposable>
            {
                _hostWindow.GetObservable(Window.WindowStateProperty)
                    .Subscribe(new AnonymousObserver<WindowState>(x =>
                    {
                        PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                        PseudoClasses.Set(":normal", x == WindowState.Normal);
                        PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                        PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                    }))
            };
        }

        private void DisposeObservables()
        {
            var list = _disposablesList;
            
            if (list == null)
                return;
            
            foreach (var disposable in list)
                disposable.Dispose();

            list.Clear();
            _disposablesList = null;
        }

        public void Detach()
        {
            DisposeObservables();
            _hostWindow = null;
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            var basicButtonsIds = new []{PartNameCloseButton, PartNameRestoreButton, PartNameMinimizeButton, PartNameFullscreenButton};

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
                    case PartNameCloseButton:
                    {
                        var d = b.AddDisposableHandler(Button.ClickEvent, OnCloseButtonClicked);
                        _disposablesList?.Add(d);
                    } break;
                    
                    case PartNameRestoreButton:
                    {
                        var d = b.AddDisposableHandler(Button.ClickEvent, OnRestoreButtonClicked);
                        _disposablesList?.Add(d);
                    } break;
                    
                    case PartNameMinimizeButton:
                    {
                        var d = b.AddDisposableHandler(Button.ClickEvent, OnMinimiseButtonClicked);
                        _disposablesList?.Add(d);
                    } break;
                    
                    case PartNameFullscreenButton:
                    {
                        var d = b.AddDisposableHandler(Button.ClickEvent, OnFullScreenButtonClicked);
                        _disposablesList?.Add(d);
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