using Fitness.Domain.Enums;
using Fitness.Domain.Abstractions;

namespace Fitness.Domain.Entities
{
    public class Movement : Entity
    {
        public String Name { get; set; } = null!;
        public MuscleGroup MuscleGroup { get; set; }
        public IEnumerable<Workout> Workouts { get; set; } = null!;
    }
}