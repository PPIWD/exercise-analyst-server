using API.Services.Measurements.Dtos.Requests;
namespace API.Services.Measurements.Dtos_ml {
public class MLGyroscope
    {
        public int id { get; set; } = 1;
        public long timestampUtc { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public int measurementId { get; set; } = 1;

        public static explicit operator MLGyroscope(GyroscopeMeasEntityForCreateMeasurement gyro){
            return new MLGyroscope(){
                timestampUtc = gyro.TimestampUtc,
                x = gyro.X,
                y = gyro.Y,
                z = gyro.Z
            };
        }
    }

}