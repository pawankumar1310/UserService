using Model.UserService;
using DTO.UserService;
using Package;
using DBService;

namespace Service
{
    public class UserRegistrationService
    {

        public  StatusResponse<string?> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            var registerUserModelRequest = new RegisterUserModelRequest
            {
                FirstName = registerUserRequest.FirstName,
                LastName = registerUserRequest.LastName,
                CountryID = registerUserRequest.CountryID,
                PhoneNumber = registerUserRequest.PhoneNumber,
                EmailAddress = registerUserRequest.EmailAddress,
                AdditionalAddress = registerUserRequest.AdditionalAddress,
                ZipcodeID = registerUserRequest.ZipcodeID   
            };

            try
            {
                UserRegistrationDBService userRegistrationDBService = new();
                var result = userRegistrationDBService.RegisterUser(registerUserModelRequest).Result;
                

                if (result.Success)
                {
                    return StatusResponse<string?>.SuccessStatus(result.Data, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<string?>.FailureStatus(result.StatusCode, new Exception());
                }

            }

            catch (Exception ex)
            {
                return StatusResponse<string?>.FailureStatus(StatusCode.knownException, ex);
            }
        }


        public StatusResponse<int> UpdateUserWithUrl(UpdateUserWithUrlRequest updateUserWithUrlRequest)
        {

            List<UpdateUrlModelRequest> userUrlModels = updateUserWithUrlRequest.UserUrls?.Select(url => new UpdateUrlModelRequest
            {
                Url = url.Url,
                UrlLabelId = url.UrlLabelId
            }).ToList() ?? new List<UpdateUrlModelRequest>();



            UpdateUserWithUrlModelRequest updateUserWithUrlModelRequest = new UpdateUserWithUrlModelRequest
            {
                UserID = updateUserWithUrlRequest.UserID,
                FirstName = updateUserWithUrlRequest.FirstName,
                LastName = updateUserWithUrlRequest.LastName,
                CountryID = updateUserWithUrlRequest.CountryID,
                PhoneNumber = updateUserWithUrlRequest.PhoneNumber,
                EmailAddress = updateUserWithUrlRequest.EmailAddress,
                AdditionalAddress = updateUserWithUrlRequest.AdditionalAddress,
                ZipCodeID = updateUserWithUrlRequest.ZipCodeID,
                UserUrls = userUrlModels
            };

            try
            {
                UserRegistrationDBService userRegistrationDBService = new();
                var result = userRegistrationDBService.UpdateUserWithUrls(updateUserWithUrlModelRequest).Result;

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

        public StatusResponse<UserWithUrlResponse> GetUserWithUrls(UserWithUrlRequest userWithUrlRequest)
        {
            var userWithUrlModelRequest = new UserWithUrlModelRequest { UserID = userWithUrlRequest.UserID };
           

            try
            {
                UserRegistrationDBService userRegistrationDBService = new();
                var result = userRegistrationDBService.GetUserWithUrls(userWithUrlModelRequest).Result;

                if(result.Success)
                {
                    var userWithUrlResponse = new UserWithUrlResponse()
                    {
                        UserID = result.Data.UserID,
                        FirstName = result.Data.FirstName,
                        LastName = result.Data.LastName,
                        AdditionalAddress = result.Data.AdditionalAddress,
                        UTLzipcodeID = result.Data.UTLzipcodeID,
                        PhoneNumber = result.Data.PhoneNumber,
                        EmailAddress = result.Data.EmailAddress,
                        UTLcountryID = result.Data.UTLcountryID,
                        UserUrls = result.Data.UserUrls.Select(url => new DTO.UserService.UrlResponse
                        {
                            UrlID = url.UrlID,
                            UTLiTableID = url.UTLiTableID,
                            Url = url.Url,
                            UrlLabelId = url.UrlLabelId,
                            ReferenceID = url.ReferenceID
                        }).ToList()
                    };
                    return StatusResponse<UserWithUrlResponse>.SuccessStatus(userWithUrlResponse, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<UserWithUrlResponse>.FailureStatus(result.StatusCode, new Exception());
                }

            }
            catch (Exception ex)
            {
                return StatusResponse<UserWithUrlResponse>.FailureStatus(StatusCode.knownException, ex);
            }
        }


        public StatusResponse<int> UpdateUserAdditionalAddress(UpdateUserAdditionalAddressRequest updateUserAdditionalAddressRequest)
        {
            var updateUserAdditionalAddressModelRequest = new UpdateUserAdditionalAddressModelRequest 
            { 
                UserID = updateUserAdditionalAddressRequest.UserID,
                AdditionalAddress = updateUserAdditionalAddressRequest.AdditionalAddress
            };

            try
            {
                UserRegistrationDBService userRegistrationDBService = new();
                var result = userRegistrationDBService.UpdateUserAdditionalAddress(updateUserAdditionalAddressModelRequest).Result;

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


        public StatusResponse<GetUsersAddtionalAddressResponse> GetUsersAddtionalAddress(GetUsersAddtionalAddressRequest getUsersAddtionalAddressRequest)
        {
            var getUsersAddtionalAddressModelRequest = new GetUsersAddtionalAddressModelRequest
            {
                UserID = getUsersAddtionalAddressRequest.UserID
            };

            try
            {
                UserRegistrationDBService userRegistrationDBService = new();
                var result = userRegistrationDBService.GetUsersAddtionalAddress(getUsersAddtionalAddressModelRequest).Result;
                var getUsersAddtionalAddressResponse = new GetUsersAddtionalAddressResponse()
                {
                    AdditionalAddress = result.Data.AdditionalAddress
                };

                if (result.Success)
                {
                    return StatusResponse<GetUsersAddtionalAddressResponse>.SuccessStatus(getUsersAddtionalAddressResponse, StatusCode.Success);

                }
                else
                {
                    return StatusResponse<GetUsersAddtionalAddressResponse>.FailureStatus(result.StatusCode, new Exception());
                }

            }
            catch (Exception ex)
            {
                return StatusResponse<GetUsersAddtionalAddressResponse>.FailureStatus(StatusCode.knownException, ex);
            }

        }

    }

}
