using System.Data.SqlClient;
using Constants.StoredProcedure;
using Package;
using Model.UserService;
using Middleware;


namespace DBService
{
    public class FacilityDBService
    {
        public async Task<StatusResponse<int>> CreateFacility(CreateFacilityModelRequest createFacilityModelRequest)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@Name", createFacilityModelRequest.Name),
                    new SqlParameter("@CreatedBy", createFacilityModelRequest.CreatedBy)
                };

                var storedProcedure = UserDB.CreateFacility;
                string connectionString = Utility.ConfigurationUtility.GetConnectionString();



                var result = await curdMiddleware.ExecuteNonQuery(
                    connectionString: connectionString,
                    storedProcedureName: storedProcedure,
                    parameters: parameter
                );

                if (result > 0)
                {
                    return StatusResponse<int>.SuccessStatus(result, StatusCode.Success);

                }
                else
                {
                    return StatusResponse<int>.FailureStatus(StatusCode.NotFound, new Exception());

                }

            }
            catch (Exception ex)
            {
                return StatusResponse<int>.FailureStatus(StatusCode.knownException, ex);
            }
        }

        public async Task<StatusResponse<GetFacilityModelResponse>> GetFacility(GetFacilityModelRequest getFacilityModelRequest)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@FacilityID", getFacilityModelRequest.FacilityID),
                   
                };

                var storedProcedure = UserDB.CreateFacility;
                string connectionString = Utility.ConfigurationUtility.GetConnectionString();



                var result = await curdMiddleware.ExecuteDataReaderSingle<GetFacilityModelResponse>(
                    connectionString: connectionString,
                    storedProcedureName: storedProcedure,
                    (reader) => new GetFacilityModelResponse
                    {
                        FacilityID = reader["facilityID"].ToString(),
                        Name = reader["name"].ToString(),
                        CreatedBy = reader["createdBy"].ToString(),
                        UpdatedBy = reader["updatedBy"].ToString(),
                        CreatedDate = (DateTime)reader["createdDate"],
                        UpdatedDate = (DateTime)reader["updatedDate"]
                    },
                    parameters: parameter
                );

                if (result != null)
                {
                    return StatusResponse<GetFacilityModelResponse>.SuccessStatus(result, StatusCode.Success);

                }
                else
                {
                    return StatusResponse<GetFacilityModelResponse>.FailureStatus(StatusCode.NotFound, new Exception());

                }
            }
            catch (Exception ex)
            {
                return StatusResponse<GetFacilityModelResponse>.FailureStatus(StatusCode.knownException, ex);
            }

        }
    }
}
