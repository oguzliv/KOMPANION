using Fitness.Application.Abstractions.Request;
using Fitness.Domain.Enums;

namespace Fitness.Application.Models.WorkoutModels.WorkoutRequests
{
    public class CreateWorkoutRequest : Request
    {
        public string Name { get; set; } = null!;
        public Level Level { get; set; }
        public Duration Duration { get; set; }
        public IList<Guid> Movements { get; set; } = null!;
    }
}