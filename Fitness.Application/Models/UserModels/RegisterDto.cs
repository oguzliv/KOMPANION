using FluentValidation;

namespace Fitness.Application.Models.UserModels
{
    public record RegisterDto(String Email, String Username, String Password);

}