using Avalonia.RedditMVVM.Models;
using Avalonia.RedditMVVM.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Nito.AsyncEx;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Avalonia.RedditMVVM.ViewModels;

public partial class SubredditWidgetViewModel : ObservableRecipient
{
    private readonly AsyncLock loadingLock = new();

    private readonly IRedditService reddit;

    private readonly ISettingsService settings;

    private Post? selectedPost;

    private string? selectedSubreddit;

    public SubredditWidgetViewModel(ISettingsService settings, IRedditService reddit)
    {
        LoadPostsCommand = new AsyncRelayCommand(LoadPostsAsync);
        this.reddit = reddit;
        this.settings = settings;
        SelectedSubreddit = settings.GetValue<string>(nameof(SelectedSubreddit)) ?? Subreddits[0];
    }

    public IAsyncRelayCommand LoadPostsCommand { get; }

    public ObservableCollection<object> Posts { get; } = new();

    public Post? SelectedPost
    {
        get => selectedPost;
        set => SetProperty(ref selectedPost, value, true);
    }

    public string? SelectedSubreddit
    {
        get => selectedSubreddit;
        set
        {
            SetProperty(ref selectedSubreddit, value);
            settings.SetValue(nameof(SelectedSubreddit), value);
            LoadPostsCommand.Cancel();
            LoadPostsCommand.Execute(this);
        }
    }

    public IReadOnlyList<string> Subreddits { get; } = new[] {
        "avaloniaui",
        "csharp",
        "dotnet",
        "fsharp"
    };

    private async Task LoadPostsAsync()
    {
        if (SelectedSubreddit is null)
        {
            return;
        }

        using var lck = await loadingLock.LockAsync();
        var response = await reddit.GetSubredditPostsAsync(SelectedSubreddit);
        Posts.Clear();
        foreach (var item in response.Data.Items)
        {
            Posts.Add(item.Data);
        }
    }
}
