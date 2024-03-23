using DBService;
using Dto;
using Structure;

namespace Service
{
    public class FilterInstituteData
    {
        private readonly IInstituteFilters _institutionFilter;
        public FilterInstituteData(IInstituteFilters institutionFilter)
        {
            _institutionFilter = institutionFilter;
        }
       
        public async Task<List<ViewInstitutionModel>> FilterInstituteService(string status)
        {
            List<ViewInstitutionModel> lst = await _institutionFilter.GetInstitutionsByStatus(status);
            return lst;
        }
        public async Task<List<FilterByDateModel>> GetFilteredDataBydate(string sortOrder)
        {
            List<FilterByDateModel> lst = await _institutionFilter.GetInstitutionsSortedByDateAsync(sortOrder);
            return lst;
        }
        public async Task<List<institutionByCountry>> GetFilteredDataByCountry(string countryName)
        {
            List<institutionByCountry> lst = await _institutionFilter.GetInstitutionsSortedByCountryAsync(countryName);
            return lst;
        }
        public async Task<List<institutionByCountry>> GetFilteredDataByState(string stateName)
        {
            List<institutionByCountry> lst = await _institutionFilter.GetInstitutionsSortedByStateAsync(stateName);
            return lst;
        }
        public async Task<List<institutionByCountry>> GetFilteredDataByCity(string cityName)
        {
            List<institutionByCountry> lst = await _institutionFilter.GetInstitutionsSortedByCityAsync(cityName);
            return lst;
        }


    }
}