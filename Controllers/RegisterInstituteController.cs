using Dto;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service;
using Structure;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterInstituteController:ControllerBase
    {
        private readonly RegisterInstituteService _registerInstituteService;
        public RegisterInstituteController(IInstitute institute)
        {
            _registerInstituteService=new RegisterInstituteService(institute);
        }

        [HttpPost]
        public async Task<IActionResult> AddInstitution([FromBody] AddInstituteModel model)
        {
            try
            {
                
                int rowsAffected =await  _registerInstituteService.InstituteRegistrationService(model);

                if (rowsAffected > 0)
                {
                    return Ok($" {OutputModel.InstitutionID} ");
                }
                else
                {
                    return BadRequest("Failed to add institution.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInstitutions()
        {
            try
            {
                List<GetInstitution> institutions = await _registerInstituteService.GetInstituteService();
                return Ok(institutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetInstitutionById/{institutionId}")]
        public async Task<IActionResult> GetInstitutionById(string institutionId)
        {
            try
            {
                var institution = await _registerInstituteService.GetInstitutionById(institutionId);
                return Ok(institution);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateInstitution/{institutionID}")]
        public async Task<IActionResult> UpdateInstitution(string institutionID, [FromBody] UpdateInstitutionModel model)
        {
            try
            {
                int rowsAffected = await _registerInstituteService.InstituteUpdationService(institutionID, model);

                if (rowsAffected > 0)
                {
                    return Ok($"Institution with ID  updated successfully.");
                }
                else
                {
                    return BadRequest($"Failed to update institution with ID {institutionID}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("DeleteInstitution/{institutionID}")]
        public async Task<IActionResult> DeleteInstitution(string institutionID)
        {
             try
             {
                 int rowsAffected =await _registerInstituteService.InstituteDeletionService(institutionID);

                 if (rowsAffected > 0)
                 {
                    return Ok($"Institution with ID {institutionID} deleted successfully.");
                 }
                 else
                 {
                     return BadRequest($"Failed to delete institution with ID {institutionID}.");
                 }
             }
             catch (Exception ex)
             {
                 return StatusCode(500, $"Internal server error: {ex.Message}");
             }
        }


        [HttpGet("GetUserIDbyEmailOrPhone/{emailOrPhone}")]
        public async Task<IActionResult> GetUserIDbyEmailOrPhone(string emailOrPhone)
        {
            try
            {
                var userId = await _registerInstituteService.GetUserIdByEmailOrPhoneNumber(emailOrPhone);

                if(userId != null)
                {
                    return Ok(userId);
                }
                else
                {
                    return BadRequest("No userId Found");
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e.Message}");
            }
        }


    }
}