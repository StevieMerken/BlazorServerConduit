using BlazorServerConduit.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;
using System.Net;

namespace BlazorServerConduit.Services
{
    public class ArticlesService : BaseService
    {
        private string _globalFeedUrl = "articles";
        private string _personalFeedUrl = "articles/feed";

        public ArticlesService(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {
        }

        public async Task<MultipleArticlesResponse> GetArticlesAsync(ArticleFilter articleFilter)
        {
            var queryParams = new Dictionary<string, string>()
            {
                {"limit", articleFilter.Limit.ToString(CultureInfo.InvariantCulture) },
                {"offset", articleFilter.Offset.ToString(CultureInfo.InvariantCulture) },
            };

            if (!string.IsNullOrEmpty(articleFilter.Tag))
            {
                queryParams.Add("tag", articleFilter.Tag);
            }

            string feed = articleFilter.FeedType switch
            {
                FeedType.Global => _globalFeedUrl,
                FeedType.Personal => _personalFeedUrl,
                FeedType.Tag => _globalFeedUrl,
                _ => throw new NotImplementedException(),
            };

            return await HttpClient.GetFromJsonAsync<MultipleArticlesResponse>(QueryHelpers.AddQueryString(feed, queryParams));
        }

        public async Task<ApiResponse<SingleArticleResponse>> FavoriteArticleAsync(Article article)
        {
            var response = await HttpClient.PostAsync($"articles/{article.Slug}/favorite", new StringContent(""));

            if(response.IsSuccessStatusCode)
            {
                return ApiResponse<SingleArticleResponse>.FromSucces(await response.Content.ReadFromJsonAsync<SingleArticleResponse>());
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return ApiResponse<SingleArticleResponse>.FromError(new GenericErrorModel(new Dictionary<string, List<string>>()));
            }

            return ApiResponse<SingleArticleResponse>.FromError(await response.Content.ReadFromJsonAsync<GenericErrorModel>());
        }

        public async Task<ApiResponse<SingleArticleResponse>> UnfavoriteArticleAsync(Article article)
        {
            var response = await HttpClient.DeleteAsync($"articles/{article.Slug}/favorite");

            if (response.IsSuccessStatusCode)
            {
                return ApiResponse<SingleArticleResponse>.FromSucces(await response.Content.ReadFromJsonAsync<SingleArticleResponse>());
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return ApiResponse<SingleArticleResponse>.FromError(new GenericErrorModel(new Dictionary<string, List<string>>()));
            }

            return ApiResponse<SingleArticleResponse>.FromError(await response.Content.ReadFromJsonAsync<GenericErrorModel>());
        }
    }
}
