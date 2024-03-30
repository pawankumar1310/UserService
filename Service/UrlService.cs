using Dto;
using Model.UserService;
using Package;
using DBService;
using DTO.UserService;


namespace Service
{
    public class UrlService
    {
        public StatusResponse<int> InsertUrl(AddUrlRequest addUrlRequest)
        {

            List<UpdateUrlModelRequest> userUrlModels = addUrlRequest.Urls?.Select(url => new UpdateUrlModelRequest
            {
                Url = url.Url,
                UrlLabelId = url.UrlLabelId
            }).ToList() ?? new List<UpdateUrlModelRequest>();




            AddUrlModelRequest addUrlModelRequest = new AddUrlModelRequest
            {
                Urls = userUrlModels,
                ReferenceID = addUrlRequest.ReferenceID,
            };
            try
            {
                UrlDBService urlDBService = new();
                var result = urlDBService.InsertUrl(addUrlModelRequest).Result;

                if (result.Success)
                {
                    return StatusResponse<int>.SuccessStatus(result.Data, StatusCode.Found);

                }
                else
                {
                    return StatusResponse<int>.FailureStatus(result.StatusCode, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<int>.FailureStatus(StatusCode.knownException, ex);
            }
        }


        public StatusResponse<List<GetUrlLabelsModelResponse>> GetAllUrlLabels()
        {
            try
            {
                UrlDBService urlDBService = new();
                var result = urlDBService.GetAllUrlLabels().Result;

                if (result.Success)
                {
                    return StatusResponse<List<GetUrlLabelsModelResponse>>.SuccessStatus(result.Data, StatusCode.Found);

                }
                else
                {
                    return StatusResponse<List<GetUrlLabelsModelResponse>>.FailureStatus(result.StatusCode, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetUrlLabelsModelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }
    }
}