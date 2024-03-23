using Dto;


namespace Structure
{
    public interface IGovernance
    {
        public Task<int> InsertGovernance(GovernanceModel model);
        public Task<List<GetGovernanceModel>> GetGovernance();
        public Task<int> UpdateGovernance(string governanceID, UpdateGovernanceModel model);
        public Task<int> DeleteGovernance(string governanceID);
    }
}
