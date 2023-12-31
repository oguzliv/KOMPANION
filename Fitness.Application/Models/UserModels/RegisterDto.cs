using FluentValidation;

namespace Fitness.Application.Models.User
{
    public record RegisterDto(String Email, String Username, String Password);

}