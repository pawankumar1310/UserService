using Structure;
using System.Data.SqlClient;
using System.Data;
using Dto;

namespace DBService
{
    public class GovernanceDB : IGovernance
    {
        private readonly string _connectionString;
        public GovernanceDB(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("UserDB"); ;
        }

        public async Task<int> InsertGovernance(GovernanceModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spInsertGovernance", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", model.Name);
                    command.Parameters.AddWithValue("@UTLZipCodeID", model.UTLZipCodeID);
                    command.Parameters.AddWithValue("@governanceTypeID", model.GovernanceTypeID);
                    command.Parameters.AddWithValue("@createdBy", model.CreatedBy);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<List<GetGovernanceModel>> GetGovernance()
        {
            List<GetGovernanceModel> governanceTypes = new List<GetGovernanceModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spGetGovernanceList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            governanceTypes.Add(new GetGovernanceModel
                            {
                                GovernaceID = reader["governaceID"] as string,
                                Name = reader["name"] as string,
                                UTLpincodeID = reader["UTLZipCodeID"] as string,
                                GovernanceTypeID = reader["governanceTypeID"] as string,
                                CreatedBy = reader["createdBy"] as string,
                                UpdatedBy = reader["updatedBy"] as string,
                                CreatedDate = reader["createdDate"] != DBNull.Value ? (DateTime)reader["createdDate"] : default(DateTime),
                                UpdatedDate = reader["updatedDate"] != DBNull.Value ? (DateTime)reader["updatedDate"] : default(DateTime),
                            });
                        }
                    }
                }
            }

            return governanceTypes;
        }
        public async Task<int> UpdateGovernance(string governanceID, UpdateGovernanceModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spUpdateGovernanceById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@governaceID", model.GovernaceID);
                    command.Parameters.AddWithValue("@name", model.Name);
                    command.Parameters.AddWithValue("@UTLpincodeID", model.UTLpincodeID);
                    command.Parameters.AddWithValue("@governanceTypeID", model.GovernanceTypeID);
                    command.Parameters.AddWithValue("@updatedBy", model.UpdatedBy);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteGovernance(string governanceID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spDeleteGovernanceById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@governaceID", governanceID);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }



    }
}

