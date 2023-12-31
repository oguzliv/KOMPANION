using Fitness.Domain.Abstractions;

namespace Fitness.Domain.Errors
{
    public record WorkoutError : Error
    {
        public WorkoutError(string Code, string Name) : base(Code, Name) { }
        public static WorkoutError WorkoutAlreadyExists = new("Workout.AlreadyExist", "Workout already exists");
        public static WorkoutError WorkoutNotExists = new("Workout.NotExist", "Workout not exists");
        // public static WorkoutError InvalidPassword = new("User.InvalidPassword", "Invalid password");
    }
}