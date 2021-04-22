using System.Collections.Generic;

namespace API.Services.MeasurementsDev.Dtos
{
    public class CreateMeasurementDevRequest
    {
        public ICollection<AccelerometerMeasEntity> AccelerometerMeasEntities { get; set; }
        public ICollection<GyroscopeMeasEntity> GyroscopeMeasEntities { get; set; }
        public SessionEntity SessionEntity { get; set; }
    }

   
}
