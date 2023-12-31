
using Fitness.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Fitness.Infra.Repositories
{
    public class WorkoutRepository : IRepository<Workout>
    {
        private readonly string _connString;

        public WorkoutRepository(IConfiguration conf)
        {
            _connString = conf.GetConnectionString("DefaultConnection")!;
        }
        public Task<int> Create(Workout entity)
        {
            // there are movement(id,createdBy,createdBy,updatedAt,updatedBy,muscleGroup) and workout(id,level,duration,)  
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
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

        public async Task<Workout> GetByName(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("get_workout_by_name", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_name", name);

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {

                        if (await reader.ReadAsync())
                        {
                            // Assuming User class has appropriate properties matching the table columns
                            return new Workout
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                // MuscleGroup = reader["MuscleGroup"].ToString()!,
                                Level = reader["Level"].ToString()!,
                                Duration = reader["Duration"].ToString()!,
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                CreatedBy = Guid.Parse(reader["CreatedBy"].ToString()!),
                                UpdatedAt = (DateTime)reader["UpdatedAt"],
                                UpdatedBy = Guid.Parse(reader["UpdatedBy"].ToString()!)
                                // Add other properties as needed
                            };
                        }
                        else
                        {
                            // Handle the case when the user is not found
                            return null;
                        }
                    }
                }
            }
        }
        public Task<int> Update(Workout entity)
        {
            throw new NotImplementedException();
        }

    }
}