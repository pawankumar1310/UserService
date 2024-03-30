using DTO.UserService;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Controller
{
    [Route("[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        [HttpPost("RegisterUser")]

        public IActionResult RegisterUser(RegisterUserRequest registerUserRequest)
        {
            try
            {
                UserRegistrationService userRegistrationService = new();
                return Ok(userRegistrationService.RegisterUser(registerUserRequest));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("UpdateUserWithUrls")]

        public IActionResult UpdateUserWithUrls(UpdateUserWithUrlRequest updateUserWithUrlRequest)
        {
            try
            {
                UserRegistrationService userRegistrationService = new();
                return Ok(userRegistrationService.UpdateUserWithUrl(updateUserWithUrlRequest));
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPost("GetUserWithUrls")]
        public IActionResult GetUserWithUrls(UserWithUrlRequest userWithUrlRequest)
        {
            try
            {
                UserRegistrationService userRegistrationService = new();
                return Ok(userRegistrationService.GetUserWithUrls(userWithUrlRequest));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("UpdateUserAdditionalAddress")]
        public IActionResult UpdateUserAdditionalAddress(UpdateUserAdditionalAddressRequest updateUserAdditionalAddressRequest)
        {
            try
            {
                UserRegistrationService userRegistrationService = new();
                return Ok(userRegistrationService.UpdateUserAdditionalAddress(updateUserAdditionalAddressRequest));
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPost("GetUsersAddtionalAddress")]
        public IActionResult GetUsersAddtionalAddress(GetUsersAddtionalAddressRequest getUsersAddtionalAddressRequest)
        {
            try
            {
                UserRegistrationService userRegistrationService = new();
                return Ok(userRegistrationService.GetUsersAddtionalAddress(getUsersAddtionalAddressRequest));
            }
            catch
            {
                return StatusCode(500);
            }
        }


    }
}