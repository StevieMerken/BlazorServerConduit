using BlazorServerConduit.Services;
using BlazorServerConduit.Store.Articles.Actions;
using Fluxor;

namespace BlazorServerConduit.Store.Articles.Effects
{
    public class Effects
    {
        private readonly ArticlesService _articleService;
        private readonly ProfileService _profileService;

        public Effects(ArticlesService articleService, ProfileService profileService)
        {
            _articleService = articleService;
            _profileService = profileService;
        }

        [EffectMethod]
        public async Task HandleFetchArticlesAction(FetchArticlesAction action, IDispatcher dispatcher)
        {
            var articles = await _articleService.GetArticlesAsync(action.Filter);
            if (articles is not null)
            {
                dispatcher.Dispatch(new FetchArticlesResultAction(articles));
            }
        }

        [EffectMethod]
        public async Task HandleFetchArticleAction(FetchArticleAction action, IDispatcher dispatcher)
        {
            var articleReponse = await _articleService.GetArticleAsync(action.Slug);
            if (articleReponse.IsSuccess)
            {
                dispatcher.Dispatch(new FetchArticleResultAction(articleReponse.SuccessData.Article));
            }
        }

        [EffectMethod]
        public async Task HandleFavoriteArticleAction(FavoriteArticleAction action, IDispatcher dispatcher)
        {
            var articleReponse = await _articleService.FavoriteArticleAsync(action.Slug);
            if (articleReponse.IsSuccess)
            {
                dispatcher.Dispatch(new FavoriteArticleResultAction(articleReponse.SuccessData.Article));
            }
        }

        [EffectMethod]
        public async Task HandleUnfavoriteArticleAction(UnfavoriteArticleAction action, IDispatcher dispatcher)
        {
            var articleReponse = await _articleService.UnfavoriteArticleAsync(action.Slug);
            if (articleReponse.IsSuccess)
            {
                dispatcher.Dispatch(new UnfavoriteArticleResultAction(articleReponse.SuccessData.Article));
            }
        }

        [EffectMethod]
        public async Task HandleFollowAuthorAction(FollowAuthorAction action, IDispatcher dispatcher)
        {
            var followResponse = await _profileService.FollowProfileAsync(action.UserName);
            if(followResponse.IsSuccess)
            {
                dispatcher.Dispatch(new FollowAuthorResultAction(followResponse.SuccessData.Profile));
            }
        }

        [EffectMethod]
        public async Task HandleUnfollowAuthorAction(UnfollowAuthorAction action, IDispatcher dispatcher)
        {
            var unfollowResponse = await _profileService.UnfollowProfileAsync(action.UserName);
            if (unfollowResponse.IsSuccess)
            {
                dispatcher.Dispatch(new UnfollowAuthorResultAction(unfollowResponse.SuccessData.Profile));
            }
        }
    }
}
