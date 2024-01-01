using Fitness.Application.Abstractions.Response;

namespace Fitness.Application.Models.WorkoutModels.WorkoutResponses
{
    public class GetWorkoutResponse : Response
    {
        public List<string> MuscleGroups { get; set; } = null!;
    }
}