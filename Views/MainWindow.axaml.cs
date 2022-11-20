using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Immutable;

using FluentAvalonia.Styling;
using FluentAvalonia.UI.Media;
using FluentAvalonia.UI.Windowing;

using System;
using System.Runtime.InteropServices;

namespace Avalonia.RedditMVVM.Views
{
    public partial class MainWindow : AppWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //TitleBar.ExtendsContentIntoTitleBar = true;
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);
            var thm = AvaloniaLocator.Current.GetService<FluentAvaloniaTheme>()!;
            thm.RequestedThemeChanged += OnRequestedThemeChanged;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (IsWindows11 && thm.RequestedTheme != FluentAvaloniaTheme.HighContrastModeString)
                {
                    TransparencyBackgroundFallback = Brushes.Transparent;
                    TransparencyLevelHint = WindowTransparencyLevel.Mica;
                    TryEnableMicaEffect(thm);
                }
            }
        }

        protected override void OnRequestedThemeChanged(FluentAvaloniaTheme sender, RequestedThemeChangedEventArgs args)
        {
            base.OnRequestedThemeChanged(sender, args);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (IsWindows11 && args.NewTheme != FluentAvaloniaTheme.HighContrastModeString)
                {
                    TryEnableMicaEffect(sender);
                }
                else if (args.NewTheme == FluentAvaloniaTheme.HighContrastModeString)
                {
                    SetValue(BackgroundProperty, AvaloniaProperty.UnsetValue);
                }
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void TryEnableMicaEffect(FluentAvaloniaTheme thm)
        {
            if (thm.RequestedTheme == FluentAvaloniaTheme.DarkModeString)
            {
                var color = this.TryFindResource("SolidBackgroundFillColorBase", out var value) ? (Color2)(Color)value : new Color2(32, 32, 32);
                color = color.LightenPercent(-0.8f);
                Background = new ImmutableSolidColorBrush(color, 0.78);
            }
            else if (thm.RequestedTheme == FluentAvaloniaTheme.LightModeString)
            {
                var color = this.TryFindResource("SolidBackgroundFillColorBase", out var value) ? (Color2)(Color)value : new Color2(243, 243, 243);
                color = color.LightenPercent(0.5f);
                Background = new ImmutableSolidColorBrush(color, 0.9);
            }
        }
    }
}
