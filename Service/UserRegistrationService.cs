using Structure;
using System.Data.SqlClient;
using System.Data;
using Dto;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;


namespace DBService
{
    public class UserRegistrationService
    {

        private readonly UserRegistrationDBService _userRegistrationDBService;

        public UserRegistrationService(UserRegistrationDBService userRegistrationDBService)
        {
            _userRegistrationDBService = userRegistrationDBService;

        }


        public async Task<Guid?> RegisterUserAsync(string name, string countryID, string phoneNumber, string emailAddress, string zipCodeID)
        {
            return await _userRegistrationDBService.RegisterUser(name, countryID, phoneNumber, emailAddress, zipCodeID);
        }


        public async Task<bool> UpdateUser(string userID, string name, string countryID, string phoneNumber, string emailAddress, string zipCodeID)
        {
            return await _userRegistrationDBService.UpdateUser(userID, name, countryID, phoneNumber, emailAddress, zipCodeID);
        }

        public async Task<UserWithUrlsModel> GetUserWithUrls(string userID)
        {
            return await _userRegistrationDBService.GetUserWithUrls(userID);
        }

        public async Task InsertAddtionalAddressIntoUser(string userId, string additionalAddress)
        {
            await _userRegistrationDBService.UpdateUserAdditionalAddress(userId, additionalAddress);
        }


        public async Task<string> GetUsersAddtionalAddress(string userId)
        {
            return await _userRegistrationDBService.GetUsersAddtionalAddress(userId);
        }

        public async Task<bool> UpdateUserWithUrls(string UserID, UserWithUrl updateUser)
        {
            return await _userRegistrationDBService.UpdateUserWithUrls(UserID, updateUser);
        }
    }
}

