using Model.UserService;
using Package;
using DTO.UserService;
using DBService;

namespace Service
{
    public class FacilityService
    {
        public StatusResponse<int> CreateFacility(CreateFacilityRequest createFacilityRequest)
        {
            var createFacilityModelRequest = new CreateFacilityModelRequest
            {
                Name = createFacilityRequest.Name,
                CreatedBy = createFacilityRequest.CreatedBy,
            };

            try
            {
                FacilityDBService facilityDBService = new();
                var result = facilityDBService.CreateFacility(createFacilityModelRequest).Result;

                if (result.Success)
                {
                    return StatusResponse<int>.SuccessStatus(result.Data, StatusCode.Found);

                }
                else
                {
                    return StatusResponse<int>.FailureStatus(result.StatusCode, new Exception());
                }

            }
            catch (Exception ex)
            {
                return StatusResponse<int>.FailureStatus(StatusCode.knownException, ex);
            }

        }
    }
}
