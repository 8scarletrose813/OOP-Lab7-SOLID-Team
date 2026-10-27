using System;
using Variant7_FitnessTracker.Models;

namespace Variant7_FitnessTracker.Services.Validation
{
    public class WorkoutValidator : IWorkoutValidator
    {
        public void Validate(WorkoutSessionData session)
        {
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
                throw new ArgumentOutOfRangeException(nameof(session.AverageHeartRate), "Пульс виходить за межі безпечних фізіологічних показників.");
            }
        }
    }
}