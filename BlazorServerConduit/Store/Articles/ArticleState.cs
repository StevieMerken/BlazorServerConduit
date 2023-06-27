using BlazorServerConduit.Models;
using Fluxor;

namespace BlazorServerConduit.Store.Articles
{
    [FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
    public record ArticleState(Article[] Articles, int ArticlesCount, bool IsLoadingArticles, Article? SelectedArticle, bool IsLoadingArticle)
    {
        public static ArticleState CreateInitialState()
        {
            return new ArticleState(Array.Empty<Article>(), 0, false, null, false);
        }
    }
}
