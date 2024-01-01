using Fitness.Domain.Enums;
using Fitness.Domain.Abstractions;

namespace Fitness.Domain.Entities
{
    public class Workout : Entity
    {
        public string Name { get; set; } = null!;
        public string Level { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public IList<Movement> Movements { get; set; } = null!;
    }
}