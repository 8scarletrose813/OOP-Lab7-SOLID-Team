using System;

namespace Variant7_IoTTelemetryProcessor.Services.Logging
{
    public class LocalDiskEmergencyWriter : IEmergencyWriter
    {
        public void WriteBackupLog(string data)
        {
            Console.WriteLine($"[Emergency Disk Log] Локальний дублюючий запис телеметрії: {data}");
        }
    }
}