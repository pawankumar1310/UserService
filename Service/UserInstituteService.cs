using Dto;
using DTO;
using Structure;

namespace Service
{
    public class UserInstituteService
    {
        private readonly IUserInstitution _userinstitution;
        public UserInstituteService(IUserInstitution institution)
        {
            _userinstitution = institution;
        }
        public async Task<int> RegisterUserInstitute(UserInstitutionModel model)
        {
            int result=await _userinstitution.InsertUserInstitution(model);
            return result;
        }
        public async Task<List<UserInstituteModel>> GetUserInstitute(string UserID)
        {
            List<UserInstituteModel> result = await _userinstitution.GetUserInstitute(UserID);
            return result;
        }

    }
}
