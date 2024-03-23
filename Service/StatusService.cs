using Structure;

namespace Service
{
    public class StatusService
    {
        private readonly IStatus _status;
        public StatusService(IStatus status)
        {
            _status = status;
        }
        public async Task<int> approveInstituteService(string statusName,string institutionID)
        {
            int result=await _status.approveInstitution(statusName, institutionID);
            return result;
        }
        
    }
}
