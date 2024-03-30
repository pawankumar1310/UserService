using DBService;
using Model.UserService;
using DTO.UserService;
using Package;

namespace Service
{
    public class UserLoginService
    {
        public StatusResponse<UserStatusResponse> CheckUserExistence(UserStatusRequest userStatusRequest)
        {
            var userStatusModelRequest = new UserStatusModelRequest { UserIdentity = userStatusRequest.UserIdentity};
            try
            {
                UserLoginDBService userLoginDBService = new();
                var result = userLoginDBService.CheckUserExistence(userStatusModelRequest).Result;
                if (result.Success)
                {
                    var loginStatus = new UserStatusResponse()
                    {
                        UserNAmeStatus = result.Data.UserNAmeStatus,
                        IsOtp = result.Data.IsOtp,
                        IsOtpAvailable = result.Data.IsOtpAvailable,
                        IsPasswordAvailable = result.Data.IsPasswordAvailable
                    };

               
                    return StatusResponse<UserStatusResponse>.SuccessStatus(loginStatus, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<UserStatusResponse>.FailureStatus(result.StatusCode, new Exception());

                }
            } 
            catch (Exception ex)
            {
                return StatusResponse<UserStatusResponse>.FailureStatus(StatusCode.knownException, ex);
            }
        }


        public StatusResponse<GetUserIdByEmailResponse> GetUserIdByEmail(GetUserIdByEmailRequest getUserIdByEmailRequest)
        {
            var getUserIdByEmailModelRequest = new GetUserIdByEmailModelRequest { UserEmail = getUserIdByEmailRequest.UserEmail };
            try
            {
                UserLoginDBService userLoginDBService = new();
                var result = userLoginDBService.GetUserIdByEmail(getUserIdByEmailModelRequest).Result;
                if (result.Success)
                {
                    var getUserIdByEmailStatus = new GetUserIdByEmailResponse()
                    {
                        UserId = result.Data.UserId
                    };


                    return StatusResponse<GetUserIdByEmailResponse>.SuccessStatus(getUserIdByEmailStatus, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<GetUserIdByEmailResponse>.FailureStatus(result.StatusCode, new Exception());

                }
            }
            catch (Exception ex)
            {
                return StatusResponse<GetUserIdByEmailResponse>.FailureStatus(StatusCode.knownException, ex);
            }
        }
    }
}
