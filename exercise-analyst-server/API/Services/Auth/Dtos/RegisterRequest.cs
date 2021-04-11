using System.ComponentModel.DataAnnotations;
using API.Services.Common;

namespace API.Services.Auth.Dtos
{
    public class RegisterRequest
    {
        [Required, MaxLength(255)]
        public string Password { get; set; }
        
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Range(50, 250)]
        public int HeightInCm { get; set; }
        
        [Range(40, 300)]
        public double WeightInKg { get; set; }
        
        [Range(1, 120)]
        public int Age { get; set; }
        
        [Required]
        public Gender Gender { get; set; }
    }
}