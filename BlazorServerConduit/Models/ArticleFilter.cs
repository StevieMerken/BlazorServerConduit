namespace BlazorServerConduit.Models
{
    public record ArticleFilter(FeedType FeedType = FeedType.Global, string? Tag = null, int Offset = 0, int Limit = 10);

    public enum FeedType
    {
        Global,
        Personal,
        Tag
    }
}
