using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Services.Common;
using Microsoft.AspNetCore.Identity;

namespace API.Domain.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Range(50, 250)]
        public int HeightInCm { get; set; }

        [Range(40, 300)]
        public double WeightInKg { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public DateTime CreatedAtUTC { get; set; }


        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}