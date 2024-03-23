using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;

namespace Structure
{
    public interface IFacilityService
    {
        Task<bool> CreateFacility(CreateFacilityRequest createRequest);
        Task<Facility> GetFacilityById(string facilityId);
        Task<List<Facility>> GetAllFacilities();
        Task UpdateFacility(string facilityId, UpdateFacilityRequest updateRequest);
        Task DeleteFacility(string facilityId);
    }
}
