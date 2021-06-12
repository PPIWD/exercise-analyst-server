namespace API.Services.Measurements.Dtos_ml {
public class Gyroscope
    {
        public int id { get; set; } = 1;
        public long timestampUtc { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public int measurementId { get; set; } = 1;
    }

public class Accelerometer
    {
        public int id { get; set; } = 1;
        public long timestampUtc { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public int measurementId { get; set; } = 1;
    }
}