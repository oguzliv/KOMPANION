using Fitness.Application.Abstractions.Response;

namespace Fitness.Application.Models.UserModels.UserResponses
{
    public class LoginResponse : Response
    {
        public string Token { get; set; } = null!;
    }
}