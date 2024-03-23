using Dto;
using DTO;

namespace Structure
{
    public interface IInstitute
    {
        public Task<int> InsertInstitution(AddInstituteModel model);

        public Task<List<GetInstitution>> GetAllInstitutions();
        public Task<GetInstitution> GetInstitutionById(string institutionId);

        public Task<int> UpdateInstitution(string institutionID, UpdateInstitutionModel model);

        public Task<int> DeleteInstitution(string institutionID);
        public Task<List<UserIDModel>> GetUserIdByEmailOrPhoneNumber(string emailOrPhone);


    }
}