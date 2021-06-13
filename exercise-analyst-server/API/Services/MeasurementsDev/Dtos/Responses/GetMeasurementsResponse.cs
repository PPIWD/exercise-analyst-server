using System.Collections.Generic;

namespace API.Services.MeasurementsDev.Dtos.Responses
{
    public class GetMeasurementsResponse
    {
        public List<MeasurementForGetMeasurementsResponse> Measurements { get; set; }
    }

    public class MeasurementForGetMeasurementsResponse
    {
        public int Id { get; set; }
        public string Activity { get; set; }
        public int IdFromMobile { get; set; }
        public int Repetitions { get; set; }
    }
}