using Fitness.Domain.Enums;
using Fitness.Domain.Abstractions;

namespace Fitness.Domain.Entities
{
    public class Movement : Entity
    {
        public string Name { get; set; } = null!;
        public string MuscleGroup { get; set; } = null!;
    }
}