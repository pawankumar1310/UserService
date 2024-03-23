using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Structure;
using System.Data.SqlClient;
using UserService.Service;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproveInstitutionController : ControllerBase
    {
        private readonly StatusService _statusService;
        public ApproveInstitutionController(IStatus status)
        {
            _statusService = new StatusService(status);
        }

        [HttpPost("approveStatus")]
        public async Task<IActionResult> UpdateInstitutionStatus([FromBody] ApproveStatusModel status)
        {
            try
            {
                int result = await _statusService.approveInstituteService(status.IInstitutionStatusName, status.InstitutionID);
                if (result>0) 
                {
                 return Ok("Institution status updated successfully.");
                }
                else
                {
                    return BadRequest("Not approveed");
                }
               
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating institution status: {ex.Message}");
            }
        }

    }
}
