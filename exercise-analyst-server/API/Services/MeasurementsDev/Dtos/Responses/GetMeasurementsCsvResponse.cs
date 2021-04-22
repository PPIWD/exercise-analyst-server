using System.Collections.Generic;

namespace API.Services.MeasurementsDev.Dtos.Responses
{
    public class GetMeasurementsCsvResponse
    {
        public List<MeasurementForGetMeasurementsCsvResponse> Measurements { get; set; }
    }

    public class MeasurementForGetMeasurementsCsvResponse
    {
        public int Id { get; set; }
        public string Activity { get; set; }
        public int IdFromMobile { get; set; }
        public int Repetitions { get; set; }
        public List<AccelerometerMeasurementForGetMeasurementsCsvResponse> AccelerometerMeasurements { get; set; }
        public List<GyroscopeMeasurementForGetMeasurementsCsvResponse> GyroscopeMeasurements { get; set; }
    }

    public class GyroscopeMeasurementForGetMeasurementsCsvResponse
    {
        public long Id { get; set; }
        public long TimestampUtc { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int MeasurementId { get; set; }
    }

    public class AccelerometerMeasurementForGetMeasurementsCsvResponse
    {
        public long Id { get; set; }
        public long TimestampUtc { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int MeasurementId { get; set; }
    }
}