using BlazorServerConduit.Services;
using BlazorServerConduit.Store.Articles.Actions;
using Fluxor;

namespace BlazorServerConduit.Store.Articles.Effects
{
    public class Effects
    {
        private readonly ArticlesService Http;

        public Effects(ArticlesService http)
        {
            Http = http;
        }

        [EffectMethod]
        public async Task HandleFetchArticlesAction(FetchArticlesAction action, IDispatcher dispatcher)
        {
            var articles = await Http.GetArticlesAsync(action.Filter);
            if (articles is not null)
            {
                dispatcher.Dispatch(new FetchArticlesResultAction(articles));
            }
        }

        [EffectMethod]
        public async Task HandleFetchArticleAction(FetchArticleAction action, IDispatcher dispatcher)
        {
            var articleReponse = await Http.GetArticleAsync(action.Slug);
            if (articleReponse.IsSuccess)
            {
                dispatcher.Dispatch(new FetchArticleResultAction(articleReponse.SuccessData.Article));
            }
        }

        [EffectMethod]
        public async Task HandleFavoriteArticleAction(FavoriteArticleAction action, IDispatcher dispatcher)
        {
            var articleReponse = await Http.FavoriteArticleAsync(action.Slug);
            if (articleReponse.IsSuccess)
            {
                dispatcher.Dispatch(new FavoriteArticleResultAction(articleReponse.SuccessData.Article));
            }
        }

        [EffectMethod]
        public async Task HandleUnfavoriteArticleAction(UnfavoriteArticleAction action, IDispatcher dispatcher)
        {
            var articleReponse = await Http.UnfavoriteArticleAsync(action.Slug);
            if (articleReponse.IsSuccess)
            {
                dispatcher.Dispatch(new UnfavoriteArticleResultAction(articleReponse.SuccessData.Article));
            }
        }
    }
}
