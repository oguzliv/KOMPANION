using FluentValidation;

namespace Fitness.Application.Models.UserModels.UserRequest
{
    public record RegisterDto(String Email, String Username, String Password);

}