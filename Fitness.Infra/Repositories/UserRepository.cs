using Fitness.Domain.Abstractions;
using Fitness.Domain.Entites;
using Fitness.Domain.Errors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Fitness.Infra.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly String _connString;

        public UserRepository(IConfiguration conf)
        {
            _connString = conf.GetConnectionString("DefaultConnection")!;
        }
        public async Task<User> Create(User entity)
        {
            int result = 0;
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("create_user", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_username", entity.Username);
                    cmd.Parameters.AddWithValue("@p_email", entity.Email);
                    cmd.Parameters.AddWithValue("@p_createdAt", entity.CreatedAt);
                    cmd.Parameters.AddWithValue("@p_createdBy", entity.CreatedBy);
                    cmd.Parameters.AddWithValue("@p_updatedAt", entity.UpdatedAt);
                    cmd.Parameters.AddWithValue("@p_updatedBy", entity.UpdatedBy);
                    cmd.Parameters.AddWithValue("@p_role", entity.Role);
                    cmd.Parameters.AddWithValue("@p_password", entity.Password);
                    cmd.Parameters.AddWithValue("@p_id", entity.Id);
                    try
                    {

                        result = await cmd.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            return result == 1 ? entity : null;
        }

        public Task<User> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByEmail(String email)
        {
            using (MySqlConnection connection = new MySqlConnection(_connString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("get_user_by_email", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    cmd.Parameters.AddWithValue("@p_email", email);

                    using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        try
                        {
                            if (await reader.ReadAsync())
                            {
                                // Assuming User class has appropriate properties matching the table columns
                                return new User
                                {
                                    Email = reader["Email"].ToString()!,
                                    // Add other properties as needed
                                };
                            }
                            else
                            {
                                // Handle the case when the user is not found
                                return null;
                            }

                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }

        }
        public Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
