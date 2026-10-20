using System;
using System.Collections.Generic;

// ---- Завдання 1 (SRP): Variant7_FitnessTracker ----
using Variant7_FitnessTracker.Managers;
using Variant7_FitnessTracker.Models;
using Variant7_FitnessTracker.Services.Calculations;
using Variant7_FitnessTracker.Services.Notifications;
using Variant7_FitnessTracker.Services.Persistence;
using Variant7_FitnessTracker.Services.Validation;

// ---- Завдання 5 (DIP): Variant7_IoTTelemetryProcessor ----
using Variant7_IoTTelemetryProcessor;
using Variant7_IoTTelemetryProcessor.Models;
using Variant7_IoTTelemetryProcessor.Services.Alerts;
using Variant7_IoTTelemetryProcessor.Services.Logging;
using Variant7_IoTTelemetryProcessor.Services.Persistence;

namespace Variant7_Lab7
{
    /// <summary>
    /// Composition Root для ЛР №7. Єдине місце, де створюються конкретні реалізації (new)
    /// та зв'язуються з класами бізнес-логіки. Service Locator не використовується.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            RunTask1_Srp();
            Console.WriteLine();
            RunTask5_Dip();
        }

        // =====================================================================
        // ЗАВДАННЯ 1. SRP: God Object розбито на валідатор, калькулятор,
        // репозиторій, сервіс сповіщень та клас-оркестратор.
        // =====================================================================
        static void RunTask1_Srp()
        {
            Console.WriteLine("=== ЗАВДАННЯ 1: SRP (FitnessActivityManager) ===");

            // Manual DI: кожну відповідальність передаємо окремою залежністю
            IWorkoutValidator validator = new WorkoutValidator();
            IWorkoutCalculator calculator = new WorkoutCalculator();
            IWorkoutRepository repository = new LocalFileWorkoutRepository();
            INotificationService notifications = new HttpPushNotificationService();

            var manager = new FitnessActivityManager(validator, calculator, repository, notifications);

            // 1) Коректне тренування: валідація -> розрахунок -> збереження -> push
            var validSession = new WorkoutSessionData
            {
                UserId = "user-42",
                DeviceToken = "device-token-abc123",
                DistanceKm = 10.5,
                DurationMinutes = 55,
                AverageHeartRate = 148,
                UserWeightKg = 72.0
            };

            Console.WriteLine("-> Обробка коректного тренування...");
            manager.ProcessWorkoutSession(validSession);
            Console.WriteLine("   Тренування оброблено (JSON-файл збережено у робочій теці).");

            // 2) Некоректний пульс: валідатор має зупинити обробку ще до розрахунків
            var invalidSession = new WorkoutSessionData
            {
                UserId = "user-42",
                DeviceToken = "device-token-abc123",
                DistanceKm = 5.0,
                DurationMinutes = 30,
                AverageHeartRate = 300, // поза межами 40..230
                UserWeightKg = 72.0
            };

            Console.WriteLine("-> Обробка тренування з некоректним пульсом...");
            try
            {
                manager.ProcessWorkoutSession(invalidSession);
            }
            catch (ArgumentException ex) // ArgumentOutOfRangeException успадковує ArgumentException
            {
                Console.WriteLine($"   Fail-fast спрацював: {ex.Message}");
            }

            // 3) null-сесія: перевірка Guard Clause у валідаторі
            Console.WriteLine("-> Обробка null-сесії...");
            try
            {
                manager.ProcessWorkoutSession(null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"   Fail-fast спрацював: параметр '{ex.ParamName}' = null.");
            }
        }

        // =====================================================================
        // ЗАВДАННЯ 5. DIP: IoTTelemetryProcessor залежить від абстракцій.
        // Демонструємо підміну реалізацій: Production vs Mock.
        // =====================================================================
        static void RunTask5_Dip()
        {
            Console.WriteLine("=== ЗАВДАННЯ 5: DIP (IoTTelemetryProcessor) ===");

            // ---------- Сценарій A: «бойові» реалізації ----------
            Console.WriteLine("--- Сценарій A: Production-реалізації ---");

            ITelemetryRepository timescaleRepo = new TimescaleDbTelemetryRepository();
            IAlertPublisher kafkaPublisher = new KafkaAlertPublisher();
            IEmergencyWriter emergencyWriter = new LocalDiskEmergencyWriter();

            var productionProcessor = new IoTTelemetryProcessor(timescaleRepo, kafkaPublisher, emergencyWriter);

            productionProcessor.ProcessTelemetry(new TelemetryPacket
            {
                DeviceId = "Sensor-77",
                Temperature = 90.2, // > 85.0 -> тривога
                Pressure = 8.5,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });

            // ---------- Сценарій B: Mock-реалізації (той самий клас, інші залежності) ----------
            Console.WriteLine();
            Console.WriteLine("--- Сценарій B: Mock-реалізації (без зміни IoTTelemetryProcessor) ---");

            var mockRepo = new InMemoryTelemetryRepository();
            var mockPublisher = new RecordingAlertPublisher();
            var mockWriter = new SilentEmergencyWriter();

            var testProcessor = new IoTTelemetryProcessor(mockRepo, mockPublisher, mockWriter);

            // Нормальний пакет -> тривоги бути не повинно
            testProcessor.ProcessTelemetry(new TelemetryPacket
            {
                DeviceId = "Sensor-01",
                Temperature = 60.0,
                Pressure = 5.0,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });
            PrintCheck("Нормальний пакет: тривог немає", mockPublisher.Alerts.Count == 0);

            // Критичний тиск -> рівно одна тривога
            testProcessor.ProcessTelemetry(new TelemetryPacket
            {
                DeviceId = "Sensor-02",
                Temperature = 70.0,
                Pressure = 12.3, // > 10.0
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            });
            PrintCheck("Критичний тиск: опубліковано 1 тривогу", mockPublisher.Alerts.Count == 1);
            PrintCheck("Обидва пакети збережено в репозиторії", mockRepo.Records.Count == 2);
            PrintCheck("Топік тривоги = 'iot-critical-alerts'", mockPublisher.Alerts[0].Topic == "iot-critical-alerts");

            // ---------- Guard Clauses ----------
            Console.WriteLine();
            Console.WriteLine("--- Перевірка Guard Clauses ---");
            try
            {
                testProcessor.ProcessTelemetry(null);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"   Некоректний пакет відхилено: {ex.Message}");
            }

            try
            {
                var broken = new IoTTelemetryProcessor(null, mockPublisher, mockWriter);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"   Конструктор відхилив null-залежність: '{ex.ParamName}'.");
            }
        }

        static void PrintCheck(string description, bool passed)
        {
            Console.WriteLine($"   [{(passed ? "OK" : "FAIL")}] {description}");
        }
    }

    // =========================================================================
    // Тестові (Mock) реалізації абстракцій із Завдання 5.
    // За бажанням перенесіть у окремі файли (наприклад, Task5/Services/Mocks/).
    // =========================================================================

    /// <summary>Заміна TimescaleDbTelemetryRepository: тримає записи в пам'яті.</summary>
    internal sealed class InMemoryTelemetryRepository : ITelemetryRepository
    {
        public List<string> Records { get; } = new List<string>();

        public void InsertMetric(string deviceId, double temp, double press, long time)
        {
            Records.Add($"{deviceId}|{temp}|{press}|{time}");
        }
    }

    /// <summary>Заміна KafkaAlertPublisher: запам'ятовує надіслані тривоги.</summary>
    internal sealed class RecordingAlertPublisher : IAlertPublisher
    {
        public List<(string Topic, string Payload)> Alerts { get; } = new List<(string Topic, string Payload)>();

        public void SendEmergencyAlert(string topic, string payload)
        {
            Alerts.Add((topic, payload));
        }
    }

    /// <summary>Заміна LocalDiskEmergencyWriter: нічого не виводить.</summary>
    internal sealed class SilentEmergencyWriter : IEmergencyWriter
    {
        public void WriteBackupLog(string data)
        {
        }
    }
}
