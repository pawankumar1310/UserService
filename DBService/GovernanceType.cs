using Structure;
using System.Data.SqlClient;
using System.Data;
using Dto;

namespace DBService
{
    public class GovernanceType:IGovernanceType
    {
        private readonly string _connectionString;
        public GovernanceType(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("UserDB"); ;
        }

        public async  Task<int> InsertGovernanceType(string name, string createdBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spInsertGovernanceType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@CreatedBy", createdBy);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<List<ViewGovernanceTypeModel>> GetGovernanceTypes()
        {
            List<ViewGovernanceTypeModel> governanceTypes = new List<ViewGovernanceTypeModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spGetGovernanceTypes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader =await  command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            governanceTypes.Add(new ViewGovernanceTypeModel
                            {
                                GovernanceTypeID = reader["GovernanceTypeID"].ToString(),
                                Name = reader["Name"].ToString(),
                                CreatedBy = reader["CreatedBy"].ToString(),
                                UpdatedBy = reader["UpdatedBy"].ToString(),
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                UpdatedDate = (DateTime)reader["UpdatedDate"],
                            });
                        }
                    }
                }
            }

            return governanceTypes;
        }
        public async Task<int> UpdateGovernanceType(string governanceTypeID, ViewGovernanceTypeModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spUpdateGovernanceType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@GovernanceTypeID", governanceTypeID);
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                    command.Parameters.AddWithValue("@UpdatedBy", model.UpdatedBy);
                    command.Parameters.AddWithValue("@CreatedDate", model.CreatedDate);
                    command.Parameters.AddWithValue("@UpdatedDate", model.UpdatedDate);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        
        public async Task<int> DeleteGovernanceType(string governanceTypeID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("DeleteGovernanceType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@GovernanceTypeID", governanceTypeID);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }



    }
}
