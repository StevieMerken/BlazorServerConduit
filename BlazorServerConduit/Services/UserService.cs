using BlazorServerConduit.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace BlazorServerConduit.Services
{
    public class UserService : BaseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(HttpClient httpClient, 
                           IConfiguration configuration,
                           IHttpContextAccessor httpContextAccessor) : base(httpClient, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<UserResponse>> AuthenticateAsync(string email, string password)
        {
            var authResult = await HttpClient.PostAsJsonAsync($"users/login", new LoginUserRequest(new LoginUser(email, password)));

            if (authResult.IsSuccessStatusCode)
            {
                var userDetails = await authResult.Content.ReadFromJsonAsync<UserResponse>();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(Claims.UserNameClaim, userDetails.user.Username),
                    new Claim(Claims.ApiToken, userDetails.user.Token),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                                   new ClaimsPrincipal(claimsIdentity),
                                                                   authProperties);


                return new ApiResponse<UserResponse>(userDetails, null);
            }
            else
            {
                return new ApiResponse<UserResponse>(null, await authResult.Content.ReadFromJsonAsync<GenericErrorModel>());
            }
        }
    }
}