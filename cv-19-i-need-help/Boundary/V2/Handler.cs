using System;
using System.Linq;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using CV19INeedHelp.Boundary.V2.Responses;
using CV19INeedHelp.Data.V1;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Helpers.V2;
using CV19INeedHelp.UseCases.V1;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CV19INeedHelp.Boundary.V2
{
    public class Handler
    {
        private readonly string _connectionString;

        public Handler()
        {
            _connectionString = Environment.GetEnvironmentVariable("CV_19_DB_CONNECTION");
        }

        public APIGatewayProxyResponse GetHelpRequests(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var getRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
            var getRequestsUseCase = new GetHelpRequestsUseCase(getRequestGateway);
            var requestParams = request.QueryStringParameters;
            var master = QueryValueGiven("master", request) && bool.TryParse(requestParams["master"], out var v)
                ? SetValue(v, "master")
                : ValueNotSet<bool>("master");

            var uprn = QueryValueGiven("uprn", request)
                ? SetValue(requestParams["uprn"], "uprn")
                : ValueNotSet<string>("uprn");

            var postcode = QueryValueGiven("postcode", request)
                ? SetValue(requestParams["postcode"], "postcode")
                : ValueNotSet<string>("postcode");

            try
            {
                var requests = new ResidentSupportAnnexResponseList
                {
                    HelpRequests = getRequestsUseCase.GetHelpRequests(uprn, postcode, master).ToResponse(),
                };
                var resp = ConvertToCamelCasedJson(requests);
                LambdaLogger.Log("Records retrieval success: " + resp);

                return new APIGatewayProxyResponse
                {
                    IsBase64Encoded = true,
                    StatusCode = 200,
                    Body = resp
                };
            }
            catch (Exception e)
            {
                return SendErrorResponse(e);
            }
        }

        public APIGatewayProxyResponse GetHelpRequest(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var getRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
            var getRequestUseCase = new GetHelpRequestUseCase(getRequestGateway);
            var requestParams = request.PathParameters;

            var requestedId = Convert.ToInt32(requestParams["id"]);
            var responseBody = getRequestUseCase.GetHelpRequest(requestedId);

            try
            {
                if (responseBody is null)
                {
                    return new APIGatewayProxyResponse
                    {
                        IsBase64Encoded = true,
                        StatusCode = 404,
                    };
                }

                var resp = ConvertToCamelCasedJson(responseBody.ToResponse());
                return new APIGatewayProxyResponse
                {
                    IsBase64Encoded = true,
                    StatusCode = 200,
                    Body = resp
                };
            }
            catch (Exception e)
            {
                return SendErrorResponse(e);
            }
        }

        private static APIGatewayProxyResponse SendErrorResponse(Exception e)
        {
            LambdaLogger.Log("Error: " + e.Message);
            return new APIGatewayProxyResponse
            {
                IsBase64Encoded = true,
                StatusCode = 500,
                Body = "Error processing request: " + ". Error Details: " + e.Message + e.StackTrace
            };
        }

        private static bool QueryValueGiven(string keyName, APIGatewayProxyRequest request)
        {
            return request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey(keyName);
        }

        private static T ValueNotSet<T>(string name)
        {
            LambdaLogger.Log($"{name} parameter not provided.");
            return default;
        }

        private static T SetValue<T>(T result, string name)
        {
            LambdaLogger.Log($"{name}: " + result);
            return result;
        }

        private static string ConvertToCamelCasedJson<T>(T responseBody)
        {
            return JsonConvert.SerializeObject(responseBody, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(),
                },
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            });
        }
    }
}
