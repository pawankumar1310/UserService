using Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service;

namespace Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlaceInformationController : ControllerBase
    {

        private readonly UtilityServiceRequest _utilityServiceRequest;

        public PlaceInformationController(UtilityServiceRequest utilityServiceRequest)
        {
            _utilityServiceRequest = utilityServiceRequest;
        }

        [HttpGet("GetAddressByZipID/{zipCodeID}")]
        public async Task<IActionResult> GetAddressByZipID(string zipCodeID)
        {
            try
            {
                var response = await _utilityServiceRequest.GetAdddressByZipID(zipCodeID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}