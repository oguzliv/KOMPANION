using Fitness.Application.Abstractions.Response;
using Fitness.Application.Models.MovementModels.MovementRequests;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services.MovementService
{
    public interface IMovementService
    {
        Task<Response> CreateMovement(MovementCreateDto movementDto);
        Task<Response> UpdateMovement(MovementUpdateDto movementUpdateDto);
        Task<bool> DeleteMovement(Guid id);
        Task<Movement> GetMovementByName(string name);
        Task<IEnumerable<Movement>> GetMovements();
    }
}