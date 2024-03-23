using Dto;

namespace Structure
{
    public interface IGovernanceType
    {
        public Task<int> InsertGovernanceType(string name,string createdBy);
        public Task<List<ViewGovernanceTypeModel>> GetGovernanceTypes();
        public Task<int> UpdateGovernanceType(string governanceTypeID, ViewGovernanceTypeModel model);
        public Task<int> DeleteGovernanceType(string governanceTypeID);
    }
}
