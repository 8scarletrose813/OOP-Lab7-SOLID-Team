namespace Variant7_FitnessTracker.Services.Notifications
{
    public interface INotificationService
    {
        void SendPushNotification(string deviceToken, double distanceKm, double calories);
    }
}