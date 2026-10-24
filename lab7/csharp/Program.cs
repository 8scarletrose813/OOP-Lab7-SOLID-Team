using System;
using Variant7_IoTTelemetryProcessor.Models;
using Variant7_IoTTelemetryProcessor.Services.Persistence;
using Variant7_IoTTelemetryProcessor.Services.Alerts;
using Variant7_IoTTelemetryProcessor.Services.Logging;

namespace Variant7_IoTTelemetryProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Composition Root: ручне зв'язування залежностей (Manual DI без сервіс-локаторів)
            ITelemetryRepository timescaleRepo = new TimescaleDbTelemetryRepository();
            IAlertPublisher kafkaPublisher = new KafkaAlertPublisher();
            IEmergencyWriter emergencyWriter = new LocalDiskEmergencyWriter();

            var processor = new IoTTelemetryProcessor(timescaleRepo, kafkaPublisher, emergencyWriter);

            // Тестовий пакет телеметрії (з перевищенням показників для активації тривоги)
            var packet = new TelemetryPacket
            {
                DeviceId = "Sensor-77",
                Temperature = 90.2, // > 85.0
                Pressure = 8.5,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };

            processor.ProcessTelemetry(packet);
        }
    }
}