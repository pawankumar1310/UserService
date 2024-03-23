using DBService;
using Dto;

namespace Service
{
    public class FacilityService
    {
        private readonly FacilityDBService _facilityDBService;

        public FacilityService(FacilityDBService facilityDBService)
        {
            _facilityDBService = facilityDBService;
        }


//-----------------------------  ADD Facility --------------------------------------

        public async Task<bool> CreateFacility(CreateFacilityRequest createFacilityRequest)
        {
            return await _facilityDBService.CreateFacility(createFacilityRequest);
        }
        
//------------------------------------- GET Facility BY ID ---------------------------------------

        public async Task<Facility> GetFacilityById(string FacilityId)
        {
            return await _facilityDBService.GetFacilityById(FacilityId);
        }


//----------------------------------- GET ALL FacilityS ------------------------------------

        public async Task<List<Facility>> GetAllFacilities()
        {
            return await _facilityDBService.GetAllFacilities();
        }


// ---------------------------------------- UPDATE Facility ---------------------------------------
        public async Task UpdateFacility(string FacilityId, UpdateFacilityRequest updateFacilityRequest)
        {
            await _facilityDBService.UpdateFacility(FacilityId, updateFacilityRequest);
        }

//-------------------------------------- DELETE Facility ----------------------------------------

        public async Task DeleteFacility(string FacilityId)
        {
            await _facilityDBService.DeleteFacility(FacilityId);
        }


    }

}