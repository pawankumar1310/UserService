using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dto;
using DTO;
using Structure;

namespace DBService
{
    public class InstitutionRegistration : IInstitute
    {
        private readonly string _connectionString;
        public InstitutionRegistration(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("UserDB"); ;
        }
        public async Task<int> InsertInstitution(AddInstituteModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SP__AddInstitution", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@ZipcodeID", model.ZipcodeID);
                    command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                    command.Parameters.AddWithValue("@url", model.url);
                    command.Parameters.AddWithValue("@institutionFacility", string.Join(",", model.institutionFacilities));
                    command.Parameters.AddWithValue("@institutionGovernance", string.Join(",", model.institutionGovernance));

                    SqlParameter outputParameter = new SqlParameter
                    {
                        ParameterName = "@OutputInstitutionID",
                        SqlDbType = SqlDbType.UniqueIdentifier,
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Execute the stored procedure
                    await command.ExecuteNonQueryAsync();

                    // Check the output parameter value
                    if (outputParameter.Value != DBNull.Value)
                    {
                        Guid outputInstitutionID = (Guid)outputParameter.Value;
                        OutputModel.InstitutionID = outputInstitutionID;
                        return 1; // Success
                    }
                    else
                    {
                        return 0; // Failure
                    }
                }
            }
        }


        public async Task<List<GetInstitution>> GetAllInstitutions()
        {
            List<GetInstitution> institutions = new List<GetInstitution>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SP__GetAllInstitutions", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            GetInstitution institution = MapInstitutionFromReader(reader);
                            institutions.Add(institution);
                        }
                    }
                }
            }

            return institutions;
        }

        public async Task<GetInstitution> GetInstitutionById(string institutionId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SP__GetInstitutionByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InstitutionID", institutionId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapInstitutionFromReader(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        private GetInstitution MapInstitutionFromReader(SqlDataReader reader)
        {
            return new GetInstitution
            {
                institutionID = GetValueOrDefault<string>(reader, "institutionID"),
                institutionName = GetValueOrDefault<string>(reader, "institutionName"),
                zipCodeID = GetValueOrDefault<string>(reader, "zipCodeId"),
                additionalAddress = GetValueOrDefault<string>(reader, "additionalAddress"),
                url = GetValueOrDefault<string>(reader, "url"),
                facilities = GetFacilities(reader),
                governance = GetGovernance(reader)
            };
        }

        private List<string> GetFacilities(SqlDataReader reader)
        {
            string facility = GetValueOrDefault<string>(reader, "facilities");
            return string.IsNullOrEmpty(facility) ? new List<string>() : facility.Split(',').ToList();
        }

        private List<string> GetGovernance(SqlDataReader reader)
        {
            string governance = GetValueOrDefault<string>(reader, "governance");
            return string.IsNullOrEmpty(governance) ? new List<string>() : governance.Split(',').ToList();
        }

        private T GetValueOrDefault<T>(SqlDataReader reader, string columnName)
        {
            object value = reader[columnName];
            return value == DBNull.Value ? default(T) : (T)value;
        }



        public async Task<int> UpdateInstitution(string institutionID, UpdateInstitutionModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spUpdateInstitution", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InstitutionID", institutionID);
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@ZipcodeID", model.ZipcodeID);
                    command.Parameters.AddWithValue("@institutionFacility", string.Join(",", model.institutionFacility));
                    command.Parameters.AddWithValue("@institutionGovernance", string.Join(",", model.institutionGovernance));
                    command.Parameters.AddWithValue("@url", model.url);
                    command.Parameters.AddWithValue("@UpdatedBy", model.UpdatedBy);
                    return await command.ExecuteNonQueryAsync();
                }
            }

        }


        public async Task<int> DeleteInstitution(string institutionID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spDeleteInstitution", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@InstitutionID", institutionID);

                    // Add an output parameter for the return value
                    SqlParameter isDeletedParam = command.Parameters.Add("@IsDeleted", SqlDbType.Bit);
                    isDeletedParam.Direction = ParameterDirection.Output;

                    await command.ExecuteNonQueryAsync();

                    // Retrieve the output parameter value
                    int result = (bool)isDeletedParam.Value ? 1 : 0;

                    return result;
                }
            }
        }
        public async Task<List<UserIDModel>> GetUserIdByEmailOrPhoneNumber(string emailorPhone)
        {
            List<UserIDModel> users = new List<UserIDModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SP__GetUserID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@contactIdentifier", emailorPhone);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new UserIDModel
                            {
                                UserID = reader["userID"].ToString()
                            });
                        }
                    }
                }
            }
        

            return users;
        }



    }
}