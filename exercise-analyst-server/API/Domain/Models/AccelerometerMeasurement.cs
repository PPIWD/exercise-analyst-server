using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models
{
    public class AccelerometerMeasurement
    {
        public long Id { get; set; }

        public long TimestampUtc { get; set; }

        [Column(TypeName = "decimal(12,4)")]
        public double X { get; set; }

        [Column(TypeName = "decimal(12,4)")]
        public double Y { get; set; }

        [Column(TypeName = "decimal(12,4)")]
        public double Z { get; set; }

        public int MeasurementId { get; set; }
        public virtual Measurement Measurement { get; set; }
    }
}