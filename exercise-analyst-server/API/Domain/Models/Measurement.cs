using System.Collections.Generic;

namespace API.Domain.Models
{
    public class Measurement
    {
        public uint Id { get; set; }

        public string Activity { get; set; }
        public int IdFromMobile { get; set; }
        public int Repetitions { get; set; }
        public virtual ICollection<AccelerometerMeasurement> AccelerometerMeasurements{ get; set; }
        public virtual ICollection<GyroscopeMeasurement> GyroscopeMeasurements{ get; set; }
    }
}
