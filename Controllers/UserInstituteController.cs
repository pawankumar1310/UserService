using Dto;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Structure;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInstituteController : ControllerBase
    {
        private readonly UserInstituteService _registerUserInstituteService;
        public UserInstituteController(IUserInstitution institute)
        {
            _registerUserInstituteService = new UserInstituteService(institute);
        }
        [HttpPost]
        public async Task<IActionResult> InsertUserInstitution([FromBody] UserInstitutionModel model)
        {
            try
            {
                int result=await _registerUserInstituteService.RegisterUserInstitute(model);
                if(result!=0)
                {
                    return Ok("UserInstitution record inserted successfully.");
                }
                else
                {
                    return BadRequest("Invalid details");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Error inserting UserInstitution record: {ex.Message}");
            }
        }
        [HttpGet("getByUserID")]
        public async Task<IActionResult> GetInstitutionsByUserID(string userID)
        {
            try
            {
                List<UserInstituteModel> institutions = await _registerUserInstituteService.GetUserInstitute(userID);
                return Ok(institutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
