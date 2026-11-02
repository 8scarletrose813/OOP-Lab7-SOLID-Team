using System;

namespace Variant7_FitnessTracker.Models
{
    public class WorkoutResult
    {
        public string UserId { get; set; }
        public double DistanceKm { get; set; }
        public string Pace { get; set; }
        public double Calories { get; set; }
        public string IntensityZone { get; set; }
        public DateTime Timestamp { get; set; }
    }
}