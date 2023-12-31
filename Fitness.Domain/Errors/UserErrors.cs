using Fitness.Domain.Abstractions;

namespace Fitness.Domain.Errors
{
    public record UserErrors : Error
    {
        public UserErrors(string Code, string Name) : base(Code, Name) { }
        public static UserErrors UserAlreadyExists = new("User.AlreadyExist", "User already exists");

    }
}