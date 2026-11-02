namespace Variant7_IoTTelemetryProcessor.Services.Alerts
{
    public interface IAlertPublisher
    {
        void SendEmergencyAlert(string topic, string payload);
    }
}