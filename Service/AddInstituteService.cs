using Package;
using DTO.UserService;
using DBService;
using Model.UserService;

namespace Service
{
    public class AddInstituteService
    {
        public StatusResponse<InstituteIDResponse> RegisterInstitute(AddInstituteRequest addInstituteRequest)
        {
            var addInstitute = new AddInstituteModelRequest
            {
                Name = addInstituteRequest.Name,
                AffiliationID = addInstituteRequest.AffiliationID,
                CreatedBy = addInstituteRequest.CreatedBy,
                institutionFacilities = addInstituteRequest.institutionFacilities,
                institutionGovernance = addInstituteRequest.institutionGovernance,
                Association = addInstituteRequest.Association,
                Categories = addInstituteRequest.Categories,
                Courses = addInstituteRequest.Courses,
                Levels = addInstituteRequest.Levels,
                Management = addInstituteRequest.Management,
                url = addInstituteRequest.url,
                YearOfEstablishment = addInstituteRequest.YearOfEstablishment
            };

            try
            {
                AddInstituteDBService addInstituteDBService = new();
                var result = addInstituteDBService.RegisterInstitute(addInstitute).Result;
                if (result.Success)
                {
                    InstituteIDResponse institute = new()
                    {
                        InstitutionID = result.Data.InstitutionID
                    };
                    return StatusResponse<InstituteIDResponse?>.SuccessStatus(institute, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<InstituteIDResponse?>.FailureStatus(result.StatusCode, new Exception());
                }

            }

            catch (Exception ex)
            {
                return StatusResponse<InstituteIDResponse?>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<int> AddInstituteAddress(InstituteAddressRequest instituteAddress)
        {
            try
            {
                AddInstituteDBService addInstituteDB = new();
                var addAddressModel = new InstituteAddressModelRequest
                {
                    AdditionalAddress = instituteAddress.AdditionalAddress,
                    UserID = instituteAddress.UserID,
                    StatusReferenceID = instituteAddress.StatusReferenceID,
                    Landmark = instituteAddress.Landmark,
                    ZipCodeID = instituteAddress.ZipCodeID,
                };
                var result = addInstituteDB.AddInstituteAddress(addAddressModel).Result;
                if (result.Success)
                {
                    return StatusResponse<int>.SuccessStatus(result.Data, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<int>.FailureStatus(result.StatusCode, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<int>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<List<OwnershipResponse>> GetOwnership()
        {
            try
            {
                AddInstituteDBService addInstituteDBService = new();
                var result = addInstituteDBService.GetOwnerShip().Result;
                if (result.Success)
                {
                    List<OwnershipResponse> ownerResponse = new();
                    foreach (var owner in result.Data)
                    {
                        ownerResponse.Add(new OwnershipResponse
                        {
                            OwnershipTypeName = owner.OwnershipTypeName,
                            OwnershipTypeID = owner.OwnershipTypeID,
                            
                        });
                    }
                    return StatusResponse<List<OwnershipResponse>>.SuccessStatus(ownerResponse, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<List<OwnershipResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<OwnershipResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<int> AddInstituteOwnerShip(AddInstituteOwnershipRequest instituteOwner)
        {
            try
            {
                AddInstituteDBService addInstituteDB = new();
                var addInsOwnModel = new AddInstituteOwnershipRequestModel
                {
                    institutionID = instituteOwner.institutionID,
                    ownerShipID = instituteOwner.ownerShipID,
                    createdBy = instituteOwner.createdBy,
                };
                var result = addInstituteDB.AddInstituteOwnership(addInsOwnModel).Result;
                if (result.Success)
                {
                    return StatusResponse<int>.SuccessStatus(result.Data, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<int>.FailureStatus(result.StatusCode, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<int>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<List<GetGovernanceResponse>> GetGovernance()
        {
            try
            {
                AddInstituteDBService addInstituteDBService = new();
                var result = addInstituteDBService.GetGovernance().Result;
                if (result.Success)
                {
                    List<GetGovernanceResponse> ownerResponse = new();
                    foreach (var owner in result.Data)
                    {
                        ownerResponse.Add(new GetGovernanceResponse
                        {
                            GovernaceID = owner.GovernaceID,
                            Name = owner.Name,
                        });
                    }
                    return StatusResponse<List<GetGovernanceResponse>>.SuccessStatus(ownerResponse, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<List<GetGovernanceResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetGovernanceResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<List<GetFacilityResponse>> GetFacility()
        {
            try
            {
                AddInstituteDBService addInstituteDBService = new();
                var result = addInstituteDBService.GetFacilities().Result;
                if (result.Success)
                {
                    List<GetFacilityResponse> ownerResponse = new();
                    foreach (var owner in result.Data)
                    {
                        ownerResponse.Add(new GetFacilityResponse
                        {
                            FacilityID = owner.FacilityID,
                            Name = owner.Name,
                        });
                    }
                    return StatusResponse<List<GetFacilityResponse>>.SuccessStatus(ownerResponse, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<List<GetFacilityResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetFacilityResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<int> AddInstituteDataTemp(InstituteDataTempRequest instituteDataTempRequest)
        {
            try
            {
                AddInstituteDBService addInstituteDB = new();
                var addTempData = new InstituteDataTempModelRequest
                {
                    InstitutionAssosication = instituteDataTempRequest.InstitutionAssosication,
                    InstitutionID = instituteDataTempRequest.InstitutionID,
                    InstitutionCategory = instituteDataTempRequest.InstitutionCategory,
                    InstitutionNumberorID = instituteDataTempRequest.InstitutionNumberorID,
                    InstitutionCourse = instituteDataTempRequest.InstitutionCourse,
                    InstitutionLevel = instituteDataTempRequest.InstitutionLevel,
                    InstitutionManagement = instituteDataTempRequest.InstitutionManagement
                };
                var result = addInstituteDB.AddInstituteDataTemp(addTempData).Result;
                if (result.Success)
                {
                    return StatusResponse<int>.SuccessStatus(result.Data, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<int>.FailureStatus(result.StatusCode, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<int>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<int> AddInstituteHead(InstituteHeadRequest instituteHeadRequest)
        {
            try
            {
                AddInstituteDBService addInstituteDB = new();
                var addHeadData = new InstituteHeadModelRequest
                {
                    InstitutionId = instituteHeadRequest.InstitutionId,
                    Email = instituteHeadRequest.Email,
                    Gender = instituteHeadRequest.Gender,
                    JoiningDate = instituteHeadRequest.JoiningDate,
                    Names = instituteHeadRequest.Names,
                    PhoneNumber = instituteHeadRequest.PhoneNumber,
                };
                var result = addInstituteDB.AddInstituteHeadData(addHeadData).Result;
                if (result.Success)
                {
                    return StatusResponse<int>.SuccessStatus(result.Data, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<int>.FailureStatus(result.StatusCode, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<int>.FailureStatus(StatusCode.knownException, ex);
            }
        }

        public StatusResponse<int> AddInstituteCodeForLyceeEGov(InstituteCodeForLyceeEGOVRequest instituteCodeRequest)
        {
            try
            {
                AddInstituteDBService addInstituteDB = new();
                var addHeadData = new InstituteCodeForLyceeEGovModel
                {
                    instituteid = instituteCodeRequest.instituteid,
                    value = instituteCodeRequest.value
                };
                var result = addInstituteDB.AddInstituteCodeForLyceeEGov(addHeadData).Result;
                if (result.Success)
                {
                    return StatusResponse<int>.SuccessStatus(result.Data, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<int>.FailureStatus(result.StatusCode, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<int>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<int> AddUserInstitute(AddUserInstituteRequest userInstituteRequest)
        {
            try
            {
                AddInstituteDBService addInstituteDB = new();
                var addHeadData = new AddUserInstituteModelRequest
                {
                    instituteid = userInstituteRequest.instituteid,
                    userid = userInstituteRequest.userid,
                };
                var result = addInstituteDB.AddUserInstitute(addHeadData).Result;
                if (result.Success)
                {
                    return StatusResponse<int>.SuccessStatus(result.Data, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<int>.FailureStatus(result.StatusCode, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<int>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<List<GetCategoriesResponse>> GetCategories()
        {
            try
            {
                AddInstituteDBService addInstituteDBService = new();
                var result = addInstituteDBService.GetCategories().Result;
                if (result.Success)
                {
                    List<GetCategoriesResponse> ownerResponse = new();
                    foreach (var owner in result.Data)
                    {
                        ownerResponse.Add(new GetCategoriesResponse
                        {
                            categoriesID = owner.categoriesID,
                            categoryName = owner.categoryName,
                        });
                    }
                    return StatusResponse<List<GetCategoriesResponse>>.SuccessStatus(ownerResponse, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<List<GetCategoriesResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetCategoriesResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }

        public StatusResponse<List<GetLevelResponse>> GetLevel()
        {
            try
            {
                AddInstituteDBService addInstituteDBService = new();
                var result = addInstituteDBService.GetLevel().Result;
                if (result.Success)
                {
                    List<GetLevelResponse> ownerResponse = new();
                    foreach (var owner in result.Data)
                    {
                        ownerResponse.Add(new GetLevelResponse
                        {
                            levelsID = owner.levelsID,
                            levelsName =owner.levelsName
                        });
                    }
                    return StatusResponse<List<GetLevelResponse>>.SuccessStatus(ownerResponse, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<List<GetLevelResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetLevelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<List<GetCourseResponse>> GetCourses()
        {
            try
            {
                AddInstituteDBService addInstituteDBService = new();
                var result = addInstituteDBService.GetCourses().Result;
                if (result.Success)
                {
                    List<GetCourseResponse> ownerResponse = new();
                    foreach (var owner in result.Data)
                    {
                        ownerResponse.Add(new GetCourseResponse
                        {
                            coursesID = owner.coursesID,
                            coursesName = owner.coursesName,
                        });
                    }
                    return StatusResponse<List<GetCourseResponse>>.SuccessStatus(ownerResponse, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<List<GetCourseResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetCourseResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<List<GetManagementResponse>> GetManagement()
        {
            try
            {
                AddInstituteDBService addInstituteDBService = new();
                var result = addInstituteDBService.GetManagement().Result;
                if (result.Success)
                {
                    List<GetManagementResponse> ownerResponse = new();
                    foreach (var owner in result.Data)
                    {
                        ownerResponse.Add(new GetManagementResponse
                        {
                            managementID = owner.managementID,
                            managementName = owner.managementName,
                        });
                    }
                    return StatusResponse<List<GetManagementResponse>>.SuccessStatus(ownerResponse, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<List<GetManagementResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetManagementResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<List<GetAssosicationResponse>> GetAssociation()
        {
            try
            {
                AddInstituteDBService addInstituteDBService = new();
                var result = addInstituteDBService.GetAssociation().Result;
                if (result.Success)
                {
                    List<GetAssosicationResponse> ownerResponse = new();
                    foreach (var owner in result.Data)
                    {
                        ownerResponse.Add(new GetAssosicationResponse
                        {
                            associationID = owner.associationID,
                            associationName = owner.associationName,
                        });
                    }
                    return StatusResponse<List<GetAssosicationResponse>>.SuccessStatus(ownerResponse, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<List<GetAssosicationResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetAssosicationResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<List<GetUserInstituteInfoResponse>> GetUserInstitute(UserIDRequestUser userIDRequest)
        {
            try
            {
                AddInstituteDBService addInstituteDBService = new();
                var request = new UserIDRequestModel
                {
                    UserID= userIDRequest.UserID,
                };
                var result = addInstituteDBService.GetUserInstituteDetails(request).Result;
                if (result.Success)
                {
                    List<GetUserInstituteInfoResponse> ownerResponse = new();
                    foreach (var owner in result.Data)
                    {
                        ownerResponse.Add(new GetUserInstituteInfoResponse
                        {
                            InstitutionID=owner.InstitutionID,
                            InstitutionName=owner.InstitutionName,
                            YearOfEstablishment=owner.YearOfEstablishment,
                            CategoryID=owner.CategoryID,
                            CategoryName=owner.CategoryName,
                            LevelID=owner.LevelID,
                            LevelName=owner.LevelName,
                            CourseID=owner.CourseID,
                            CourseName=owner.CourseName,
                            ManagementID=owner.ManagementID,
                            ManagementName=owner.ManagementName,
                            AssociationID=owner.AssociationID,
                            AssociationName =owner.AssociationName,
                            HeadName=owner.HeadName,
                            Gender=owner.Gender,
                            JoiningDate=owner.JoiningDate,
                            PhoneNumber=owner.PhoneNumber,
                            EmailID=owner.EmailID,
                            OwnershipID=owner.OwnershipID,
                            OwnershipTypeName=owner.OwnershipTypeName,
                            GovernanceID=owner.GovernanceID,
                            AffiliationNumber=owner.AffiliationNumber,
                            GovernanceName=owner.GovernanceName,
                            FacilityID=owner.FacilityID,
                            FacilityName=owner.FacilityName,
                            InstitutionIDValue=owner.InstitutionIDValue,
                            AddressID=owner.AddressID,
                            AdditionalAddress=owner.AdditionalAddress,
                            LandMark=owner.LandMark,
                            UTLzipCodeID=owner.UTLzipCodeID,
                        });
                    }
                    return StatusResponse<List<GetUserInstituteInfoResponse>>.SuccessStatus(ownerResponse, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<List<GetUserInstituteInfoResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetUserInstituteInfoResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }


    }
}
