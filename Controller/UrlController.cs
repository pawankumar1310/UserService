using DBService;
using DTO.UserService;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Controller
{
   [Route("[controller]")]
   [ApiController]
   public class UrlController : ControllerBase
   {

        [HttpPost("InsertUrl")]
        public IActionResult InsertUrl(AddUrlRequest addUrlRequest)
        {
            try
            {
                UrlService urlService = new();
                return Ok(urlService.InsertUrl(addUrlRequest));
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpGet("GetAllUrlLabels")]
        public IActionResult GetAllUrlLabels()
        {
            try
            {
                UrlService urlService = new();
                return Ok(urlService.GetAllUrlLabels());
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
