using BlazorServerConduit.Models;

namespace BlazorServerConduit.Store.Articles.Actions
{
    public record FetchArticlesAction(ArticleFilter Filter);
}
