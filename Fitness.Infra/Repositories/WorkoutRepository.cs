
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
        public async Task<int> Create(Workout entity)
        {
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("create_workout", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_name", entity.Name);
                    cmd.Parameters.AddWithValue("@p_createdAt", entity.CreatedAt);
                    cmd.Parameters.AddWithValue("@p_createdBy", entity.CreatedBy);
                    cmd.Parameters.AddWithValue("@p_updatedAt", entity.UpdatedAt);
                    cmd.Parameters.AddWithValue("@p_updatedBy", entity.UpdatedBy);
                    cmd.Parameters.AddWithValue("@p_movementIds", entity.Movements);
                    cmd.Parameters.AddWithValue("@p_level", entity.Level.ToString());
                    cmd.Parameters.AddWithValue("@p_duration", entity.Duration.ToString());
                    cmd.Parameters.AddWithValue("@p_id", entity.Id);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> Delete(Guid id)
        {
            using (MySqlConnection mysql = new MySqlConnection(_connString))
            {
                await mysql.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("delete_workout", mysql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id", id);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IList<Workout>> Get()
        {
            List<Workout> Workouts = new List<Workout>();
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("get_workouts", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            // Assuming User class has appropriate properties matching the table columns
                            Workouts.Add(new Workout
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                Duration = reader["Duration"].ToString()!,
                                Level = reader["Level"].ToString()!,
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                CreatedBy = Guid.Parse(reader["CreatedBy"].ToString()!),
                                UpdatedAt = (DateTime)reader["UpdatedAt"],
                                UpdatedBy = Guid.Parse(reader["UpdatedBy"].ToString()!)
                                // Add other properties as needed
                            });
                        }
                    }
                }
                return Workouts;
            }
        }

        public async Task<IList<Workout>> GeFiltered(string duration, string level, string muscleGroup)
        {
            List<Workout> Workouts = new List<Workout>();
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("get_workouts", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_level", duration);
                    cmd.Parameters.AddWithValue("@p_duration", level);
                    cmd.Parameters.AddWithValue("@p_muscleGroup", muscleGroup);

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            // Assuming User class has appropriate properties matching the table columns
                            Workouts.Add(new Workout
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                Duration = reader["Duration"].ToString()!,
                                Level = reader["Level"].ToString()!,
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                CreatedBy = Guid.Parse(reader["CreatedBy"].ToString()!),
                                UpdatedAt = (DateTime)reader["UpdatedAt"],
                                UpdatedBy = Guid.Parse(reader["UpdatedBy"].ToString()!)
                                // Add other properties as needed
                            });
                        }
                    }
                }
                return Workouts;
            }
        }
        public async Task<Workout> GetById(Guid id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("get_workout_by_id", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_id", id);

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Assuming User class has appropriate properties matching the table columns
                            return new Workout
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                Duration = reader["Duration"].ToString()!,
                                Level = reader["Level"].ToString()!,
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

        public async Task<IList<Movement>> GetWorkoutMovements(Guid id)
        {
            List<Movement> WorkoutMovements = new List<Movement>();
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("get_workout_movements", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_id", id);

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            // Assuming User class has appropriate properties matching the table columns
                            WorkoutMovements.Add(new Movement
                            {
                                Id = Guid.Parse(reader["Id"].ToString()!),
                                Name = reader["Name"].ToString()!,
                                MuscleGroup = reader["MuscleGroup"].ToString()!,
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                CreatedBy = Guid.Parse(reader["CreatedBy"].ToString()!),
                                UpdatedAt = (DateTime)reader["UpdatedAt"],
                                UpdatedBy = Guid.Parse(reader["UpdatedBy"].ToString()!)
                                // Add other properties as needed
                            });
                        }
                    }
                }
                return WorkoutMovements;
            }
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
        public async Task<int> Update(Workout entity)
        {
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("update_workout", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_name", entity.Name);
                    cmd.Parameters.AddWithValue("@p_updatedAt", entity.UpdatedAt);
                    cmd.Parameters.AddWithValue("@p_updatedBy", entity.UpdatedBy);
                    cmd.Parameters.AddWithValue("@p_movementIds", string.Join(",", entity.Movements.Select(id => id.ToString())));
                    cmd.Parameters.AddWithValue("@p_level", entity.Level.ToString());
                    cmd.Parameters.AddWithValue("@p_duration", entity.Duration.ToString());
                    cmd.Parameters.AddWithValue("@p_id", entity.Id);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }

        }
    }
}