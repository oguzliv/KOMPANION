using Fitness.Application.Abstractions.Request;
using Fitness.Domain.Enums;

namespace Fitness.Application.Models.MovementModels.MovementRequests
{
    public class MovementDto : Request
    {
        public string Name { get; set; } = null!;
        public MuscleGroup MuscleGroup { get; set; }
    }
}