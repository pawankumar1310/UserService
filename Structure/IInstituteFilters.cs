using Dto;


namespace Structure
{
    public interface IInstituteFilters
    {
        public Task<List<ViewInstitutionModel>> GetInstitutionsByStatus(string status);
        public  Task<List<FilterByDateModel>> GetInstitutionsSortedByDateAsync(string sortOrder);

        public Task<List<institutionByCountry>> GetInstitutionsSortedByCountryAsync(string countryName);
        public Task<List<institutionByCountry>> GetInstitutionsSortedByStateAsync(string stateName);
        public Task<List<institutionByCountry>> GetInstitutionsSortedByCityAsync(string stateName);

    }
}
