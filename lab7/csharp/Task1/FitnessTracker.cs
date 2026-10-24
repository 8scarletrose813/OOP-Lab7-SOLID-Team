using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace Variant7_FitnessTracker
{
        // Опис предметної області: Мобільний додаток для фітнесу та спортивних тренувань.
        // Завдання коду: Обробка даних забігу/тренування — валідація показань датчиків, розрахунок спалених калорій, середнього темпу та пульсових зон, формування підсумкового звіту активності у форматі JSON, збереження тренування у файл локальної бази та відправка пуш-сповіщення у мобільний профіль користувача.

        public class WorkoutSessionData
        {
            public string UserId { get; set; }
            public string DeviceToken { get; set; }
            public double DistanceKm { get; set; }
            public int DurationMinutes { get; set; }
            public int AverageHeartRate { get; set; }
            public double UserWeightKg { get; set; }
        }

        public class FitnessActivityManager
        {
            public void ProcessWorkoutSession(WorkoutSessionData session)
            {
                // 1. Валідація даних телеметрії тренування
                if (session == null)
                {
                    throw new ArgumentNullException(nameof(session));
                }

                if (session.DistanceKm <= 0 || session.DistanceKm > 300)
                {
                    throw new ArgumentException("Некоректна дистанція забігу.");
                }

                if (session.DurationMinutes <= 0 || session.DurationMinutes > 1440)
                {
                    throw new ArgumentException("Некоректна тривалість тренування.");
                }

                if (session.AverageHeartRate < 40 || session.AverageHeartRate > 230)
                {
                    throw new ArgumentOutOfRangeException("Пульс виходить за межі безпечних фізіологічних показників.");
                }

                // 2. Алгоритми розрахунку темпу, калорій та пульсової зони
                double paceMinutesPerKm = session.DurationMinutes / session.DistanceKm;

                // Спрощена формула розрахунку калорій для бігу (MET ~ 9.8)
                double durationHours = session.DurationMinutes / 60.0;
                double caloriesBurned = 9.8 * session.UserWeightKg * durationHours;

                string heartRateZone;
                if (session.AverageHeartRate >= 160)
                {
                    heartRateZone = "Анаеробна / Анаеробний поріг";
                }
                else if (session.AverageHeartRate >= 130)
                {
                    heartRateZone = "Аеробна / Спалювання жиру";
                }
                else
                {
                    heartRateZone = "Розминка / Легка активність";
                }

                // 3. Формування підсумкового JSON-об'єкта для аналітики
                var activitySummary = new
                {
                    UserId = session.UserId,
                    DistanceKm = Math.Round(session.DistanceKm, 2),
                    Pace = $"{Math.Floor(paceMinutesPerKm)}:{Math.Round((paceMinutesPerKm - Math.Floor(paceMinutesPerKm)) * 60):00} хв/км",
                    Calories = Math.Round(caloriesBurned),
                    IntensityZone = heartRateZone,
                    Timestamp = DateTime.UtcNow
                };

                string jsonOutput = JsonSerializer.Serialize(activitySummary, new JsonSerializerOptions { WriteIndented = true });

                // 4. Збереження у локальне сховище тренувань
                string userStoragePath = $"workout_{session.UserId}_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                File.WriteAllText(userStoragePath, jsonOutput);

                // 5. Відправка Push-сповіщення на смартфон користувача
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        string pushPayload = JsonSerializer.Serialize(new
                        {
                            to = session.DeviceToken,
                            title = "Чудове тренування!",
                            body = $"Ви подолали {session.DistanceKm:F1} км та спалили {caloriesBurned:F0} ккал!"
                        });

                        var content = new StringContent(pushPayload, System.Text.Encoding.UTF8, "application/json");
                        httpClient.PostAsync("http://push-gateway.local/send", content).Wait();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка надсилання Push-сповіщення: {ex.Message}");
                }
            }
        }
    }