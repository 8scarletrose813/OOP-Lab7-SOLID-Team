using System;
using Variant7_IoTTelemetryProcessor.Models;
using Variant7_IoTTelemetryProcessor.Services.Persistence;
using Variant7_IoTTelemetryProcessor.Services.Alerts;
using Variant7_IoTTelemetryProcessor.Services.Logging;

//Опис предметної області: Моніторинг промислових IoT-пристроїв та обробка телеметрії.
//Завдання коду: Обробка показників датчиків температури та тиску, збереження часових рядів у TimescaleDB, публікація критичного критичного сповіщення в Apache Kafka та аварійний локальний запис.

namespace Variant7_IoTTelemetryProcessor
{
    public class IoTTelemetryProcessor
    {
        private readonly ITelemetryRepository _timescaleRepository;
        private readonly IAlertPublisher _kafkaPublisher;
        private readonly IEmergencyWriter _emergencyWriter;

        // Впровадження залежностей через конструктор (Constructor Injection)
        public IoTTelemetryProcessor(
            ITelemetryRepository timescaleRepository,
            IAlertPublisher kafkaPublisher,
            IEmergencyWriter emergencyWriter)
        {
            _timescaleRepository = timescaleRepository ?? throw new ArgumentNullException(nameof(timescaleRepository));
            _kafkaPublisher = kafkaPublisher ?? throw new ArgumentNullException(nameof(kafkaPublisher));
            _emergencyWriter = emergencyWriter ?? throw new ArgumentNullException(nameof(emergencyWriter));
        }

        public void ProcessTelemetry(TelemetryPacket packet)
        {
            // Guard Clause для валідації вхідних даних
            if (packet == null || string.IsNullOrEmpty(packet.DeviceId))
            {
                throw new ArgumentException("Некоректний пакет телеметрії.");
            }

            _timescaleRepository.InsertMetric(packet.DeviceId, packet.Temperature, packet.Pressure, packet.Timestamp);
            _emergencyWriter.WriteBackupLog($"DEV:{packet.DeviceId}|T:{packet.Temperature}|P:{packet.Pressure}");

            if (packet.Temperature > 85.0 || packet.Pressure > 10.0)
            {
                _kafkaPublisher.SendEmergencyAlert("iot-critical-alerts", $"Критичний перегрів або тиск на пристрої {packet.DeviceId}!");
            }
        }
    }
}