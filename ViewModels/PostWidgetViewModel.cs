using Avalonia.RedditMVVM.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Avalonia.RedditMVVM.ViewModels;

public partial class PostWidgetViewModel : ObservableRecipient, IRecipient<PropertyChangedMessage<Post?>>
{
    [ObservableProperty]
    private Post? post;

    public void Receive(PropertyChangedMessage<Post?> message)
    {
        if (message.Sender is SubredditWidgetViewModel && message.PropertyName == nameof(SubredditWidgetViewModel.SelectedPost))
        {
            Post = message.NewValue;
        }
    }
}
