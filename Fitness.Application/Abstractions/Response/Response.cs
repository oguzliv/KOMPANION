using Fitness.Domain.Abstractions;

namespace Fitness.Application.Abstractions.Response
{
    public abstract class Response
    {
        public IList<Error> Errors { get; set; } = new List<Error>();
        public bool IsSuccess { get; set; }
        public object Data { get; set; } = null!;
    }
}