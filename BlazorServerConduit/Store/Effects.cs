using BlazorServerConduit.Models;
using BlazorServerConduit.Services;
using Fluxor;

namespace BlazorServerConduit.Store
{
    public class Effects
    {
        private readonly ArticlesService Http;

        public Effects(ArticlesService http)
        {
            Http = http;
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

    public record FetchArticleAction(string Slug);

    public record FetchArticleResultAction(Article Article);

    public record FavoriteArticleAction(string Slug);

    public record FavoriteArticleResultAction(Article Article);

    public record UnfavoriteArticleAction(string Slug);

    public record UnfavoriteArticleResultAction(Article Article);
}
