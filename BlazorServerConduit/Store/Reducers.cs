using Fluxor;

namespace BlazorServerConduit.Store
{
    public static class Reducers
    {
        [ReducerMethod]
        public static ArticleState ReduceFetchArticleAction(ArticleState state, FetchArticleAction action)
        {
            return state with
            {
                IsLoading = true,
            };
        }

        [ReducerMethod]
        public static ArticleState ReduceFavoriteArticleAction(ArticleState state, FavoriteArticleAction action)
        {
            return state;
        }

        [ReducerMethod]
        public static ArticleState ReduceUnfavoriteArticleAction(ArticleState state, UnfavoriteArticleAction action)
        {
            return state;
        }

        [ReducerMethod]
        public static ArticleState ReduceFetchArticleResultAction(ArticleState state, FetchArticleResultAction action)
        {
            return state with
            {
                IsLoading = false,
                Article = action.Article,
            };
        }

        [ReducerMethod]
        public static ArticleState ReduceFetchArticleResultAction(ArticleState state, FavoriteArticleResultAction action)
        {
            return state with
            {
                IsLoading = false,
                Article = action.Article,
            };
        }

        [ReducerMethod]
        public static ArticleState ReduceFetchArticleResultAction(ArticleState state, UnfavoriteArticleResultAction action)
        {
            return state with
            {
                IsLoading = false,
                Article = action.Article,
            };
        }
    }
}
