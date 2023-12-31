using Fitness.Application.Abstractions.Request;

namespace Fitness.Application.Models.UserModels.UserRequest
{
    public class LoginDto : Request
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}