using Structure;
using System.Data.SqlClient;
using System.Data;
using Dto;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;
using DTO;


namespace DBService
{
    public class UserRegistrationDBService //: IUserLoginService
    {

        private readonly string _connectionString;

        public UserRegistrationDBService(IConfiguration configuration) 
        {
            this._connectionString = configuration.GetConnectionString("UserDB");
            
        }
        public async Task<Guid?> RegisterUser(string name, string countryID, string phoneNumber, string emailAddress, string zipCodeID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // using (SqlCommand command = new SqlCommand("SP__RegisterUser", connection))
                using (SqlCommand command = new SqlCommand("SP__RegisterUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@CountryID", countryID);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@EmailAddress", emailAddress);
                    command.Parameters.AddWithValue("@ZipCodeID", zipCodeID);

                    // Add output parameter for @UserID
                    SqlParameter outputParameter = new SqlParameter("@outputUserID", SqlDbType.UniqueIdentifier);
                    outputParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputParameter);

                    await command.ExecuteNonQueryAsync();

                    // Check the output parameter value
                    if (outputParameter.Value != DBNull.Value)
                    {
                        Guid? userID = (Guid?)outputParameter.Value;
                        return userID; // Success
                    }
                    else
                    {
                        return null; // Failure
                    }
                }
            }
        }


        public async Task<bool> UpdateUser(string userID, string name, string countryID, string phoneNumber, string emailAddress, string zipCodeID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UpdateUserSP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@CountryID", countryID);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@EmailAddress", emailAddress);
                    command.Parameters.AddWithValue("@ZipCodeID", zipCodeID);

                    await command.ExecuteNonQueryAsync();

                    return true; // Success
                }
            }
        }

        public async Task<UserWithUrlsModel> GetUserWithUrls(string userID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetUserWithUrlsSP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        UserWithUrlsModel user = null;

                        while (await reader.ReadAsync())
                        {
                            if (user == null)
                            {
                                user = new UserWithUrlsModel
                                {
                                    UserID = GetValueOrDefault<string>(reader, "userID"),
                                    Name = GetValueOrDefault<string>(reader, "name"),
                                    AdditionalAddress = GetValueOrDefault<string>(reader, "additionalAddress"),
                                    UTLzipcodeID = GetValueOrDefault<string>(reader, "UTLzipcodeID"),
                                    PhoneNumber = GetValueOrDefault<long?>(reader, "phoneNumber"),
                                    UTLcountryID = GetValueOrDefault<string>(reader, "UTLcountryID"),
                                    Urls = new List<GetUrl>()
                                };
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("urlID")))
                            {
                                user.Urls.Add(new GetUrl
                                {
                                    UrlID = GetValueOrDefault<string>(reader, "urlID"),
                                    UTLiTableID = GetValueOrDefault<string>(reader, "UTLiTableID"),
                                    Url = GetValueOrDefault<string>(reader, "url"),
                                    Label = GetValueOrDefault<string>(reader, "label"),
                                    ReferenceID = GetValueOrDefault<string>(reader, "referenceID")
                                });
                            }
                        }

                        return user;
                    }
                }
            }
        }

        private T GetValueOrDefault<T>(SqlDataReader reader, string columnName)
        {
            object value = reader[columnName];

            if (typeof(T) == typeof(double) && value is long)
            {
                // Convert Int64 to Double if the target type is double
                return (T)(object)Convert.ToDouble(value);
            }

            return value == DBNull.Value ? default(T) : (T)value;
        }



        public async Task UpdateUserAdditionalAddress(string userId, string additionalAddress)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP__UpdateUserAdditionalAddress", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@AdditionalAddress", additionalAddress);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public async Task<string> GetUsersAddtionalAddress(string userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP__GetUserAdditionalAddress", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["additionalAddress"].ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }

        }

        public async Task<bool> UpdateUserWithUrls(string userID, UserWithUrl updateUser)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SP__UpdateUserWithUrl", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Name", updateUser.Name);
                    command.Parameters.AddWithValue("@CountryID", updateUser.CountryID);
                    command.Parameters.AddWithValue("@PhoneNumber", updateUser.PhoneNumber);
                    command.Parameters.AddWithValue("@EmailAddress", updateUser.EmailAddress);
                    command.Parameters.AddWithValue("@ZipCodeID", updateUser.ZipCodeID);

                    // Assuming the stored procedure accepts a TVP (Table-Valued Parameter) for URLs
                    // You may need to adjust the structure and logic based on your actual database schema

                    DataTable urlsTable = new DataTable();
                    urlsTable.Columns.Add("url", typeof(string));
                    urlsTable.Columns.Add("label", typeof(string));

                    if (updateUser.UserUrls != null)
                    {
                        foreach (var url in updateUser.UserUrls)
                        {
                            urlsTable.Rows.Add(url.Url, url.label);
                        }
                    }

                    SqlParameter urlsParameter = command.Parameters.AddWithValue("@Urls", urlsTable);
                    urlsParameter.SqlDbType = SqlDbType.Structured;
                    urlsParameter.TypeName = "dbo.UpdateUrlTableType"; // Update with your actual TVP type

                    // Execute the stored procedure
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            int success = reader.GetInt32(reader.GetOrdinal("Success"));
                            return success == 1;
                        }
                    }
                }
            }

            return false; // In case of an error or unexpected result
        }



    }
}
