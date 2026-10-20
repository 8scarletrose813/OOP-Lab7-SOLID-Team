using System;

namespace Variant7_IoTTelemetryProcessor.Services.Persistence
{
    public class TimescaleDbTelemetryRepository : ITelemetryRepository
    {
        public void InsertMetric(string deviceId, double temp, double press, long time)
        {
            Console.WriteLine($"[TimescaleDB] Записано метрику для {deviceId}: Temp={temp}°C, Press={press}bar.");
        }
    }
}