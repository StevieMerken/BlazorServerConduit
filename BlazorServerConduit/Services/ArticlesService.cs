using BlazorServerConduit.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;

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
            };

            return await HttpClient.GetFromJsonAsync<MultipleArticlesResponse>(QueryHelpers.AddQueryString(feed, queryParams));
        }
    }
}
