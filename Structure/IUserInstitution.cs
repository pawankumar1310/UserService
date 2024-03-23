using Dto;
using DTO;

namespace Structure
{
    public interface IUserInstitution
    {
        public  Task<int> InsertUserInstitution(UserInstitutionModel model);
        public Task<List<UserInstituteModel>> GetUserInstitute(string userID);
    }
}
