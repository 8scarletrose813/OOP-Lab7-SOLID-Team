namespace Variant7_FitnessTracker.Models
{
    public class WorkoutSessionData
    {
        public string UserId { get; set; }
        public string DeviceToken { get; set; }
        public double DistanceKm { get; set; }
        public int DurationMinutes { get; set; }
        public int AverageHeartRate { get; set; }
        public double UserWeightKg { get; set; }
    }
}