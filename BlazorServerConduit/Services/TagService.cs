using BlazorServerConduit.Models;

namespace BlazorServerConduit.Services
{
    public class TagService : BaseService
    {
        public TagService(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {
        }

        public async Task<TagResponse> GetTagsAsync()
        {
            return await HttpClient.GetFromJsonAsync<TagResponse>($"tags");
        }
    }
}
