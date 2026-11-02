using Variant7_FitnessTracker.Models;

namespace Variant7_FitnessTracker.Services.Calculations
{
    public interface IWorkoutCalculator
    {
        WorkoutResult Calculate(WorkoutSessionData session);
    }
}