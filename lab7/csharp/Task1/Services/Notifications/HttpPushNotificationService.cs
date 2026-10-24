using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Variant7_FitnessTracker.Services.Notifications
{
    public class HttpPushNotificationService : INotificationService
    {
        public void SendPushNotification(string deviceToken, double distanceKm, double calories)
        {
            try
            {
                using var httpClient = new HttpClient();
                string pushPayload = JsonSerializer.Serialize(new
                {
                    to = deviceToken,
                    title = "Чудове тренування!",
                    body = $"Ви подолали {distanceKm:F1} км та спалили {calories:F0} ккал!"
                });

                var content = new StringContent(pushPayload, Encoding.UTF8, "application/json");
                httpClient.PostAsync("http://push-gateway.local/send", content).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка надсилання Push-сповіщення: {ex.Message}");
            }
        }
    }
}