using System.Data.SqlClient;
using Model.UserService;
using System.Data;
using Middleware;
using Package;
using Constants.StoredProcedure;
using Constants;

namespace DBService
{
    public class UserRegistrationDBService
    {

        public async Task<StatusResponse<string>?> RegisterUser(RegisterUserModelRequest registerUserModelRequest)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@FirstName", registerUserModelRequest.FirstName),
                    new SqlParameter("@LastName", registerUserModelRequest.LastName),
                    new SqlParameter("@CountryID", registerUserModelRequest.CountryID),
                    new SqlParameter("@PhoneNumber", registerUserModelRequest.PhoneNumber),
                    new SqlParameter("@EmailAddress", registerUserModelRequest.EmailAddress),
                    new SqlParameter("@additionalAddress", registerUserModelRequest.AdditionalAddress),
                    new SqlParameter("@UTLzipCodeID", registerUserModelRequest.ZipcodeID)
                };

                var storedProcedure = UserDB.RegisterUser;
                string connectionString = Utility.ConfigurationUtility.GetConnectionString();
                string outputValue = OutputParameters.UserID;


                var result =  curdMiddleware.ExecuteNonQueryGeneric<string>(
                    connectionString: connectionString,
                    storedProcedureName: storedProcedure,
                    outputValue: outputValue,
                    outputParameterName: OutputParameters.UserID,
                    parameters: parameter
                ).Result;

