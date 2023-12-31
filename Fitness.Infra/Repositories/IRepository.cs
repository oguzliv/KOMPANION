using Fitness.Domain.Abstractions;

namespace Fitness.Infra.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> Create(T entity);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> Get();
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }
}