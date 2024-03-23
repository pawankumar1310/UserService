using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dto;
using Structure;

namespace DBService
{
    public class FacilityDBService : IFacilityService
    {
        private readonly string _connectionString;

        public FacilityDBService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UserDB");
        }

        public async Task<bool> CreateFacility(CreateFacilityRequest createRequest)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("CreateFacilitySP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", createRequest.Name);
                    command.Parameters.AddWithValue("@CreatedBy", createRequest.CreatedBy);


                    await command.ExecuteNonQueryAsync();

                    return true;
                }
            }
        }

        public async Task<Facility> GetFacilityById(string facilityId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetFacilityByIdSP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FacilityID", facilityId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Facility
                            {
                                FacilityID = (string)reader["facilityID"],
                                Name = reader["name"].ToString(),
                                CreatedBy = reader["createdBy"].ToString(),
                                UpdatedBy = reader["updatedBy"].ToString(),
                                CreatedDate = (DateTime)reader["createdDate"],
                                UpdatedDate = (DateTime)reader["updatedDate"],
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task<List<Facility>> GetAllFacilities()
        {
            List<Facility> facilities = new List<Facility>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetAllFacilitiesSP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            facilities.Add(new Facility
                            {
                                FacilityID = (string)reader["facilityID"],
                                Name = reader["name"].ToString(),
                                CreatedBy = reader["createdBy"].ToString(),
                                UpdatedBy = reader["updatedBy"].ToString(),
                                CreatedDate = (DateTime)reader["createdDate"],
                                UpdatedDate = (DateTime)reader["updatedDate"],
                            });
                        }
                    }
                }
            }

            return facilities;
        }

        public async Task UpdateFacility(string facilityId, UpdateFacilityRequest updateRequest)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UpdateFacilitySP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FacilityID", facilityId);
                    command.Parameters.AddWithValue("@Name", updateRequest.Name);
                    command.Parameters.AddWithValue("@UpdatedBy", updateRequest.UpdatedBy);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteFacility(string facilityId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DeleteFacilitySP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FacilityID", facilityId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