                if( result != null)  
                {
                    return StatusResponse<string?>.SuccessStatus(result, StatusCode.Success);

                }
                else
                {
                    return StatusResponse<string?>.FailureStatus(StatusCode.NotFound, new Exception());
                }

            }
            catch (Exception ex)
            {
                return StatusResponse<string?>.FailureStatus(StatusCode.knownException, ex);
            }
        }

        public async Task<StatusResponse<int>> UpdateUserWithUrls(UpdateUserWithUrlModelRequest updateUserWithUrlModelRequest)
        {
            try
            {

                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@UserID", updateUserWithUrlModelRequest.UserID),
                    new SqlParameter("@FirstName", updateUserWithUrlModelRequest.FirstName),
                    new SqlParameter("@LastName", updateUserWithUrlModelRequest.LastName),
                    new SqlParameter("@CountryID", updateUserWithUrlModelRequest.CountryID),
                    new SqlParameter("@PhoneNumber", updateUserWithUrlModelRequest.PhoneNumber),
                    new SqlParameter("@EmailAddress", updateUserWithUrlModelRequest.EmailAddress),
                    new SqlParameter("@additionalAddress", updateUserWithUrlModelRequest.AdditionalAddress),
                    new SqlParameter("@ZipCodeID", updateUserWithUrlModelRequest.ZipCodeID)
                };

                var storedProcedure = UserDB.UpdateUserWithUrls;
                string connectionString = Utility.ConfigurationUtility.GetConnectionString();

                DataTable urlsTable = new DataTable();
                urlsTable.Columns.Add("url", typeof(string));
                urlsTable.Columns.Add("label", typeof(string));

                if (updateUserWithUrlModelRequest.UserUrls != null)
                {
                    foreach (var url in updateUserWithUrlModelRequest.UserUrls)
                    {
                        urlsTable.Rows.Add(url.Url, url.UrlLabelId);
                    }
                }

                SqlParameter urlsParameter = new SqlParameter("@Urls", SqlDbType.Structured);
                urlsParameter.Value = urlsTable;
                urlsParameter.TypeName = "dbo.UpdateUrlTableType"; 

                var result = await curdMiddleware.ExecuteNonQuery(
                    connectionString: connectionString,
                    storedProcedureName: storedProcedure,
                    parameters: parameter.Concat(new[] { urlsParameter }).ToArray()
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

        public async Task<StatusResponse<UserWithUrlModelResponse>> GetUserWithUrls(UserWithUrlModelRequest userWithUrlModelRequest)
        {
            try
                {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[] { new SqlParameter("@UserID", userWithUrlModelRequest.UserID) };
                string connectionString = Utility.ConfigurationUtility.GetConnectionString();
                var storedProcedure = UserDB.GetUserWithUrls;

                var result = await curdMiddleware.ExecuteDataReaderSingle(connectionString, storedProcedure, (reader) =>
                {
                    return Task.Run(async () =>
                    {
                        UserWithUrlModelResponse user = new UserWithUrlModelResponse
                        {
                            UserID = await reader.GetFieldValueAsync<string>("userID"),
                            FirstName = await reader.GetFieldValueAsync<string>("firstName"),
                            LastName = await reader.GetFieldValueAsync<string>("lastName"),
                            AdditionalAddress = await reader.GetFieldValueAsync<string>("additionalAddress"),
                            UTLzipcodeID = await reader.GetFieldValueAsync<string>("UTLzipcodeID"),
                            PhoneNumber = await reader.GetFieldValueAsync<long?>("phoneNumber"),
                            EmailAddress = await reader.GetFieldValueAsync<string?>("emailAddress"),
                            UTLcountryID = await reader.GetFieldValueAsync<string>("UTLcountryID"),
                            UserUrls = new List<UrlModelResponse>()
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("urlID")))
                        {
                            user.UserUrls.Add(new UrlModelResponse
                            {
                                UrlID = await reader.GetFieldValueAsync<string>("urlID"),
                                UTLiTableID = await reader.GetFieldValueAsync<string>("UTLiTableID"),
                                Url = await reader.GetFieldValueAsync<string>("url"),
                                UrlLabelId = await reader.GetFieldValueAsync<string>("urllabelId"),
                                ReferenceID = await reader.GetFieldValueAsync<string>("referenceID")
                            });
                        }

                        while (await reader.ReadAsync())
                        {
                            user.UserUrls.Add(new UrlModelResponse
                            {
                                UrlID = await reader.GetFieldValueAsync<string>("urlID"),
                                UTLiTableID = await reader.GetFieldValueAsync<string>("UTLiTableID"),
                                Url = await reader.GetFieldValueAsync<string>("url"),
                                UrlLabelId = await reader.GetFieldValueAsync<string>("urllabelId"),
                                ReferenceID = await reader.GetFieldValueAsync<string>("referenceID")
                            });
                        }

                        return user;
                    }).Result;
                }, parameter);

                if (result != null)
                {
                    return StatusResponse<UserWithUrlModelResponse>.SuccessStatus(result, StatusCode.Success);

                }
                else
                {
                    return StatusResponse<UserWithUrlModelResponse>.FailureStatus(StatusCode.NotFound, new Exception());

                }
            }
            catch (Exception ex)
            {
                return StatusResponse<UserWithUrlModelResponse>.FailureStatus(StatusCode.knownException, ex);
            }
        }



        public async Task<StatusResponse<int>> UpdateUserAdditionalAddress(UpdateUserAdditionalAddressModelRequest updateUserAdditionalAddressModelRequest)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[] 
                { 
                    new SqlParameter("@UserID", updateUserAdditionalAddressModelRequest.UserID) ,
                    new SqlParameter("@AdditionalAddress", updateUserAdditionalAddressModelRequest.AdditionalAddress)
                };
                string connectionString = Utility.ConfigurationUtility.GetConnectionString();
                var storedProcedure = UserDB.UpdateUserAdditionalAddress;

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


        public async Task<StatusResponse<GetUsersAddtionalAddressModelResponse>> GetUsersAddtionalAddress(GetUsersAddtionalAddressModelRequest getUsersAddtionalAddressModelRequest)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@UserID", getUsersAddtionalAddressModelRequest.UserID) 
                };
                string connectionString = Utility.ConfigurationUtility.GetConnectionString();
                var storedProcedure = UserDB.GetUsersAddtionalAddress;

                var result = await curdMiddleware.ExecuteDataReaderSingle<GetUsersAddtionalAddressModelResponse>(
                         connectionString: connectionString,
                         storedProcedureName: storedProcedure,
                         (reader) => new GetUsersAddtionalAddressModelResponse
                         {
                             AdditionalAddress = reader["additionalAddress"].ToString()
                         }
                         ,parameters: parameter
                     );

                if (result != null)
                {
                    return StatusResponse<GetUsersAddtionalAddressModelResponse>.SuccessStatus(result, StatusCode.Success);

                }
                else
                {
                    return StatusResponse<GetUsersAddtionalAddressModelResponse>.FailureStatus(StatusCode.NotFound, new Exception());

                }

            }
            catch (Exception ex)
            {
                return StatusResponse<GetUsersAddtionalAddressModelResponse>.FailureStatus(StatusCode.knownException, ex);
            }
        }

    }
}

    
