using Constants.StoredProcedure;
using Constants;
using Middleware;
using Model.UserService;
using Package;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DBService
{
    public class AddInstituteDBService
    {
        public string connectionString = Utility.ConfigurationUtility.GetConnectionString();
        public async Task<StatusResponse<InstitutionIDModelResponse?>> RegisterInstitute(AddInstituteModelRequest registerInstitute)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@Name", registerInstitute.Name),
                    new SqlParameter("@CreatedBy", registerInstitute.CreatedBy),
                    new SqlParameter("@url", registerInstitute.url),
                    new SqlParameter("@institutionFacility", string.Join(",",registerInstitute.institutionFacilities)),
                    new SqlParameter("@institutionGovernance", string.Join(",",registerInstitute.institutionGovernance)),
                    new SqlParameter("@categories", string.Join(",", registerInstitute.Categories)),
                    new SqlParameter("@levels", string.Join(",", registerInstitute.Levels)),
                    new SqlParameter("@courses", string.Join(",", registerInstitute.Courses)),
                    new SqlParameter("@management", string.Join(",", registerInstitute.Management)),
                    new SqlParameter("@association", string.Join(",", registerInstitute.Association)),
                    new SqlParameter("@yearofEstablishment", registerInstitute.YearOfEstablishment),
                    new SqlParameter("@affiliationID",registerInstitute.AffiliationID)
                };

                var storedProcedure = UserDB.AddInstituteV3;
                string outputValue = OutputParameters.InstitutionID;
                var result = curdMiddleware.ExecuteNonQueryGeneric<string?>(
                connectionString: connectionString,
                storedProcedureName: storedProcedure,
                outputValue: outputValue,
                outputParameterName: OutputParameters.InstitutionID,
                parameters: parameter
                );
                if (result != null)
                {
                    InstitutionIDModelResponse institutionIDModelResponse = new()
                    {
                        InstitutionID = result.Result
                    };
                    return StatusResponse<InstitutionIDModelResponse?>.SuccessStatus(institutionIDModelResponse, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<InstitutionIDModelResponse>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<InstitutionIDModelResponse?>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public async Task<StatusResponse<int>> AddInstituteAddress(InstituteAddressModelRequest instituteAddressModel)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@additionalAddress", instituteAddressModel.AdditionalAddress),
                    new SqlParameter("@landmark", instituteAddressModel.Landmark),
                    new SqlParameter("@zipCodeID", instituteAddressModel.ZipCodeID),
                    new SqlParameter("@statusreferenceID", instituteAddressModel.StatusReferenceID),
                    new SqlParameter("@createdBy", instituteAddressModel.UserID),
                };

                var storedProcedure = UserDB.InstituteAddress;
                var result = await curdMiddleware.ExecuteNonQuery(connectionString, storedProcedure, parameter);
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
        public async Task<StatusResponse<List<GetOwnershipModelResponse>>> GetOwnerShip()
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UserDB.GetOwnerShip;
                var result = await curdMiddleware.ExecuteDataReaderList<GetOwnershipModelResponse>(
                    connectionString, storedProcedure,
                    (reader) => new GetOwnershipModelResponse
                    {
                        OwnershipTypeID = reader["ownershipTypeID"].ToString()!,
                        OwnershipTypeName = reader["ownershipTypeName"].ToString()!,
                    });
                if (result != null)
                {
                    return StatusResponse<List<GetOwnershipModelResponse>>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<List<GetOwnershipModelResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetOwnershipModelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }

        }
        public async Task<StatusResponse<int>> AddInstituteOwnership(AddInstituteOwnershipRequestModel instituteOwnershipModel)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@institutionID", instituteOwnershipModel.institutionID),
                    new SqlParameter("@ownerShipID", instituteOwnershipModel.ownerShipID),
                    new SqlParameter("@createdBy", instituteOwnershipModel.createdBy),

                };

                var storedProcedure = UserDB.CreateInstitutionOwnership;
                var result = await curdMiddleware.ExecuteNonQuery(connectionString, storedProcedure, parameter);
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
        public async Task<StatusResponse<List<GetGovernanceModelResponse>>> GetGovernance()
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UserDB.GetGovernance;
                var result = await curdMiddleware.ExecuteDataReaderList<GetGovernanceModelResponse>(
                    connectionString, storedProcedure,
                    (reader) => new GetGovernanceModelResponse
                    {
                        GovernaceID = reader["governaceID"].ToString()!,
                        Name = reader["name"].ToString()!,
                    });
                if (result != null)
                {
                    return StatusResponse<List<GetGovernanceModelResponse>>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<List<GetGovernanceModelResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetGovernanceModelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }

        }
        public async Task<StatusResponse<List<GetFacilityRequestModel>>> GetFacilities()
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UserDB.GetFacility;
                var result = await curdMiddleware.ExecuteDataReaderList<GetFacilityRequestModel>(
                    connectionString, storedProcedure,
                    (reader) => new GetFacilityRequestModel
                    {
                        FacilityID = reader["facilityID"].ToString()!,
                        Name = reader["name"].ToString()!,
                    });
                if (result != null)
                {
                    return StatusResponse<List<GetFacilityRequestModel>>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<List<GetFacilityRequestModel>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetFacilityRequestModel>>.FailureStatus(StatusCode.knownException, ex);
            }

        }
        public async Task<StatusResponse<int>> AddInstituteDataTemp(InstituteDataTempModelRequest instituteDataModel)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@InstitutionNumberorID", instituteDataModel.InstitutionNumberorID),
                    new SqlParameter("@InstitutionCategory", instituteDataModel.InstitutionCategory),
                    new SqlParameter("@InstitutionLevel", instituteDataModel.InstitutionLevel),
                    new SqlParameter("@InstitutionCourse", instituteDataModel.InstitutionCourse),
                    new SqlParameter("@InstitutionManagement", instituteDataModel.InstitutionManagement),
                    new SqlParameter("@InstitutionAssosication", instituteDataModel.InstitutionAssosication),
                    new SqlParameter("@institutionID", instituteDataModel.InstitutionID)
                };

                var storedProcedure = UserDB.InsertInstituteTempData;
                var result = await curdMiddleware.ExecuteNonQuery(connectionString, storedProcedure, parameter);
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
        public async Task<StatusResponse<int>> AddInstituteHeadData(InstituteHeadModelRequest instituteHeadModel)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@names", instituteHeadModel.Names),
                    new SqlParameter("@gender", instituteHeadModel.Gender),
                    new SqlParameter("@joiningDate", instituteHeadModel.JoiningDate),
                    new SqlParameter("@phoneNumber", instituteHeadModel.PhoneNumber),
                    new SqlParameter("@email", instituteHeadModel.Email),
                    new SqlParameter("@institutionId", instituteHeadModel.InstitutionId)
            };

                var storedProcedure = UserDB.AddInstituteHead;
                var result = await curdMiddleware.ExecuteNonQuery(connectionString, storedProcedure, parameter);
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

        public async Task<StatusResponse<int>> AddInstituteCodeForLyceeEGov(InstituteCodeForLyceeEGovModel instituteCodeModel)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@value", instituteCodeModel.value),
                    new SqlParameter("@instituteid", instituteCodeModel.instituteid)
                };

                var storedProcedure = UserDB.AddInstituteCodeForLyceeEGov;
                var result = await curdMiddleware.ExecuteNonQuery(connectionString, storedProcedure, parameter);
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
        public async Task<StatusResponse<int>> AddUserInstitute(AddUserInstituteModelRequest addUserInstituteModel)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@userid", addUserInstituteModel.userid),
                    new SqlParameter("@instituteid", addUserInstituteModel.instituteid)
                };

                var storedProcedure = UserDB.AddUserInstitute;
                var result = await curdMiddleware.ExecuteNonQuery(connectionString, storedProcedure, parameter);
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



        public async Task<StatusResponse<List<GetCategoriesModelResponse>>> GetCategories()
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UserDB.GetCategories;
                var result = await curdMiddleware.ExecuteDataReaderList<GetCategoriesModelResponse>(
                    connectionString, storedProcedure,
                    (reader) => new GetCategoriesModelResponse
                    {
                        categoriesID = reader["categoriesID"].ToString()!,
                        categoryName = reader["categoryName"].ToString()!,
                    });
                if (result != null)
                {
                    return StatusResponse<List<GetCategoriesModelResponse>>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<List<GetCategoriesModelResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetCategoriesModelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }

        }
        public async Task<StatusResponse<List<GetLevelModelResponse>>> GetLevel()
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UserDB.GetLevels;
                var result = await curdMiddleware.ExecuteDataReaderList<GetLevelModelResponse>(
                    connectionString, storedProcedure,
                    (reader) => new GetLevelModelResponse
                    {
                        levelsID = reader["levelsID"].ToString()!,
                        levelsName = reader["levelsName"].ToString()!,
                    });
                if (result != null)
                {
                    return StatusResponse<List<GetLevelModelResponse>>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<List<GetLevelModelResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetLevelModelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }

        }
        public async Task<StatusResponse<List<GetCourseModelResponse>>> GetCourses()
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UserDB.GetCourse;
                var result = await curdMiddleware.ExecuteDataReaderList<GetCourseModelResponse>(
                    connectionString, storedProcedure,
                    (reader) => new GetCourseModelResponse
                    {
                        coursesID = reader["coursesID"].ToString()!,
                        coursesName = reader["coursesName"].ToString()!,
                    });
                if (result != null)
                {
                    return StatusResponse<List<GetCourseModelResponse>>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<List<GetCourseModelResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetCourseModelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }

        }
        public async Task<StatusResponse<List<GetManagementModelResponse>>> GetManagement()
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UserDB.GetManagement;
                var result = await curdMiddleware.ExecuteDataReaderList<GetManagementModelResponse>(
                    connectionString, storedProcedure,
                    (reader) => new GetManagementModelResponse
                    {
                        managementID = reader["managementID"].ToString()!,
                        managementName = reader["managementName"].ToString()!,
                    });
                if (result != null)
                {
                    return StatusResponse<List<GetManagementModelResponse>>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<List<GetManagementModelResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetManagementModelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }

        }
        public async Task<StatusResponse<List<GetAssosicationModelResponse>>> GetAssociation()
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UserDB.GetAssosication;
                var result = await curdMiddleware.ExecuteDataReaderList<GetAssosicationModelResponse>(
                    connectionString, storedProcedure,
                    (reader) => new GetAssosicationModelResponse
                    {
                        associationID = reader["associationID"].ToString()!,
                        associationName = reader["associationName"].ToString()!,
                    });
                if (result != null)
                {
                    return StatusResponse<List<GetAssosicationModelResponse>>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<List<GetAssosicationModelResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetAssosicationModelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }

        }

        public async Task<StatusResponse<List<GetUserInstDetailsModel>>> GetUserInstituteDetails(UserIDRequestModel userIDRequestModel )
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UserDB.GetUserInstitutes;
                var parameter = new SqlParameter[] { new SqlParameter("@userID", userIDRequestModel.UserID) };
                var result = await curdMiddleware.ExecuteDataReaderList<GetUserInstDetailsModel>(
                    connectionString, storedProcedure,
                    (reader) => 
                    new GetUserInstDetailsModel
                    {
                        InstitutionID = reader["institutionID"] != DBNull.Value ? reader["institutionID"].ToString()! : null,
                        InstitutionName = reader["institutionName"].ToString()!,
                        YearOfEstablishment = reader["yearofEstablishment"].ToString()!,
                        CategoryID = GetListFromReader<string>(reader, "categoryID"),
                        CategoryName = GetListFromReader<string>(reader, "categoryName"),
                        LevelID = GetListFromReader<string>(reader, "levelID"),
                        LevelName = GetListFromReader<string>(reader, "levelsName"),
                        CourseID = GetListFromReader<string>(reader, "courseID"),
                        CourseName = GetListFromReader<string>(reader, "coursesName"),
                        ManagementID = GetListFromReader<string>(reader, "managementID"),
                        ManagementName = GetListFromReader<string>(reader, "managementName"),
                        AssociationID = GetListFromReader<string>(reader, "associationID"),
                        AssociationName = GetListFromReader<string>(reader, "associationName"),
                        HeadName = reader["Names"] != DBNull.Value ? reader["Names"].ToString()! : null,
                        Gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString()! : null,
                        JoiningDate = reader["JoiningDate"] != DBNull.Value ? Convert.ToDateTime(reader["JoiningDate"]) : DateTime.MinValue,
                        PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString()! : null,
                        EmailID = reader["EmailID"] != DBNull.Value ? reader["EmailID"].ToString()! : null,
                        OwnershipID = GetListFromReader<string>(reader, "ownershipID"),
                        OwnershipTypeName = GetListFromReader<string>(reader, "ownershipTypeName"),
                        GovernanceID = GetListFromReader<string>(reader, "governanceID"),
                        AffiliationNumber = reader["affiliationNumber"] != DBNull.Value ? reader["affiliationNumber"].ToString()! : null,
                        GovernanceName = GetListFromReader<string>(reader, "governanceName"),
                        FacilityID = GetListFromReader<string>(reader, "facilitieID"),
                        FacilityName = GetListFromReader<string>(reader, "facilityName"),
                        InstitutionIDValue = reader["institutionIDValue"] != DBNull.Value ? reader["institutionIDValue"].ToString()! : null,
                        AddressID = reader["addressID"] != DBNull.Value ? reader["addressID"].ToString()! : null,
                        AdditionalAddress = reader["additionalAddress"] != DBNull.Value ? reader["additionalAddress"].ToString()! : null,
                        LandMark = reader["landMark"] != DBNull.Value ? reader["landMark"].ToString()! : null,
                        UTLzipCodeID = reader["UTLzipCodeID"] != DBNull.Value ? reader["UTLzipCodeID"].ToString()! : null,
                    }, parameter);
                List<T> GetListFromReader<T>(SqlDataReader reader, string columnName)
                {
                    return reader[columnName] != DBNull.Value
                        ? new List<T> { (T)reader[columnName] }
                        : new List<T>();
                }
                if (result != null && result[0].InstitutionID!=null)
                {
                    return StatusResponse<List<GetUserInstDetailsModel>>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<List<GetUserInstDetailsModel>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetUserInstDetailsModel>>.FailureStatus(StatusCode.knownException, ex);
            }
        }

    }
}
