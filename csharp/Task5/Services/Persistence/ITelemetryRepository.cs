namespace Variant7_IoTTelemetryProcessor.Services.Persistence
{
    public interface ITelemetryRepository
    {
        void InsertMetric(string deviceId, double temp, double press, long time);
    }
}