using BlazorServerConduit.Models;

namespace BlazorServerConduit.Services
{
    public class ArticlesService
    {
        private readonly HttpClient _httpClient;

        public ArticlesService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["BaseUrlApi"]);
        }

        public async Task<MultipleArticlesResponse> GetArticles(int limit, int offset)
        {
            return await _httpClient.GetFromJsonAsync<MultipleArticlesResponse>($"articles?limit={limit}&offset={offset}");
        }
    }
}
