namespace Fitness.Application.Models.WorkoutModels.WorkoutRequests
{
    public class UpdateWorkoutRequest : CreateWorkoutRequest
    {
        public Guid Id { get; set; }
    }
}