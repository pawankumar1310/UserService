using DTO.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Controller
{
    [Route("[controller]")]
    [ApiController]
    public class AddInstituteController : ControllerBase
    {
        [HttpPost("RegisterInstitute")]
        public IActionResult RegisterInstitute(AddInstituteRequest addInstituteRequest)
        {
            if (!string.IsNullOrEmpty(addInstituteRequest.Name) || !string.IsNullOrEmpty(addInstituteRequest.AffiliationID) || !string.IsNullOrEmpty(addInstituteRequest.YearOfEstablishment))
            {
                try
                {
                    AddInstituteService addinstituteService = new();
                    return Ok(addinstituteService.RegisterInstitute(addInstituteRequest));
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("AddInstituteAddress")]
        public IActionResult AddInstituteAddress(InstituteAddressRequest addInstituteRequest)
        {
            if (!string.IsNullOrEmpty(addInstituteRequest.Landmark) || !string.IsNullOrEmpty(addInstituteRequest.AdditionalAddress) || !string.IsNullOrEmpty(addInstituteRequest.ZipCodeID) || !string.IsNullOrEmpty(addInstituteRequest.StatusReferenceID) || !string.IsNullOrEmpty(addInstituteRequest.UserID))
            {
                try
                {
                    AddInstituteService addinstituteService = new();
                    return Ok(addinstituteService.AddInstituteAddress(addInstituteRequest));
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("GetOwnership")]
        public IActionResult GetOwnership()
        {
            try
            {
                AddInstituteService addinstituteService = new();
                return Ok(addinstituteService.GetOwnership());
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost("AddInstituteOwnership")]
        public IActionResult AddInstituteOwnership(AddInstituteOwnershipRequest addInstituteOwner)
        {
            if (!string.IsNullOrEmpty(addInstituteOwner.institutionID) || !string.IsNullOrEmpty(addInstituteOwner.ownerShipID) || !string.IsNullOrEmpty(addInstituteOwner.createdBy))
            {
                try
                {
                    AddInstituteService addinstituteService = new();
                    return Ok(addinstituteService.AddInstituteOwnerShip(addInstituteOwner));
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("GetGovernance")]
        public IActionResult GetGovernance()
        {
            try
            {
                AddInstituteService addinstituteService = new();
                return Ok(addinstituteService.GetGovernance());
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("GetFacilitie")]
        public IActionResult GetFacilitie()
        {
            try
            {
                AddInstituteService addinstituteService = new();
                return Ok(addinstituteService.GetFacility());
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost("AddInstituteHead")]
        public IActionResult AddInstituteHead(InstituteHeadRequest addInstituteHead)
        {
            if (!string.IsNullOrEmpty(addInstituteHead.InstitutionId) || !string.IsNullOrEmpty(addInstituteHead.Email) || !string.IsNullOrEmpty(addInstituteHead.Names) || !string.IsNullOrEmpty(addInstituteHead.Gender))
            {
                try
                {
                    AddInstituteService addinstituteService = new();
                    return Ok(addinstituteService.AddInstituteHead(addInstituteHead));
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("AddUserInstitute")]
        public IActionResult AddUserInstitute(AddUserInstituteRequest addInstituteUser)
        {
            if (!string.IsNullOrEmpty(addInstituteUser.instituteid) || !string.IsNullOrEmpty(addInstituteUser.userid))
            {
                try
                {
                    AddInstituteService addinstituteService = new();
                    return Ok(addinstituteService.AddUserInstitute(addInstituteUser));
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("AddInstituteCode")]
        public IActionResult AddInstituteCode(InstituteCodeForLyceeEGOVRequest addCode)
        {
            if (!string.IsNullOrEmpty(addCode.instituteid) || !string.IsNullOrEmpty(addCode.value))
            {
                try
                {
                    AddInstituteService addinstituteService = new();
                    return Ok(addinstituteService.AddInstituteCodeForLyceeEGov(addCode));
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("GetCategories")]
        public IActionResult GetCategories()
        {
            try
            {
                AddInstituteService addinstituteService = new();
                return Ok(addinstituteService.GetCategories());
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("GetLevel")]
        public IActionResult GetLevel()
        {
            try
            {
                AddInstituteService addinstituteService = new();
                return Ok(addinstituteService.GetLevel());
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("GetCourses")]
        public IActionResult GetCourses()
        {
            try
            {
                AddInstituteService addinstituteService = new();
                return Ok(addinstituteService.GetCourses());
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("GetManagement")]
        public IActionResult GetManagement()
        {
            try
            {
                AddInstituteService addinstituteService = new();
                return Ok(addinstituteService.GetManagement());
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("GetAssociation")]
        public IActionResult GetAssociation()
        {
            try
            {
                AddInstituteService addinstituteService = new();
                return Ok(addinstituteService.GetAssociation());
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost("GetUserInstitution")]
        public IActionResult GetUserInstitution(UserIDRequestUser userIDRequest)
        {
            if (!string.IsNullOrEmpty(userIDRequest.UserID))
            {
                try
                {
                    AddInstituteService addinstituteService = new();
                    return Ok(addinstituteService.GetUserInstitute(userIDRequest));
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }




    }
}
