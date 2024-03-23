using System.Data;
using System.Data.SqlClient;
using Dto;
using Structure;


namespace DBService
{
    public class InstituteFilters : IInstituteFilters
    {
        private readonly string _connectionString;
        public InstituteFilters(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("UserDB"); ;
        }

        public async Task<List<ViewInstitutionModel>> GetInstitutionsByStatus(string status)
        {
            List<ViewInstitutionModel> institutions = new List<ViewInstitutionModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("spGetInstitutionsByStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Status", status);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ViewInstitutionModel vim = new ViewInstitutionModel
                            {
                                InstitutionID = reader["institutionID"].ToString(),
                                Name = reader["name"].ToString(),
                                statusReferenceID = reader["iInstitutionStatusID"].ToString(),
                                IsDeleted = Convert.ToBoolean(reader["isDeleted"]),
                                additionalAddress = reader["address"].ToString(),
                                IsDeletable = Convert.ToBoolean(reader["isDeletable"]),
                                UTLzipcodeID = reader["zipcodeID"].ToString(),
                                CreatedBy = reader["createdBy"].ToString(),
                                UpdatedBy = reader["updatedBy"].ToString(),
                                CreatedDate = (DateTime)reader["createdDate"],
                                UpdatedDate = (DateTime)reader["updatedDate"]
                            };
                            institutions.Add(vim);
                        }
                    }
                }
            }
            return institutions;
        }
        public async Task<List<FilterByDateModel>> GetInstitutionsSortedByDateAsync(string sortOrder)
        {
            List<FilterByDateModel> filterdata = new List<FilterByDateModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("spGetInstitutionsSortedByDate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@sortOrder", sortOrder);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            FilterByDateModel vim = new FilterByDateModel
                            {
                                institutionID = reader["institutionID"].ToString(),
                                name = reader["name"] is DBNull ? null : reader["name"].ToString(),
                                iInstitutionStatusID = reader["iInstitutionStatusID"] is DBNull ? null : reader["iInstitutionStatusID"].ToString(),
                                isDeleted = reader["isDeleted"] is DBNull ? (bool?)null : Convert.ToBoolean(reader["isDeleted"]),
                                address = reader["address"] is DBNull ? null : reader["address"].ToString(),
                                // IsDeletable = reader["IsDeletable"] is DBNull ? (bool?)null : Convert.ToBoolean(reader["IsDeletable"]),
                                zipcodeID = reader["zipcodeID"] is DBNull ? null : reader["zipcodeID"].ToString(),
                                // CreatedBy = reader["CreatedBy"] is DBNull ? null : reader["CreatedBy"].ToString(),
                                //UpdatedBy = reader["UpdatedBy"] is DBNull ? null : reader["UpdatedBy"].ToString(),
                                createdDate = reader["createdDate"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["createdDate"]),
                                //UpdatedDate = reader["UpdatedDate"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedDate"]),
                            };
                            filterdata.Add(vim);
                        }

                        return filterdata;
                    }
                }
            }
        }
        public async Task<List<institutionByCountry>> GetInstitutionsSortedByCountryAsync(string countryName)
        {
            List<institutionByCountry> filterdata = new List<institutionByCountry>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("spGetInstitutionsByCountry", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@countryName", countryName);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            institutionByCountry vim = new institutionByCountry
                            {
                                institutionID = reader["institutionID"].ToString(),
                                name = reader["name"] is DBNull ? null : reader["name"].ToString(),
                                //UpdatedDate = reader["UpdatedDate"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedDate"]),
                            };
                            filterdata.Add(vim);
                        }

                        return filterdata;
                    }
                }
            }

        }
        public async Task<List<institutionByCountry>> GetInstitutionsSortedByStateAsync(string stateName)
        {
            List<institutionByCountry> filterdata = new List<institutionByCountry>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("spGetInstitutionsByState", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@stateName", stateName);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            institutionByCountry vim = new institutionByCountry
                            {
                                institutionID = reader["institutionID"].ToString(),
                                name = reader["name"] is DBNull ? null : reader["name"].ToString(),
                            };
                            filterdata.Add(vim);
                        }
                        return filterdata;
                    }
                }
            }
        }
        public async Task<List<institutionByCountry>> GetInstitutionsSortedByCityAsync(string cityName)
        {
                List<institutionByCountry> filterdata = new List<institutionByCountry>();
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("spGetInstitutionsByCity", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@cityName", cityName);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                institutionByCountry vim = new institutionByCountry
                                {
                                    institutionID = reader["institutionID"].ToString(),
                                    name = reader["name"] is DBNull ? null : reader["name"].ToString(),
                                };
                                filterdata.Add(vim);
                            }
                            return filterdata;
                        }
                    }
                }
        }















    }


}