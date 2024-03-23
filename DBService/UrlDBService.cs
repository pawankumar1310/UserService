using Dto;
using Microsoft.Extensions.Configuration;
using Structure;
using System.Data.SqlClient;
using System.Data;

namespace DBService
{
    public class UrlDBService : IUrl
    {
        private readonly string _connectionString;
        public UrlDBService(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("UserDB"); ;
        }
        public async Task<int> InsertUrl(UrlModel model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SP__InsertUrl", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UTLiTableID", model.UTLiTableID);
                    command.Parameters.AddWithValue("@url", model.Url);
                    command.Parameters.AddWithValue("@label", model.Label);
                    command.Parameters.AddWithValue("@referenceID", model.ReferenceID);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task<bool> UpdateUrl(string url, UrlModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UpdateUrlSP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure; 
                    command.Parameters.AddWithValue("@Url", url);
                    command.Parameters.AddWithValue("@UTLiTableID", model.UTLiTableID);
                    command.Parameters.AddWithValue("@Label", model.Label);
                    command.Parameters.AddWithValue("@ReferenceID", model.ReferenceID);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }


        public async Task<bool> UpdateUserWithUrl(string userID, string name, string countryID, string phoneNumber, string emailAddress, string zipCodeID, string url, string label)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SP__UpdateUserWithUrl", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@CountryID", countryID);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@EmailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@ZipCodeID", zipCodeID);
                    cmd.Parameters.AddWithValue("@Url", url);
                    cmd.Parameters.AddWithValue("@Label", label);


                    int rowsAffected = await cmd.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }
    }
}
