using API.Services.Measurements.Dtos.Requests;

namespace API.Services.Measurements.Dtos_ml {
public class MLAccelerometer
    {
        public int id { get; set; } = 1;
        public long timestampUtc { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public int measurementId { get; set; } = 1;

        public static explicit operator MLAccelerometer(AccelerometerMeasEntityForCreateMeasurement acc){
            return new MLAccelerometer(){
                timestampUtc = acc.TimestampUtc,
                x = acc.X,
                y = acc.Y,
                z = acc.Z
            };
        }
    }
}