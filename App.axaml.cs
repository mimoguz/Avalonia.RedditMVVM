using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Avalonia.RedditMVVM.Services;
using Avalonia.RedditMVVM.ViewModels;

using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

using Refit;

namespace Avalonia.RedditMVVM;

public partial class App : Application
{
    public override void Initialize()
    {
        Ioc.Default.ConfigureServices(
           new ServiceCollection()
               .AddSingleton<ISettingsService, SettingsService>()
               .AddSingleton(RestService.For<IRedditService>("https://www.reddit.com/"))
               .AddTransient<SubredditWidgetViewModel>()
               .AddTransient<PostWidgetViewModel>()
               .BuildServiceProvider()
       );

        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Views.MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
