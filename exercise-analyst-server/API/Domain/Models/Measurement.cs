using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Domain.Models
{
    public class Measurement
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Activity { get; set; }

        public int IdFromMobile { get; set; }
        public int Repetitions { get; set; }
        public virtual ICollection<AccelerometerMeasurement> AccelerometerMeasurements{ get; set; }
        public virtual ICollection<GyroscopeMeasurement> GyroscopeMeasurements{ get; set; }
    }
}
