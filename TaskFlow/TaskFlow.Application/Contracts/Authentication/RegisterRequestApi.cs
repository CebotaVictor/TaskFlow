using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskFlow.Application.Contracts.Authentication
{
    public record RegisterRequestApi
    {
        [Required(ErrorMessage = "Usename is required")]
        public string Username { get; set; } = string.Empty;

        [Required (ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "ConfirmPasswo is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        [JsonInclude]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
