using Dto;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDetailsService _userDetailsService;

        public UserController(UserDetailsService userDetailsService)
        {
            _userDetailsService = userDetailsService;
        }

        [HttpGet("GetUserDetails/{userId}")]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            try
            {
                var userDetails = await _userDetailsService.GetUserDetails(userId);

                if (userDetails != null)
                {
                    return Ok(userDetails);
                }
                else
                {
                    return NotFound(); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetCountryIdByPhoneNumber")]
        public IActionResult GetCountryIdByPhoneNumber([FromQuery] string phoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phoneNumber))
                {
                    return BadRequest(new { Message = "Phone number is required." });
                }

                var countryId =  _userDetailsService.GetCountryIdByPhoneNumber(phoneNumber);

                if (!string.IsNullOrEmpty(countryId))
                {
                    return Ok(new { CountryId = countryId });
                }
                else
                {
                    return NotFound(new { Message = "CountryId not found for the provided phone number." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }

}
