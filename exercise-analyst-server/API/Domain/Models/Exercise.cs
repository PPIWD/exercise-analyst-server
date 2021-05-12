using System;
using System.ComponentModel.DataAnnotations;

namespace API.Domain.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Activity { get; set; }

        public int Repetitions { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }


        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
