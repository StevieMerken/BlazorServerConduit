using BlazorServerConduit.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;

namespace BlazorServerConduit.Services
{
    public class ArticlesService : BaseService
    {
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

            return await HttpClient.GetFromJsonAsync<MultipleArticlesResponse>(QueryHelpers.AddQueryString("articles", queryParams));
        }
    }
}
