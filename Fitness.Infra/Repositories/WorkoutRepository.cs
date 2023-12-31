
using Fitness.Domain.Entities;

namespace Fitness.Infra.Repositories
{
    public class WorkoutRepository : IRepository<Workout>
    {
        public Task<Workout> Create(Workout entity)
        {
            throw new NotImplementedException();
        }

        public Task<Workout> Delete(Workout entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Workout>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Workout> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Workout> Update(Workout entity)
        {
            throw new NotImplementedException();
        }
    }
}