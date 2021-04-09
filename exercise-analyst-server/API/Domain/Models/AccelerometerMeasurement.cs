using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models
{
    public class AccelerometerMeasurement
    {
        public ulong Id { get; set; }

        public int MeasurementIdFromMobile { get; set; }

        public int SessionIdFromMobile { get; set; }

        public long TimestampUtc { get; set; }

        [Column(TypeName = "decimal(12,4)")]
        public double X { get; set; }

        [Column(TypeName = "decimal(12,4)")]
        public double Y { get; set; }

        [Column(TypeName = "decimal(12,4)")]
        public double Z { get; set; }

        public uint MeasurementId { get; set; }
        public virtual Measurement Measurement { get; set; }
    }
}