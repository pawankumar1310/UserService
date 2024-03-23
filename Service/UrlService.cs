using Dto;
using Structure;

namespace UserService.Service
{
    public class UrlService
    {
        private readonly IUrl _url;
        public UrlService(IUrl url)
        {
            _url = url;
        }

        public async Task<int> AddUrlService(UrlModel model)
        {
            int result = await _url.InsertUrl(model);
            return result;
        }

        public async Task<bool> UpdateUrl(string url, UrlModel model)
        {
            return await _url.UpdateUrl(url, model);
        }

        public async Task<bool> UpdateUserWithUrl(string userID, string name, string countryID, string phoneNumber, string emailAddress, string zipCodeID, string url, string label)
        {
            return await _url.UpdateUserWithUrl(userID, name, countryID, phoneNumber, emailAddress, zipCodeID, url, label);
        }
    }
}
