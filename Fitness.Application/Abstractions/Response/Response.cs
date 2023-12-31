using Fitness.Domain.Abstractions;

namespace Fitness.Application.Abstractions.Response
{
    public abstract class Response
    {
        public IEnumerable<Error> Errors { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public object Data { get; set; } = null!;
    }
}