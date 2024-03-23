using Dto;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class FacilityController : ControllerBase
    {
        private readonly FacilityService _facilityService;

        public FacilityController(FacilityService facilityService)
        {
            _facilityService = facilityService;
        }



        [HttpPost("CreateFacility")]
        public async Task<IActionResult> CreateFacility([FromBody] CreateFacilityRequest request)
        {
            try
            {
                var FacilityId = await _facilityService.CreateFacility(request);
                return Ok("Facility Created Successfully..");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet("GetFacilityById/{FacilityId}")]
        public async Task<IActionResult> GetFacilityById(string FacilityId)
        {
            try
            {
                var Facility = await _facilityService.GetFacilityById(FacilityId);
                return Ok(Facility);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllFacilitys()
        {
            try
            {
                var facilities = await _facilityService.GetAllFacilities();
                return Ok(facilities);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



        [HttpPut("UpdateFacility/{FacilityId}")]
        public async Task<IActionResult> UpdateFacility(string FacilityId, [FromBody] UpdateFacilityRequest request)
        {
            try
            {
                await _facilityService.UpdateFacility(FacilityId, request);
                return Ok("Facility Updated Successfully..");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



        [HttpDelete("DeleteFacility/{FacilityId}")]
        public async Task<IActionResult> DeleteFacility(string FacilityId)
        {
            try
            {
                await _facilityService.DeleteFacility(FacilityId);
                return Ok("Facility Deleted Successfully..");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}

