using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Structure;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituteFilterController : ControllerBase
    {
        private readonly FilterInstituteData _filterInstituteData;
        public InstituteFilterController(IInstituteFilters instituteFilter)
        {
            _filterInstituteData = new FilterInstituteData(instituteFilter);
        }
        [HttpGet]
        public async Task<IActionResult> GetInstitutionsByStatus(string status)
        {
            try
            {
                List<ViewInstitutionModel> institutions =await  _filterInstituteData.FilterInstituteService(status);

                return Ok(institutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("DateFilter")]
        public async Task<IActionResult> GetInstitutionsSortedByDate([FromQuery] string sortOrder)
        {
            try
            {
                var institutions = await _filterInstituteData.GetFilteredDataBydate(sortOrder);
                return Ok(institutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("CountryFilter")]
        public async Task<IActionResult> GetInstitutionsSortedByCountry([FromQuery] string countryName)
        {
            try
            {
                var institutions = await _filterInstituteData.GetFilteredDataByCountry(countryName);
                return Ok(institutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("StateFilter")]
        public async Task<IActionResult> GetInstitutionsSortedByState([FromQuery] string stateName)
        {
            try
            {
                var institutions = await _filterInstituteData.GetFilteredDataByState(stateName);
                return Ok(institutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("CityFilter")]
        public async Task<IActionResult> GetInstitutionsSortedByCity([FromQuery] string cityName)
        {
            try
            {
                var institutions = await _filterInstituteData.GetFilteredDataByCity(cityName);
                return Ok(institutions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
