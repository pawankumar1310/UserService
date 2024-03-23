using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Structure;


namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernanceTypeController : ControllerBase
    {
        private readonly GovernanceTypeService _governanceTypeService;
        public GovernanceTypeController(IGovernanceType governance)
        {
            _governanceTypeService = new GovernanceTypeService(governance);
        }
       
        [HttpPost]
        public async Task<IActionResult> AddGovernanceType([FromBody] GovernanaceTypeModel model)
        {
            try
                {
                    int rowsAffected =await  _governanceTypeService.AddGovernanceService(model.Name,model.CreatedBy);

                    if (rowsAffected > 0)
                    {
                        return Ok("GovernanceType added successfully.");
                    }
                    else
                    {
                        return BadRequest("Failed to add GovernanceType.");
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
                List<ViewGovernanceTypeModel> governanceTypes = await _governanceTypeService.GetGovernanceService();

                return Ok(governanceTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGovernanceType(string governanceTypeID, [FromBody] ViewGovernanceTypeModel model)
        {
            try
            {
                int rowsAffected = await _governanceTypeService.GovernanceTypeUpdationService(governanceTypeID, model);

                if (rowsAffected > 0)
                {
                    return Ok($"GovernanceType with ID {governanceTypeID} updated successfully.");
                }
                else
                {
                    return BadRequest($"Failed to update GovernanceType with ID {governanceTypeID}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
   
        [HttpDelete]
        public async Task<IActionResult> DeleteGovernanceType(string governanceTypeID)
        {
            try
            {
                int rowsAffected =await  _governanceTypeService.GovernanceTypeDeletionService(governanceTypeID);

                if (rowsAffected > 0)
                {
                    return Ok($"GovernanceType with ID {governanceTypeID} deleted successfully.");
                }
                else
                {
                    return BadRequest($"Failed to delete GovernanceType with ID {governanceTypeID}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}

