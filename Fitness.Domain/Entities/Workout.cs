using Fitness.Domain.Enums;
using Fitness.Domain.Abstractions;

namespace Fitness.Domain.Entities
{
    public class Workout : Entity
    {
        public String Name { get; set; } = null!;
        public Level Level { get; set; }
        public Duration Duration { get; set; }
        public IEnumerable<Movement> Movements { get; set; } = null!;
    }
}