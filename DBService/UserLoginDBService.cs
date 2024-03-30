using System.Data.SqlClient;
using Constants.StoredProcedure;
using Package;
using Model.UserService;
using Middleware;

namespace DBService
{
    public class UserLoginDBService
    {
        public async Task<StatusResponse<UserStatusModelResponse>> CheckUserExistence(UserStatusModelRequest userStatusModelRequest)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[] { new SqlParameter("@UserInput", userStatusModelRequest.UserIdentity) };
                var storedProcedure = UserDB.CheckUserExistence;

                string connectionString = Utility.ConfigurationUtility.GetConnectionString();

                var result = await curdMiddleware.ExecuteDataReaderSingle<UserStatusModelResponse>(
                    connectionString: connectionString,
                    storedProcedureName: storedProcedure,
                    mapFunction: (reader) => new UserStatusModelResponse
                    {
                        UserNAmeStatus = Convert.ToBoolean(reader["IsUsernameExists"]) || Convert.ToBoolean(reader["IsEmailExists"]) || Convert.ToBoolean(reader["IsPhoneNumberExists"]),
                        IsOtp = Convert.ToBoolean(reader["IsEmailExists"]) || Convert.ToBoolean(reader["IsPhoneNumberExists"]),
                        IsOtpAvailable = Convert.ToBoolean(reader["IsEmailExists"]) || Convert.ToBoolean(reader["IsPhoneNumberExists"]),
                        IsPasswordAvailable = Convert.ToBoolean(reader["IsUsernameExists"]) && Convert.ToBoolean(reader["IsPasswordExists"])
                    },
                    parameters: parameter
                );
                if (result.UserNAmeStatus)
                {
                    return StatusResponse<UserStatusModelResponse>.SuccessStatus(result, StatusCode.Success);

                }
                else
                {
                    return StatusResponse<UserStatusModelResponse>.FailureStatus(StatusCode.NotFound, new Exception());

                }

            }
            catch (Exception ex)
            {
                return StatusResponse<UserStatusModelResponse>.FailureStatus(StatusCode.knownException, ex);
            }
        }

        public async Task<StatusResponse<GetUserIdByEmailModelResponse>> GetUserIdByEmail(GetUserIdByEmailModelRequest getUserIdByEmailModelRequest)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                new SqlParameter("@EmailAddress", getUserIdByEmailModelRequest.UserEmail)
                };

                var storedProcedure = UserDB.GetUserIdByEmailAddress;
                string connectionString = Utility.ConfigurationUtility.GetConnectionString();



                var result = await curdMiddleware.ExecuteDataReaderSingle(
                    connectionString: connectionString,
                    storedProcedureName: storedProcedure,
                    (reader) => new GetUserIdByEmailModelResponse
                    {
                        UserId = reader["UserID"].ToString()
                    },
                    parameters: parameter
                );

                if (result.UserId != "")
                {
                    return StatusResponse<GetUserIdByEmailModelResponse>.SuccessStatus(result, StatusCode.Success);

                }
                else
                {
                    return StatusResponse<GetUserIdByEmailModelResponse>.FailureStatus(StatusCode.NotFound, new Exception());

                }

            }
            catch (Exception ex)
            {
                return StatusResponse<GetUserIdByEmailModelResponse>.FailureStatus(StatusCode.knownException, ex);
            }
        }


        
    }
}