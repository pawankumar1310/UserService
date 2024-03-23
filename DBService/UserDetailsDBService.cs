using Structure;
using System.Data.SqlClient;
using System.Data;
using Dto;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;


namespace DBService
{
    public class UserDetailsDBService 
    {

        private readonly string _connectionString;
        public UserDetailsDBService(IConfiguration configuration) 
        {
            this._connectionString = configuration.GetConnectionString("UserDB");
            
        }

        public async Task<UserDetails> GetUserDetails(string userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetUserNameandEmail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                   
                    command.Parameters.AddWithValue("@userID", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new UserDetails
                            {
                                UserName = reader["UserName"].ToString(),
                                UserEmail = reader["UserEmail"].ToString()
                            };
                        }
                        else
                        {
                            return null; // User not found
                        }
                    }
                }
            }
        }


        public string GetCountryIdByPhoneNumber(string phoneNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                 connection.Open();

                using (SqlCommand command = new SqlCommand("SP__GetCountryIdByPhoneNumber", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@phoneNumber", phoneNumber);

                    var result =  command.ExecuteScalar();

                    // Check for DBNull
                    return result == DBNull.Value ? null : result.ToString();
                }
            }
        }
    }
}
