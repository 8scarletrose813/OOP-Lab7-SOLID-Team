using System;
using System.IO;
using System.Text.Json;
using Variant7_FitnessTracker.Models;

namespace Variant7_FitnessTracker.Services.Persistence
{
    public class LocalFileWorkoutRepository : IWorkoutRepository
    {
        public void Save(WorkoutResult result)
        {
            string jsonOutput = JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
            string userStoragePath = $"workout_{result.UserId}_{DateTime.Now:yyyyMMdd_HHmmss}.json";
            File.WriteAllText(userStoragePath, jsonOutput);
        }
    }
}