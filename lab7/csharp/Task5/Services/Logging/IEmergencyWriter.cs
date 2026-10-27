namespace Variant7_IoTTelemetryProcessor.Services.Logging
{
    public interface IEmergencyWriter
    {
        void WriteBackupLog(string data);
    }
}