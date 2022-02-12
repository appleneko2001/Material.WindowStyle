﻿using System;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace Material.WindowStyle.Chrome
{
    [PseudoClasses(":minimized", ":normal", ":maximized", ":fullscreen", ":left-aligned-buttons",
        ":right-aligned-buttons")]
    public class MaterialTitleBar : TemplatedControl
    {
        private CompositeDisposable? _disposables;
        private MaterialTitleBarButtons? _captionButtons;
        private Border? _dragzone;
        private DateTime _prevClickDragzone;

        public static readonly StyledProperty<string> TitleProperty =
            Window.TitleProperty.AddOwner<TitleBar>();

        public object Title
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
            IsVisible = window?.PlatformImpl.NeedsManagedDecorations ?? true;
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

            _dragzone = e.NameScope.Find<Border>("PART_WindowDragZone");

            if (_dragzone != null)
            {
                _dragzone.PointerPressed += DragZonePointerPressed;
                _dragzone.PointerReleased += DragZonePointerReleased;
            }
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);

            if (VisualRoot is not Window window)
                return;

            _disposables = new CompositeDisposable
            {
                window.GetObservable(Window.WindowDecorationMarginProperty)
                    .Subscribe(delegate { UpdateState(window); }),
                window.GetObservable(Window.ExtendClientAreaTitleBarHeightHintProperty)
                    .Subscribe(delegate { UpdateState(window); }),
                window.GetObservable(Window.OffScreenMarginProperty)
                    .Subscribe(delegate { UpdateState(window); }),
                window.GetObservable(Window.ExtendClientAreaChromeHintsProperty)
                    .Subscribe(delegate { UpdateState(window); }),
                window.GetObservable(Window.WindowStateProperty)
                    .Subscribe(delegate(WindowState x)
                    {
                        PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                        PseudoClasses.Set(":normal", x == WindowState.Normal);
                        PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                        PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                    }),
                window.GetObservable(Window.IsExtendedIntoWindowDecorationsProperty)
                    .Subscribe(delegate { UpdateState(window); }),
                window.GetObservable(Window.TitleProperty).Subscribe(delegate(string s) { Title = s; }),

                this.GetObservable(ButtonsAlignProperty).Subscribe(
                    delegate(TitleButtonAlignment alignment)
                    {
                        PseudoClasses.Set(":left-aligned-buttons", alignment == TitleButtonAlignment.Left);
                        PseudoClasses.Set(":right-aligned-buttons", alignment == TitleButtonAlignment.Right);
                    })
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

            var delta = DateTime.Now - _prevClickDragzone;

            // If delta is less than 300ms
            if (delta.TotalSeconds < 0.3)
            {
                window.WindowState = window.WindowState == WindowState.Normal
                    ? WindowState.Maximized
                    : WindowState.Normal;
            }

            _prevClickDragzone = DateTime.Now;
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            if (_dragzone != null)
            {
                _dragzone.PointerPressed -= DragZonePointerPressed;
                _dragzone.PointerReleased -= DragZonePointerReleased;
                _dragzone = null;
            }

            base.OnDetachedFromVisualTree(e);

            _disposables?.Dispose();

            _captionButtons?.Detach();
            _captionButtons = null;
        }
    }
}