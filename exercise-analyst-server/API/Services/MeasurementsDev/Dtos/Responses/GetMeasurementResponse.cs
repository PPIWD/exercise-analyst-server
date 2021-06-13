using System.Collections.Generic;

namespace API.Services.MeasurementsDev.Dtos.Responses
{
    public class GetMeasurementResponse
    {
        public int Id { get; set; }
        public string Activity { get; set; }
        public int IdFromMobile { get; set; }
        public int Repetitions { get; set; }
        public List<AccelerometerMeasurementForGetMeasurementsResponse> AccelerometerMeasurements { get; set; }
        public List<GyroscopeMeasurementForGetMeasurementsResponse> GyroscopeMeasurements { get; set; }
    }
  
    public class GyroscopeMeasurementForGetMeasurementsResponse
    {
        public long Id { get; set; }
        public long TimestampUtc { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int MeasurementId { get; set; }
    }

    public class AccelerometerMeasurementForGetMeasurementsResponse
    {
        public long Id { get; set; }
        public long TimestampUtc { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int MeasurementId { get; set; }
    }
}