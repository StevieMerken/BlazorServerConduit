using BlazorServerConduit.Models;
using System.Net.Http.Headers;

namespace BlazorServerConduit.Services
{
    public class AuthenticationHttpMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationHttpMessageHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor?.HttpContext?.User is not null)
            {
                var apiTokenClaim = _httpContextAccessor?.HttpContext?.User.FindFirst(Claims.ApiToken);
                if (apiTokenClaim is not null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Token", apiTokenClaim.Value);
                }
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
