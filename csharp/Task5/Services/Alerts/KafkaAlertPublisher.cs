using System;

namespace Variant7_IoTTelemetryProcessor.Services.Alerts
{
    public class KafkaAlertPublisher : IAlertPublisher
    {
        public void SendEmergencyAlert(string topic, string payload)
        {
            Console.WriteLine($"[Apache Kafka Broker] Опубліковано тривогу у топік '{topic}': {payload}");
        }
    }
}