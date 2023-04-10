using System.ComponentModel.DataAnnotations;

namespace BlazorServerConduit.Areas.Identity.Models
{
    public class User
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
