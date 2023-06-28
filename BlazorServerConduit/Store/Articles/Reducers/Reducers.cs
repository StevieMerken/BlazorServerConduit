using BlazorServerConduit.Models;
using BlazorServerConduit.Store.Articles.Actions;
using Fluxor;
using System;

namespace BlazorServerConduit.Store.Articles.Reducers
{
    public static class Reducers
    {
        [ReducerMethod]
        public static ArticleState ReduceFetchArticleAction(ArticleState state, FetchArticleAction action)
        {
            return state with
            {
                IsLoadingArticle = true,
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
                IsLoadingArticle = false,
                SelectedArticle = action.Article,
            };
        }

        [ReducerMethod]
        public static ArticleState ReduceFavoriteArticleResultAction(ArticleState state, FavoriteArticleResultAction action)
        {
            ReplaceArticleInArray(state.Articles, action.Article);

            return state with
            {
                IsLoadingArticle = false,
                SelectedArticle = action.Article,
            };
        }

        [ReducerMethod]
        public static ArticleState ReduceUnfavoriteArticleResultAction(ArticleState state, UnfavoriteArticleResultAction action)
        {
            ReplaceArticleInArray(state.Articles, action.Article);

            return state with
            {
                IsLoadingArticle = false,
                SelectedArticle = action.Article,
            };
        }

        [ReducerMethod]
        public static ArticleState ReduceFetchArticlesAction(ArticleState state, FetchArticlesAction action)
        {
            return state with
            {
                IsLoadingArticles = true,
            };
        }

        [ReducerMethod]
        public static ArticleState ReduceFetchArticlesResultAction(ArticleState state, FetchArticlesResultAction action)
        {
            return state with
            {
                IsLoadingArticles = false,
                Articles = action.MultipleArticlesResponse.Articles.ToArray(),
                ArticlesCount = action.MultipleArticlesResponse.ArticlesCount,
            };
        }

        [ReducerMethod]
        public static ArticleState ReduceFollowAuthorResultAction(ArticleState state, FollowAuthorResultAction action)
        {
            return state = state with
            {
                SelectedArticle = UpdateAuthor(state.SelectedArticle, action.Profile)
            };
        }

        [ReducerMethod]
        public static ArticleState ReduceUnfollowAuthorResultAction(ArticleState state, UnfollowAuthorResultAction action)
        {
            return state = state with
            {
                SelectedArticle = UpdateAuthor(state.SelectedArticle, action.Profile)
            };
        }

        private static void ReplaceArticleInArray(Article[] articles, Article newArticle)
        {
            int indexOfChangedArticle = Array.FindIndex(articles, (a) => a.Slug == newArticle.Slug);

            if (indexOfChangedArticle >= 0)
            {
                articles[indexOfChangedArticle] = newArticle;
            }
        }

        private static Article UpdateAuthor(Article article, Profile newProfile)
        {
            if (article?.Author.UserName == newProfile.UserName)
            {
                return article with
                {
                    Author = newProfile
                };
            }

            return article;
        }
    }
}
