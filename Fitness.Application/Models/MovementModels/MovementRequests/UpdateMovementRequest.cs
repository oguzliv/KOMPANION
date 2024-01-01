namespace Fitness.Application.Models.MovementModels.MovementRequests
{
    public class UpdateMovementRequest : CreateMovementRequest
    {
        public Guid Id { get; set; }
    }
}