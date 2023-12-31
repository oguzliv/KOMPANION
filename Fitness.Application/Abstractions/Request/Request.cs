namespace Fitness.Application.Abstractions.Request
{
    public abstract class Request
    {
        public DateTime Time { get; set; } = DateTime.UtcNow;
    }
}