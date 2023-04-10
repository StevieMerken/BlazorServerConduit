using BlazorServerConduit.Areas.Identity.Models;
using BlazorServerConduit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorServerConduit.Areas.Identity.Pages.Shared
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var loginResult = await _userService.AuthenticateAsync(User.Email, User.Password);
            if (!loginResult.IsSuccess)
            {
                foreach (var error in loginResult.Error.Errors)
                {
                    foreach (var errorMessage in error.Value) 
                    {
                        if(error.Key.Contains(" "))
                        {
                            ModelState.AddModelError("", $"{error.Key} {errorMessage}");
                        }
                        else
                        {
                            ModelState.AddModelError(error.Key, errorMessage);
                        }
                    }
                }

                return Page();
            }

            return Redirect("~/");
        }
    }
}
