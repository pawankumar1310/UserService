using Dto;
using System.Data.SqlClient;
using System.Data;
using Structure;
using DTO;

namespace DBService
{
    public class UserInstitute:IUserInstitution
    {
        private readonly string _connectionString;
        public UserInstitute(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("UserDB"); ;
        }
        public async Task<int> InsertUserInstitution(UserInstitutionModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SP__InsertUserInstitution", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userID", model.UserID);
                    command.Parameters.AddWithValue("@institutionID", model.InstitutionID);
                    command.Parameters.AddWithValue("@createdBy", model.CreatedBy);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<List<UserInstituteModel>> GetUserInstitute(string userID)
        {
            List<UserInstituteModel> userInstitutes = new List<UserInstituteModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SP__GetInstitutionsByUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userID", userID);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userInstitutes.Add(new UserInstituteModel
                            {
                                InstitutionID = reader["institutionID"].ToString(),
                                InstitutionName = reader["institutionName"].ToString()
                            });
                        }
                    }
                }
            }

            return userInstitutes;
        }
    }
}
