using Avalonia.Media.Imaging;

using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Avalonia.RedditMVVM.Models;

public class Post : ObservableObject
{
    private static readonly HttpClient http = new();

    private string thumbnail = string.Empty;
    private Bitmap? thumbnailImage = null;

    [JsonPropertyName("selftext")]
    public string SelfText { get; set; } = string.Empty;

    [JsonPropertyName("thumbnail")]
    public string Thumbnail
    {
        get => thumbnail;
        set
        {
            thumbnail = value;
            var _ = LoadThumbnailAsync();
        }
    }

    [JsonIgnore]
    public Bitmap? ThumbnailImage { get => thumbnailImage; set => SetProperty(ref thumbnailImage, value); }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    private async Task LoadThumbnailAsync()
    {
        if (string.IsNullOrEmpty(Thumbnail))
        {
            return;
        }

        try
        {
            var data = await http.GetByteArrayAsync(Thumbnail);
            ThumbnailImage = new Bitmap(new MemoryStream(data));
        }
        catch { }
    }
}

public sealed record PostData([property: JsonPropertyName("data")] Post Data);

public sealed record PostListing([property: JsonPropertyName("children")] IList<PostData> Items);

public sealed record PostsQueryResponse([property: JsonPropertyName("data")] PostListing Data);
