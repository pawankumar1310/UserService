using System.Data.SqlClient;
using Constants.StoredProcedure;
using Package;
using Model.UserService;
using Middleware;

namespace DBService
{
    public class UrlDBService
    {

        public async Task<StatusResponse<int>> InsertUrl(AddUrlModelRequest addUrlModelRequest)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                string connectionString = Utility.ConfigurationUtility.GetConnectionString();

                List<int> results = new List<int>();

                foreach (var urlModel in addUrlModelRequest.Urls)
                {
                    var parameters = new SqlParameter[]
                    {
                new SqlParameter("@url", urlModel.Url),
                new SqlParameter("@urllabelId", urlModel.UrlLabelId),
                new SqlParameter("@referenceID", addUrlModelRequest.ReferenceID)
                    };

                    var storedProcedure = UserDB.InsertUrl;

                    var result = await curdMiddleware.ExecuteNonQuery(
                        connectionString: connectionString,
                        storedProcedureName: storedProcedure,
                        parameters: parameters
                    );

                    results.Add(result);
                }

                if (results.Any(r => r > 0))
                {
                    return StatusResponse<int>.SuccessStatus(results.Sum(), StatusCode.Success);
                }
                else
                {
                    return StatusResponse<int>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<int>.FailureStatus(StatusCode.knownException, ex);
            }
        }


        public async Task<StatusResponse<List<GetUrlLabelsModelResponse>>> GetAllUrlLabels()
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                
                var storedProcedure = UserDB.GetUrlLabels; 
                string connectionString = Utility.ConfigurationUtility.GetConnectionString();

                var result = await curdMiddleware.ExecuteDataReaderList<GetUrlLabelsModelResponse>(
                    connectionString: connectionString,
                    storedProcedureName: storedProcedure,
                    (reader) => new GetUrlLabelsModelResponse
                    {
                        UrlLabelID = reader["urllabelID"].ToString(),
                        UrlLabel = reader["urlLabel"].ToString(),
                        Url = reader["url"].ToString()
                    }
                );



                if (result != null)
                {
                    return StatusResponse<List<GetUrlLabelsModelResponse>>.SuccessStatus(result, StatusCode.Success);

                }
                else
                {
                    return StatusResponse<List<GetUrlLabelsModelResponse>>.FailureStatus(StatusCode.NotFound, new Exception());

                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<GetUrlLabelsModelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }




    }
}
