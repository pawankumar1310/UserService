using Dto;

namespace Structure
{
    public interface IUrl
    {
        public Task<int> InsertUrl(UrlModel model);

        public Task<bool> UpdateUrl(string url, UrlModel model);
        public Task<bool> UpdateUserWithUrl(string userID, string name, string countryID, string phoneNumber, string emailAddress, string zipCodeID, string url, string label);
    }
}
