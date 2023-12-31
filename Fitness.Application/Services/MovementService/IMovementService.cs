using Fitness.Application.Abstractions.Response;
using Fitness.Application.Models.MovementModels.MovementRequests;
using Fitness.Domain.Entities;

namespace Fitness.Application.Services.MovementService
{
    public interface IMovementService
    {
        Task<Response> CreateMovement(MovementDto movementDto);
        // Task<object> UpdateUser(Guid id, CustomerUpdateDto user);
        // Task<object> UpdateAdmin(Guid id, UserDto customer);
        // Task<bool> DeleteUser(Guid id);
        // Task<object> GetUserById(Guid id);
        Task<Movement> GetMovementByName(string name);
        // Task<User> GetUserByEmail(string email);
        Task<IEnumerable<Movement>> GetMovements();
    }
}