using Microsoft.AspNetCore.Mvc;
using Service;
using DTO.UserService;

namespace Controller
{
    [Route("[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {

        [HttpPost("checkUserStatus")]
        public IActionResult CheckUserStatus([FromBody] UserStatusRequest userStatusRequest)
        {
            try
            {
                UserLoginService userLoginService = new();
                return Ok(userLoginService.CheckUserExistence(userStatusRequest));
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpPost("GetUserIdByEmail")]
        public IActionResult GetUserIdByEmail([FromBody] GetUserIdByEmailRequest getUserIdByEmailRequest)
        {
            try
            {
                UserLoginService userLoginService = new();
                return Ok(userLoginService.GetUserIdByEmail(getUserIdByEmailRequest));
            }
            catch
            {
                return StatusCode(500);
            }
        }



    }
}
