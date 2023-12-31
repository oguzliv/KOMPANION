using Fitness.Domain.Abstractions;

namespace Fitness.Domain.Errors
{
    public record MovementError : Error
    {
        public MovementError(string Code, string Name) : base(Code, Name) { }
        public static MovementError MovementAlreadyExists = new("Movement.AlreadyExist", "Movement already exists");
        public static MovementError MovementNotExists = new("Movement.NotExist", "Movement not exists");
        // public static MovementError InvalidPassword = new("User.InvalidPassword", "Invalid password");
    }
}