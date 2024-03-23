using Structure;
using System.Data.SqlClient;
using System.Data;
using Dto;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;
using DBService;


namespace Service
{
    public class UserDetailsService //: IUserLoginService
    {

        private readonly UserDetailsDBService _userDetailsDbService;
        public UserDetailsService(UserDetailsDBService userDetailsDbService) 
        {
            _userDetailsDbService = userDetailsDbService;
            
        }

        public async Task<UserDetails> GetUserDetails(string userId)
        {
            return await _userDetailsDbService.GetUserDetails(userId);
        }
    

        public string GetCountryIdByPhoneNumber(string phoneNumber)
        {
            return  _userDetailsDbService.GetCountryIdByPhoneNumber(phoneNumber);
        }
    }
}
