using Variant7_FitnessTracker.Models;

namespace Variant7_FitnessTracker.Services.Persistence
{
    public interface IWorkoutRepository
    {
        void Save(WorkoutResult result);
    }
}