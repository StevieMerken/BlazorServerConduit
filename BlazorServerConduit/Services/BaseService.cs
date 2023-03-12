namespace BlazorServerConduit.Services
{
    public class BaseService
    {
        protected readonly HttpClient HttpClient;

        public BaseService(HttpClient httpClient, IConfiguration configuration)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri(configuration["BaseUrlApi"]);
        }
    }
}
