using System.ComponentModel.DataAnnotations;

namespace API.Services.Auth.Dtos.Requests
{
    public class LoginRequest
    {
        [Required, MaxLength(255), EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }
    }
}