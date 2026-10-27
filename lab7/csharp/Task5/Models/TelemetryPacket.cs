namespace Variant7_IoTTelemetryProcessor.Models
{
    public class TelemetryPacket
    {
        public string DeviceId { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public long Timestamp { get; set; }
    }
}