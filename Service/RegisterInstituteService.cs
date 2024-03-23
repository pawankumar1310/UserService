using DBService;
using Dto;
using DTO;
using Structure;

namespace Service
{
    public class RegisterInstituteService
    {
        private readonly IInstitute _institution;
        public RegisterInstituteService(IInstitute institution)
        {
            _institution=institution;
        }
        public async Task<int> InstituteRegistrationService(AddInstituteModel model)
        {
            int result=await _institution.InsertInstitution(model);
            return result;
        }


        public async Task<List<GetInstitution>> GetInstituteService()
        {
            List<GetInstitution> lst=await _institution.GetAllInstitutions();
            return lst;
        }

         
        public async Task<GetInstitution> GetInstitutionById(string institutionId)
        {
            return await _institution.GetInstitutionById(institutionId);
        }
        public async Task<int> InstituteUpdationService(string institutionID, UpdateInstitutionModel model)
        {
            int result = await _institution.UpdateInstitution(institutionID,model);
            return result;
        }
        public async Task<int> InstituteDeletionService(string institutionID)
        {
            int result = await _institution.DeleteInstitution(institutionID);
            return result;
        }


        public async Task<List<UserIDModel>> GetUserIdByEmailOrPhoneNumber(string emailOrPhone)
        {
            List<UserIDModel> result = await _institution.GetUserIdByEmailOrPhoneNumber(emailOrPhone);
            return result;
        }
    }
}