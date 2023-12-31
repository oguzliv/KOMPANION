using Fitness.Application.Models.User;

namespace Fitness.Application.Services.UserService
{
    public interface IUserService
    {
        Task<object> CreateUser(RegisterDto user);
        // Task<object> UpdateUser(Guid id, CustomerUpdateDto user);
        // Task<object> UpdateAdmin(Guid id, UserDto customer);
        // Task<bool> DeleteUser(Guid id);
        // Task<object> GetUserById(Guid id);
        // Task<object> GetUserByEmail(string email);
        // Task<object> GetUsers();
    }
}