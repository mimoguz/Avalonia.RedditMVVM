using System.Threading.Tasks;

using Avalonia.RedditMVVM.Models;

using Refit;

namespace Avalonia.RedditMVVM.Services;

public interface IRedditService
{
    [Get("/r/{subreddit}/.json")]
    Task<PostsQueryResponse> GetSubredditPostsAsync(string subreddit);
}
