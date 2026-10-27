using System;
using Variant7_FitnessTracker.Models;

namespace Variant7_FitnessTracker.Services.Calculations
{
    public class WorkoutCalculator : IWorkoutCalculator
    {
        public WorkoutResult Calculate(WorkoutSessionData session)
        {
            double paceMinutesPerKm = session.DurationMinutes / session.DistanceKm;
            double durationHours = session.DurationMinutes / 60.0;
            
            // Спрощена формула розрахунку калорій для бігу (MET ~ 9.8)
            double caloriesBurned = 9.8 * session.UserWeightKg * durationHours;

            // Pattern Matching для пульсових зон (KISS / C# Clean Syntax)
            string heartRateZone = session.AverageHeartRate switch
            {
                >= 160 => "Анаеробна / Анаеробний поріг",
                >= 130 => "Аеробна / Спалювання жиру",
                _ => "Розминка / Легка активність"
            };

            return new WorkoutResult
            {
                UserId = session.UserId,
                DistanceKm = Math.Round(session.DistanceKm, 2),
                Pace = $"{Math.Floor(paceMinutesPerKm)}:{Math.Round((paceMinutesPerKm - Math.Floor(paceMinutesPerKm)) * 60):00} хв/км",
                Calories = Math.Round(caloriesBurned),
                IntensityZone = heartRateZone,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}