using BlazorServerConduit.Models;
using System.Net;

namespace BlazorServerConduit.Services
{
    public class ProfileService : BaseService
    {
        public ProfileService(HttpClient httpClient, IConfiguration configuration) : base(httpClient, configuration)
        {            
        }

        public async Task<ApiResponse<ProfileResponse>> FollowProfileAsync(string userName)
        {
            var response = await HttpClient.PostAsync($"profiles/{userName}/follow", new StringContent(""));

            if (response.IsSuccessStatusCode)
            {
                return ApiResponse<ProfileResponse>.FromSucces(await response.Content.ReadFromJsonAsync<ProfileResponse>());
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return ApiResponse<ProfileResponse>.FromError(new GenericErrorModel(new Dictionary<string, List<string>>()));
            }

            return ApiResponse<ProfileResponse>.FromError(await response.Content.ReadFromJsonAsync<GenericErrorModel>());
        }

        public async Task<ApiResponse<ProfileResponse>> UnfollowProfileAsync(string userName)
        {
            var response = await HttpClient.DeleteAsync($"profiles/{userName}/follow");

            if (response.IsSuccessStatusCode)
            {
                return ApiResponse<ProfileResponse>.FromSucces(await response.Content.ReadFromJsonAsync<ProfileResponse>());
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return ApiResponse<ProfileResponse>.FromError(new GenericErrorModel(new Dictionary<string, List<string>>()));
            }

            return ApiResponse<ProfileResponse>.FromError(await response.Content.ReadFromJsonAsync<GenericErrorModel>());
        }
    }
}
