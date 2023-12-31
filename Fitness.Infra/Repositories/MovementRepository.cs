using Fitness.Domain.Entities;

namespace Fitness.Infra.Repositories
{
    public class MovementRepository : IRepository<Movement>
    {
        public Task<Movement> Create(Movement entity)
        {
            throw new NotImplementedException();
        }

        public Task<Movement> Delete(Movement entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movement>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Movement> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Movement> Update(Movement entity)
        {
            throw new NotImplementedException();
        }
    }
}