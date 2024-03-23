// CommunicationService.cs
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dto;
using Newtonsoft.Json;

public class UtilityServiceRequest
{
    private readonly HttpClient _httpClient;
    private readonly string _utilityServiceBaseUrl = "https://localhost:7003/api";
    

    public UtilityServiceRequest(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<HttpResponseMessage> RequestOtpFromUtilityService()
    {
  
        return await _httpClient.GetAsync($"{_utilityServiceBaseUrl}/Otp");

    }



    public async Task<List<PlaceInformationModel>> GetAdddressByZipID(string zipCodeID)
    {
        var response = await _httpClient.GetAsync($"{_utilityServiceBaseUrl}/PlaceInformation/{zipCodeID}");
        if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var address = JsonConvert.DeserializeObject<List<PlaceInformationModel>>(responseContent);
                return address;
            }
            else
            {
                throw new InvalidOperationException($"Failed to get address by zipCodeID. Status code: {response.StatusCode}");
            }
        
    }

    

}
