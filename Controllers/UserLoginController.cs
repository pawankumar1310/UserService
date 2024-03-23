using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using Communication;
using DBService;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service;
namespace Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly UserLoginService _userService;

        private readonly UtilityServiceRequest _utilityService;
        private readonly CommunicationServiceRequest _communicationService;
        private readonly UserDetailsService _userDetailsService;

        public UserLoginController(UserLoginService userService, UserLoginDBService userDbService, UtilityServiceRequest utilityService, CommunicationServiceRequest communicationServiceRequest, UserDetailsService userDetailsService)
        {
            _userService = userService;
            _utilityService = utilityService;
            _communicationService = communicationServiceRequest;
            _userDetailsService = userDetailsService;

            
        }



        [HttpPost("checkUserStatus")]
        public async Task<IActionResult> CheckUserStatus([FromBody] LoginInitiationRequest loginInitiationRequest)
        {
            try
            {

                var loginStatus = await _userService.CheckUserExistence(loginInitiationRequest.UserInput);
                return Ok(loginStatus);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginInitiationRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.UserInput))
                {
                    return BadRequest(new { Message = "Invalid request. Email, phone number, or username is required." });
                }

                bool isEmail = IsEmail(request.UserInput);
                bool isPhoneNumber = IsPhoneNumber(request.UserInput);
                //string countryIdOfNonExistingUser = 

                if (isEmail)
                {

                    var userExist = await _userService.CheckUserExistence(request.UserInput);

                    if (userExist.UserNAmeStatus == true)
                    {
                            // If user does not exist by email or phone number, return an appropriate response
                        var utilityServiceResponse = await _utilityService.RequestOtpFromUtilityService();
                        if (!utilityServiceResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)utilityServiceResponse.StatusCode, "Error requesting OTP");
                        }

                        // Step 2: Extract OTP from UtilityService response
                        var Otp = await utilityServiceResponse.Content.ReadAsStringAsync();


                        // Step 3: Create model for CommunicationService
                        var communicationRequest = new CommunicationServiceModel
                        {
                            email = isEmail ? request.UserInput : null,
                            //phoneNumber = isPhoneNumber ? request.UserInput : null,
                            //countryID = countryIdOfExistingUser,
                            otp = int.Parse(Otp)
                        };

                        // Step 4: Send OTP to CommunicationService
                        var communicationServiceResponse = await _communicationService.SendOtpToCommunicationService(communicationRequest);
                        if (!communicationServiceResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)communicationServiceResponse.StatusCode, "Error sending OTP");
                        }

                        // Step 5: Process the response from CommunicationService (if needed)
                        var communicationServiceResult = await communicationServiceResponse.Content.ReadAsStringAsync();

                        // Return success response to the client
                        return Ok(request.UserInput);
                    }
                    else
                    {
                        
                        // Step 1: Request OTP from UtilityService
                        var utilityServiceResponse = await _utilityService.RequestOtpFromUtilityService();
                        if (!utilityServiceResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)utilityServiceResponse.StatusCode, "Error requesting OTP");
                        }

                        // Step 2: Extract OTP from UtilityService response
                        var Otp = await utilityServiceResponse.Content.ReadAsStringAsync();

                        // Step 3: Create model for CommunicationService
                        var communicationRequest = new CommunicationServiceModel
                        {
                            email = isEmail ? request.UserInput : null,
                            // phoneNumber = isPhoneNumber ? request.UserInput : null,
                            // countryID = request.countryID,
                            otp = int.Parse(Otp)
                        };

                        // Step 4: Send OTP to CommunicationService
                        var communicationServiceResponse = await _communicationService.SendOtpToCommunicationService(communicationRequest);
                        if (!communicationServiceResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)communicationServiceResponse.StatusCode, "Error sending OTP");
                        }

                        // Step 5: Process the response from CommunicationService (if needed)
                        var communicationServiceResult = await communicationServiceResponse.Content.ReadAsStringAsync();

                        // Return success response to the client
                        return Ok(request.UserInput);
                        //return Ok(new { Message = "OTP sent successfully", OtpStatus = communicationServiceResult });
                    }
                }
                else if(isPhoneNumber)
                {
                     var userExist = await _userService.CheckUserExistence(request.UserInput);

                    if (userExist.UserNAmeStatus == true)
                    {
                            // If user does not exist by email or phone number, return an appropriate response
                        var utilityServiceResponse = await _utilityService.RequestOtpFromUtilityService();
                        if (!utilityServiceResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)utilityServiceResponse.StatusCode, "Error requesting OTP");
                        }

                        // Step 2: Extract OTP from UtilityService response
                        var Otp = await utilityServiceResponse.Content.ReadAsStringAsync();

                        string countryIdOfExistingUser = _userDetailsService.GetCountryIdByPhoneNumber( request.UserInput);

                        // Step 3: Create model for CommunicationService
                        var communicationRequest = new CommunicationServiceModel
                        {
                            //email = isEmail ? request.UserInput : null,
                            phoneNumber = isPhoneNumber ? request.UserInput : null,
                            countryID = countryIdOfExistingUser,
                            otp = int.Parse(Otp)
                        };

                        // Step 4: Send OTP to CommunicationService
                        var communicationServiceResponse = await _communicationService.SendOtpToCommunicationService(communicationRequest);
                        if (!communicationServiceResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)communicationServiceResponse.StatusCode, "Error sending OTP");
                        }

                        // Step 5: Process the response from CommunicationService (if needed)
                        var communicationServiceResult = await communicationServiceResponse.Content.ReadAsStringAsync();

                        // Return success response to the client
                        return Ok(request.UserInput);
                    }
                    else
                    {
                        var utilityServiceResponse = await _utilityService.RequestOtpFromUtilityService();
                        if (!utilityServiceResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)utilityServiceResponse.StatusCode, "Error requesting OTP");
                        }

                        // Step 2: Extract OTP from UtilityService response
                        var Otp = await utilityServiceResponse.Content.ReadAsStringAsync();

                        // Step 3: Create model for CommunicationService
                        var communicationRequest = new CommunicationServiceModel
                        {
                            email = isEmail ? request.UserInput : null,
                            phoneNumber = isPhoneNumber ? request.UserInput : null,
                            countryID = request.countryID,
                            otp = int.Parse(Otp)
                        };

                        // Step 4: Send OTP to CommunicationService
                        var communicationServiceResponse = await _communicationService.SendOtpToCommunicationService(communicationRequest);
                        if (!communicationServiceResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)communicationServiceResponse.StatusCode, "Error sending OTP");
                        }

                        // Step 5: Process the response from CommunicationService (if needed)
                        var communicationServiceResult = await communicationServiceResponse.Content.ReadAsStringAsync();

                        // Return success response to the client
                        return Ok(request.UserInput);
                    }


                }
                else{
                    // Check user status by username
                    var userExist = await _userService.CheckUserExistence(request.UserInput);
                    if (!userExist?.UserNAmeStatus ?? true)
                    {
                        // If user does not exist by username, return an appropriate response
                        return BadRequest(new { Message = "User does not exist." });
                    }

                    // If the user exists by username, fetch contact information
                    var userContact = await _userService.GetUserContactByUsernameAsync(request.UserInput);
                    if (userContact != null && (!string.IsNullOrEmpty(userContact.Email) || !string.IsNullOrEmpty(userContact.PhoneNumber)))
                    {
                        // Proceed to send OTP using the fetched contact information
                        // Step 1: Request OTP from UtilityService
                        var utilityServiceResponse = await _utilityService.RequestOtpFromUtilityService();
                        if (!utilityServiceResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)utilityServiceResponse.StatusCode, "Error requesting OTP");
                        }

                        // Step 2: Extract OTP from UtilityService response
                        var Otp = await utilityServiceResponse.Content.ReadAsStringAsync(); 
                        //string countryIdOfExistingUser = _userDetailsService.GetCountryIdByPhoneNumber(isPhoneNumber ? request.UserInput : null);

                        // Step 3: Create model for CommunicationService
                        var communicationRequest = new CommunicationServiceModel
                        {
                            email = !string.IsNullOrEmpty(userContact.Email) ? userContact.Email : null,
                            phoneNumber = !string.IsNullOrEmpty(userContact.PhoneNumber) ? userContact.PhoneNumber : null,
                            //countryID = countryIdOfExistingUser,
                            otp = int.Parse(Otp)
                        };

                        // Step 4: Send OTP to CommunicationService
                        var communicationServiceResponse = await _communicationService.SendOtpToCommunicationService(communicationRequest);
                        if (!communicationServiceResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)communicationServiceResponse.StatusCode, "Error sending OTP");
                        }

                        // Step 5: Process the response from CommunicationService (if needed)
                        var communicationServiceResult = await communicationServiceResponse.Content.ReadAsStringAsync();

                        // Return success response to the client
                        return Ok(userContact.Email);
                        //return Ok(new { Message = $"OTP sent successfully to {userContact.Email}", OtpStatus = communicationServiceResult });
                    }
                    else
                    {
                        // Handle the case where userContact is null or email/phone is empty
                        return BadRequest(new { Message = "Email or Phone Number does not exist." });
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }   

        private bool IsEmail(string input)
        {
             return new EmailAddressAttribute().IsValid(input);
        }

        private bool IsPhoneNumber(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(char.IsDigit);
        }

        private string GetCountryIdFromRequest()
        {
            if (HttpContext.Request.Query.TryGetValue("countryId", out var countryIdValue))
            {
                return countryIdValue;
            }
            return null; 
        }





        [HttpPost("loginWithPassword")]
        public async Task<IActionResult> LoginWithPassword([FromBody] PasswordValidationRequest loginRequest)
        {
            try
            {
                var userExist = await _userService.CheckUserExistence(loginRequest.UserInput);

                if (userExist != null && userExist.UserNAmeStatus)
                {
                // Assuming loginRequest.UserInput is the username/email/phone and loginRequest.Password is the entered password
                bool isPasswordValid = await _userService.ValidateUserPassword(loginRequest.UserInput, loginRequest.Password);

                if (isPasswordValid)
                {
                    return Ok(new { Success = true, Message = "Login successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Login Failed.." });
                }
                }else{
                    return BadRequest(new { Success = false, Message = "Invalid username/email/phone or password" });
                }

            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, new { Success = false, Message = "Internal Server Error" });
            }
        }

        [HttpGet("GetUserIdByEmail/{emailAddress}")]
        public async Task<IActionResult> GetUserIdByEmail(string emailAddress)
        {
            try
            {
                string userId = await _userService.GetUserIDFromEmailAsync(emailAddress);

                if (string.IsNullOrEmpty(userId))
                {
                    return NotFound($"No user found for email address: {emailAddress}");
                }

                return Ok(new { UserID = userId }); 
            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }

}






