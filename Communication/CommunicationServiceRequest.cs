// CommunicationService.cs
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dto;
using Newtonsoft.Json;

namespace Communication
{
public class CommunicationServiceRequest
{
    private readonly HttpClient _httpClient;

    private readonly string _communicationServiceBaseUrl = "https://localhost:7002/api";


    public CommunicationServiceRequest(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    

    public async Task<HttpResponseMessage> SendOtpToCommunicationService(CommunicationServiceModel communicationServiceModel)
    {
        var request = new
        {
            communicationServiceModel.email,
            communicationServiceModel.phoneNumber,
            communicationServiceModel.countryID,
            communicationServiceModel.otp
        };

        var requestContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync($"{_communicationServiceBaseUrl}/SendOtp", requestContent);
    }


}
    
}
