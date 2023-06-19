using BlazorServerConduit.Models;
using Fluxor;

namespace BlazorServerConduit.Store
{
    [FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
    public record ArticleState(Article? Article, bool IsLoading)
    {
        public static ArticleState CreateInitialState()
        {
            return new ArticleState(null, false);
        }
    }
}
