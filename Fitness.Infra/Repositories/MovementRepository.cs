using Fitness.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Fitness.Infra.Repositories
{
    public class MovementRepository : IRepository<Movement>
    {
        private readonly string _connString;

        public MovementRepository(IConfiguration conf)
        {
            _connString = conf.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int> Create(Movement entity)
        {
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("create_movement", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_name", entity.Name);
                    cmd.Parameters.AddWithValue("@p_createdAt", entity.CreatedAt);
                    cmd.Parameters.AddWithValue("@p_createdBy", entity.CreatedBy);
                    cmd.Parameters.AddWithValue("@p_updatedAt", entity.UpdatedAt);
                    cmd.Parameters.AddWithValue("@p_updatedBy", entity.UpdatedBy);
                    cmd.Parameters.AddWithValue("@p_muscleGroup", entity.MuscleGroup);
                    cmd.Parameters.AddWithValue("@p_id", entity.Id);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public Task<Movement> Delete(Movement entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movement>> Get()
        {
            var Movements = new List<Movement>();
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("get_all_movements", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {

                        if (await reader.ReadAsync())
                        {
                            // Assuming User class has appropriate properties matching the table columns
                            Movements.Add(new Movement
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                MuscleGroup = reader["MuscleGroup"].ToString()!,
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                CreatedBy = Guid.Parse(reader["CreatedBy"].ToString()!),
                                UpdatedAt = (DateTime)reader["UpdatedAt"],
                                UpdatedBy = Guid.Parse(reader["UpdatedBy"].ToString()!)
                            });
                        }
                        else
                        {
                            // Handle the case when the user is not found
                            return null;
                        }
                    }
                }
            }
            return Movements;
        }

        public async Task<Movement> GetById(Guid id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("get_movement_by_id", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_id", id);

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {

                        if (await reader.ReadAsync())
                        {
                            // Assuming User class has appropriate properties matching the table columns
                            return new Movement
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                MuscleGroup = reader["MuscleGroup"].ToString()!,
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

        public async Task<Movement> GetByName(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("get_movement_by_name", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_name", name);

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {

                        if (await reader.ReadAsync())
                        {
                            // Assuming User class has appropriate properties matching the table columns
                            return new Movement
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                MuscleGroup = reader["MuscleGroup"].ToString()!,
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

        public async Task<int> Update(Movement entity)
        {
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("update_movement", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_name", entity.Name);
                    cmd.Parameters.AddWithValue("@p_createdAt", entity.CreatedAt);
                    cmd.Parameters.AddWithValue("@p_createdBy", entity.CreatedBy);
                    cmd.Parameters.AddWithValue("@p_updatedAt", entity.UpdatedAt);
                    cmd.Parameters.AddWithValue("@p_updatedBy", entity.UpdatedBy);
                    cmd.Parameters.AddWithValue("@p_muscleGroup", entity.MuscleGroup);
                    cmd.Parameters.AddWithValue("@p_id", entity.Id);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}