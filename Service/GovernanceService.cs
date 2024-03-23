using Dto;
using Structure;


namespace Service
{
    public class GovernanceService
    {
        private readonly IGovernance _governance;
        public GovernanceService(IGovernance governance)
        {
            _governance = governance;
        }

        public async Task<int> AddGovernanceService(GovernanceModel model)
        {
            int result = await _governance.InsertGovernance(model);
            return result;
        }
        public async Task<List<GetGovernanceModel>> GetGovernanceService()
        {
            List<GetGovernanceModel> lst = await _governance.GetGovernance();
            return lst;
        }
        public async Task<int> GovernanceUpdationService(string governanceID, UpdateGovernanceModel model)
        {
            int result = await _governance.UpdateGovernance(governanceID, model);
            return result;
        }

        public async Task<int> GovernanceDeletionService(string governanceTypeID)
        {
            int result = await _governance.DeleteGovernance(governanceTypeID);
            return result;
        }



    }
}
