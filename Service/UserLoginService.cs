using DBService;
using Dto;


namespace Service{
    public class UserLoginService 
    {
        private readonly UserLoginDBService _userLoginService;

        public UserLoginService(UserLoginDBService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        public async Task<bool> CheckEmailOrPhoneExistenceAsync(string emailOrPhone)
        {
            return await _userLoginService.CheckEmailOrPhoneExistenceAsync(emailOrPhone);
        }

        public async Task<UserStatus> CheckUserExistence(string userInput)
        {
            return await _userLoginService.CheckUserExistence(userInput);
        }

        public async Task<bool> ValidateUserPassword(string userInput, string enteredPassword)
        {
            try
            {
                int validationStatus = await _userLoginService.ValidatePassword(userInput, enteredPassword);

                // If the validation status is 1, the password is valid; otherwise, it's invalid
                return validationStatus == 1;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


            public async Task<UserContact> GetUserContactByUsernameAsync(string username)
            {
                return await _userLoginService.GetUserContactByUsername(username);
            }
        public async Task<String> GetUserIDFromEmailAsync(string email)
        {
            string result=await _userLoginService.GetUserIdByEmail(email);
            return result;
        }

    }
}
