using Variant7_FitnessTracker.Models;

namespace Variant7_FitnessTracker.Services.Validation
{
    public interface IWorkoutValidator
    {
        void Validate(WorkoutSessionData session);
    }
}