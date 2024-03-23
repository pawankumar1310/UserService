using Dto;
using Structure;


namespace Service
{
    public class GovernanceTypeService
    {
        private readonly IGovernanceType _governanceType;
        public GovernanceTypeService(IGovernanceType governanceType)
        {
            _governanceType = governanceType;
        }

        public async Task<int> AddGovernanceService(string name,string createdBy)
        {
            int result=await _governanceType.InsertGovernanceType(name,createdBy);
            return result;
        }
        public async Task<List<ViewGovernanceTypeModel>> GetGovernanceService()
        {
            List<ViewGovernanceTypeModel> lst = await _governanceType.GetGovernanceTypes();
            return lst;
        }
        public async Task<int> GovernanceTypeUpdationService(string governanceTypeID, ViewGovernanceTypeModel model)
        {
            int result = await _governanceType.UpdateGovernanceType(governanceTypeID, model);
            return result;
        }

        public async Task<int> GovernanceTypeDeletionService(string governanceTypeID)
        {
            int result=await _governanceType.DeleteGovernanceType(governanceTypeID); 
            return result;
        }



    }
}
