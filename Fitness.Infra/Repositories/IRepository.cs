using Fitness.Domain.Abstractions;

namespace Fitness.Infra.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<int> Create(T entity);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> Get();
        Task<int> Update(T entity);
        Task<int> Delete(Guid id);
    }
}