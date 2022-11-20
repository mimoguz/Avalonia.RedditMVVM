using Avalonia;
using Avalonia.Controls;

using Avalonia.RedditMVVM.ViewModels;
using System.Reactive.Linq;
using System;

using CommunityToolkit.Mvvm.DependencyInjection;

namespace Avalonia.RedditMVVM.Views;

public partial class SubredditWidget : UserControl
{
    public SubredditWidget()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetService<SubredditWidgetViewModel>();
    }
}
