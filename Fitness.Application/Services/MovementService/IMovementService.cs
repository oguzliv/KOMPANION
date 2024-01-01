using Fitness.Application.Abstractions.Response;
using Fitness.Application.Models.MovementModels.MovementRequests;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services.MovementService
{
    public interface IMovementService
    {
        Task<Response> CreateMovement(CreateMovementRequest movementDto);
        Task<Response> UpdateMovement(UpdateMovementRequest movementUpdateDto);
        Task<bool> DeleteMovement(Guid id);
        Task<Movement> GetMovementByName(string name);
        Task<Movement> GetMovementById(Guid id);
        Task<IEnumerable<Movement>> GetMovements();
    }
}