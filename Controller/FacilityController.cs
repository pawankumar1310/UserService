using DTO.UserService;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Controller
{
    [Route("[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {

        [HttpPost("CreateFacility")]
        public IActionResult CreateFacility(CreateFacilityRequest createFacilityRequest)
        {
            try
            {
                var facilityService = new FacilityService();
                return Ok(facilityService.CreateFacility(createFacilityRequest));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
