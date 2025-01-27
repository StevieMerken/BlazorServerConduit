﻿using System.ComponentModel.DataAnnotations;

namespace BlazorServerConduit.Models
{
    public class LoginFormModel
    {
        [Required(ErrorMessage ="Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
