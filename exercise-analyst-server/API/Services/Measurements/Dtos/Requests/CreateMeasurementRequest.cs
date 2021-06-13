using System.Collections.Generic;

namespace API.Services.Measurements.Dtos.Requests
{
    public class CreateMeasurementRequest
    {
        public ICollection<AccelerometerMeasEntityForCreateMeasurement> AccelerometerMeasEntities { get; set; }
        public ICollection<GyroscopeMeasEntityForCreateMeasurement> GyroscopeMeasEntities { get; set; }
    }


    public class GyroscopeMeasEntityForCreateMeasurement
    {
        public long TimestampUtc { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class AccelerometerMeasEntityForCreateMeasurement
    {
        public long TimestampUtc { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
