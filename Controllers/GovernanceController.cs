using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Structure;


namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernanceController : ControllerBase
    {
        private readonly GovernanceService _governanceService;
        public GovernanceController(IGovernance governance)
        {
            _governanceService = new GovernanceService(governance);
        }

        [HttpPost]
        public async Task<IActionResult> AddGovernance([FromBody] GovernanceModel model)
        {
            try
            {
                int rowsAffected = await _governanceService.AddGovernanceService(model);

                if (rowsAffected > 0)
                {
                    return Ok("Governance added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add Governance.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetGovernanceTypes()
        {
            try
            {
                List<GetGovernanceModel> governanceTypes = await _governanceService.GetGovernanceService();

                return Ok(governanceTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGovernance(string governanceID, [FromBody] UpdateGovernanceModel model)
        {
            try
            {
                int rowsAffected = await _governanceService.GovernanceUpdationService(governanceID,model);

                if (rowsAffected > 0)
                {
                    return Ok($"GovernanceType with ID {governanceID} updated successfully.");
                }
                else
                {
                    return BadRequest($"Failed to update GovernanceType with ID {governanceID}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGovernance(string governanceID)
        {
            try
            {
                int rowsAffected = await _governanceService.GovernanceDeletionService(governanceID);

                if (rowsAffected > 0)
                {
                    return Ok($"GovernanceType with ID {governanceID} deleted successfully.");
                }
                else
                {
                    return BadRequest($"Failed to delete GovernanceType with ID {governanceID}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}

