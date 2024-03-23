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
    public class UserLoginDBService //: IUserLoginService
    {

        private readonly string _connectionString;

        public UserLoginDBService(IConfiguration configuration) 
        {
            this._connectionString = configuration.GetConnectionString("UserDB");
            
        }


// ----------------------------------  CHECK WHETHER EMAIL EXIST OR NOT  ---------------------------------

        public async Task<bool> CheckEmailOrPhoneExistenceAsync(string emailOrPhoneorUserName)
        {
            using var connection = new SqlConnection(_connectionString);

            connection.Open();
            using var command = new SqlCommand("SP_CheckEmailOrPhoneOrUserNameExist", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@EmailOrPhone", emailOrPhoneorUserName);

            var existsParameter = command.Parameters.Add("@Exists", SqlDbType.Bit);
            existsParameter.Direction = ParameterDirection.Output;

            await command.ExecuteNonQueryAsync();
            connection.Close();

            return (bool)existsParameter.Value;
        }



        public async Task<UserStatus> CheckUserExistence(string userInput)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("CheckUserExistence", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserInput", userInput);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var loginStatus = new UserStatus
                            {
                                UserNAmeStatus = Convert.ToBoolean(reader["IsUsernameExists"]) || Convert.ToBoolean(reader["IsEmailExists"]) || Convert.ToBoolean(reader["IsPhoneNumberExists"]),
                                IsOtp = Convert.ToBoolean(reader["IsEmailExists"]) || Convert.ToBoolean(reader["IsPhoneNumberExists"]),
                                IsOtpAvailable = Convert.ToBoolean(reader["IsEmailExists"]) || Convert.ToBoolean(reader["IsPhoneNumberExists"]),
                                IsPasswordAvailable =   Convert.ToBoolean(reader["IsUsernameExists"]) && Convert.ToBoolean(reader["IsPasswordExists"])
                            };

                            return loginStatus;
                        }
                    }
                }
            }

            throw new InvalidOperationException("User not found");
        }



        public async Task<int> ValidatePassword(string userInput, string enteredPassword)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("ValidatePassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserInput", userInput);
                    command.Parameters.AddWithValue("@EnteredPassword", enteredPassword);

                    // Assuming the stored procedure returns an integer (0 or 1)
                    var result = await command.ExecuteScalarAsync();

                    // Convert the result to an integer
                    return result != DBNull.Value ? (int)result : 0;
                }
            }
        }


        public async Task<UserContact> GetUserContactByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetUserContactByUsernameSP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            UserContact userContact = new UserContact
                            {
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                            };

                            return userContact;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
//--------------------------- get UserID from UserName --------------------------------------

        public async Task<string> GetUserIdByEmail(string emailAddress)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP__GetUserByEmailAddress", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmailAddress", emailAddress);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["UserID"].ToString();
                        }
                        else
                        {
                            return null; // Email not found
                        }
                    }
                }
            }
        }


    }
}





        

     
    