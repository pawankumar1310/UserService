namespace Structure
{
    public interface IStatus
    {
        public Task<int> approveInstitution(string statusName,string institutionID);
    }
}
