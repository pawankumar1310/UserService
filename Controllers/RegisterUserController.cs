using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using Service;
using DBService;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {

        private readonly UserRegistrationService _service;
        private readonly UserLoginService _userService;
        public RegisterUserController(UserRegistrationService service, UserLoginService loginService)
        {
            _service = service;
            _userService = loginService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUserRequest)
        {
            try
            {
                var EmailExists = await _userService.CheckUserExistence(registerUserRequest.EmailAddress);
                var PhoneExists = await _userService.CheckUserExistence(registerUserRequest.PhoneNumber);
                if (!EmailExists.UserNAmeStatus && !PhoneExists.UserNAmeStatus)
                {
                    // Capture the user ID returned from the RegisterUser method
                    Guid? userID = await _service.RegisterUserAsync(
                        registerUserRequest.Name,
                        registerUserRequest.CountryID,
                        registerUserRequest.PhoneNumber,
                        registerUserRequest.EmailAddress,
                        registerUserRequest.ZipcodeID
                    );

                    if (userID != null)
                    {
                        // Return the user ID in the response
                        return Ok(userID);
                    }
                    else
                    {
                        return Ok("Unable to register User");
                    }
                }
                else
                {
                    return Ok("User already exists. Login to Continue");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{userID}")]
        public async Task<IActionResult> UpdateUser(string userID, [FromBody] RegisterUser updateUserModel)
        {
            try
            {
                // Validate the input if needed

                // Call the service to update the user
                bool result = await _service.UpdateUser(
                    userID,
                    updateUserModel.Name,
                    updateUserModel.CountryID,
                    updateUserModel.PhoneNumber,
                    updateUserModel.EmailAddress,
                    updateUserModel.ZipcodeID
                );

                if (result)
                {
                    return Ok("User updated successfully");
                }
                else
                {
                    return BadRequest("Failed to update user");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{userID}")]
        public async Task<IActionResult> GetUserWithUrls(string userID)
        {
            try
            {
                // Call the service to get the user with associated URLs
                var userWithUrls = await _service.GetUserWithUrls(userID);

                if (userWithUrls != null)
                {
                    return Ok(userWithUrls);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("AddAddtionalAddress")]
        public async Task<IActionResult> InsertAddtionalAddress(string userId, string address)
        {
            try
            {
                var result = _service.InsertAddtionalAddressIntoUser(userId, address);
                return Ok("address inserted successfully");
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}");
            }
        }


        [HttpGet("additionaladdress/{userId}")]
        public async Task<IActionResult> GetUserAdditionalAddress(string userId)
        {
            try
            {
                string additionalAddress = await _service.GetUsersAddtionalAddress(userId);

                if (additionalAddress != null)
                {
                    return Ok(additionalAddress);
                }
                else
                {
                    return NotFound($"User with ID {userId} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("UpdateUserWithUrl/{UserID}")]
        public async Task<IActionResult> UpdateUserWithUrls(string UserID, UserWithUrl updateUser)
        {
            try
            {
                // Call the method from the repository
                var result = await _service.UpdateUserWithUrls(UserID, updateUser);

                if (result)
                {
                    // Success
                    return Ok("User updated successfully!");
                }
                else
                {
                    // Error handling logic if needed
                    return BadRequest("Failed to update user.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or return a relevant error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
