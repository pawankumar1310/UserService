using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service;
using Structure;
using System.Data.SqlClient;
using System.Data;
using UserService.Service;
using DBService;


namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly UrlService _urlService;
        public UrlController(IUrl url)
        {
            _urlService = new UrlService(url);
        }
        [HttpPost]
        public async Task<IActionResult> InsertUrl([FromBody] UrlModel model)
        {
            try
            {
                int result = await _urlService.AddUrlService(model);
                if (result > 0)
                {
                    return Ok("Url record inserted successfully.");
                }
                else
                {
                    return BadRequest($"Error inserting Url record:");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error inserting Url record:{ex.Message}");
            }
        }


        [HttpPut("{urlID}")]
        public async Task<IActionResult> UpdateUrl(string url, [FromBody] UrlModel updateUrlModel)
        {
            //try
            //{
            bool result = await _urlService.UpdateUrl(url, updateUrlModel);

            //if (result)
            //{
            return Ok("URL updated successfully");
            //    }
            //    else
            //    {
            //        return BadRequest("Failed to update URL");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception or handle it as needed
            //    return StatusCode(500, "Internal server error");
            //}
        }


        //[HttpPut("Update/{userid}")]
        //public IActionResult UpdateUserAndUrl(string userid, UserWithUrl userWithUrl)
        //{
        //    try
        //    {
        //        _urlService.UpdateUserWithUrl(userid, userWithUrl.Name, userWithUrl.CountryID, userWithUrl.PhoneNumber, userWithUrl.EmailAddress, userWithUrl.ZipCodeID, userWithUrl.Url, userWithUrl.Label);
        //        return Ok(new { Success = true, Message = "User and Url updated successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        Console.WriteLine($"Error: {ex.Message}");

        //        return StatusCode(500, new { Success = false, Message = "Internal server error." });
        //    }
        //}
    } 
}


