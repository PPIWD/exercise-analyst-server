using System.Collections.Generic;

namespace API.Services.MeasurementsDev.Dtos.Requests
{
    public class CreateMeasurementDevRequest
    {
        public ICollection<AccelerometerMeasEntity> AccelerometerMeasEntities { get; set; }
        public ICollection<GyroscopeMeasEntity> GyroscopeMeasEntities { get; set; }
        public string Activity { get; set; }
        public long Id { get; set; }
        public int Repetitions { get; set; }
    }


    public class GyroscopeMeasEntity
    {
        public long TimestampUtc { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class AccelerometerMeasEntity
    {
        public long TimestampUtc { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

}
