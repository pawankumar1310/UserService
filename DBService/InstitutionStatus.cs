using Structure;
using System.Data.SqlClient;
using System.Data;

namespace DBservice
{
    public class InstitutionStatus:IStatus
    {
        private readonly string _connectionString;
        public InstitutionStatus(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("UserDB"); ;
        }

        public async Task<int> approveInstitution(string statusName, string institutionID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spUpdateInstitutionStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@iInstitutionStatusName", SqlDbType.VarChar, 200).Value = statusName;
                    command.Parameters.Add("@institutionID", SqlDbType.VarChar, 50).Value = institutionID;
                    return await command.ExecuteNonQueryAsync();
                    
                }
            }
        }
    }
}
