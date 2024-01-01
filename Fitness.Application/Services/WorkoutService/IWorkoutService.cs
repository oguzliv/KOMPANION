using Fitness.Application.Abstractions.Response;
using Fitness.Application.Models.WorkoutModels.WorkoutRequests;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services.WorkoutService
{
    public interface IWorkoutService
    {
        Task<Response> CreateWorkout(CreateWorkoutRequest workoutDto);
        Task<Response> UpdateWorkout(UpdateWorkoutRequest workoutUpdateDto);
        Task<bool> DeleteWorkout(Guid id);
        Task<Response> GetFilteredWorkouts(string level, string duration, string muscleGroup);
        Task<Response> GetWorkoutById(Guid id);
        Task<Response> GetWorkouts();
    }
}