using System;
using System.Linq;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using CV19INeedHelp.Data.V1;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Helpers.V1;
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
           bool master = false;
           string uprn = null;
           string postcode = null;
           try
           {
               master = bool.Parse(requestParams["master"]);
               LambdaLogger.Log("master: " + master);
           }
           catch (Exception e)
           {
               LambdaLogger.Log("master parameter not provided.");
           }
           try
           {
               uprn = requestParams["uprn"];
               LambdaLogger.Log("uprn: " + uprn);
           }
           catch (Exception e)
           {
               LambdaLogger.Log("uprn parameter not provided.");
           }
           try
           {
               postcode = requestParams["postcode"];
               LambdaLogger.Log("postcode: " + postcode);
           }
           catch (Exception e)
           {
               LambdaLogger.Log("postcode parameter not provided.");
           }
           try
           {
               var resp = getRequestsUseCase.GetHelpRequests(uprn, postcode, master)
                   .Select(x => x.ToResponse());
               LambdaLogger.Log("Records retrieval success: " + JsonConvert.SerializeObject(resp));
               return new APIGatewayProxyResponse
               {
                   IsBase64Encoded = true, StatusCode = 200, Body = ConvertToCamelCasedJson(resp)
               };
           }
           catch(Exception e)
           {
               LambdaLogger.Log("Error: " + e.Message);
               var response = new APIGatewayProxyResponse
               {
                   IsBase64Encoded = true,
                   StatusCode = 500,
                   Body = "Error processing request: " + ". Error Details: " + e.Message + e.StackTrace
               };
               return response;
           }
       }

       public Response GetHelpRequest(APIGatewayProxyRequest request, ILambdaContext context)
       {
           var getRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
           var getRequestObject = new GetHelpRequestUseCase(getRequestGateway);
           var request_params = request.PathParameters;
           var request_id = Int32.Parse(request_params["id"]);
           try
           {
               var resp = getRequestObject.GetHelpRequest(request_id);
               LambdaLogger.Log("Records retrieval success: " + JsonConvert.SerializeObject(resp));
               var response = new Response();
               response.isBase64Encoded = true;
               response.statusCode = "200";
               response.body = JsonConvert.SerializeObject(resp);
               return response;
           }
           catch(Exception e)
           {
               LambdaLogger.Log("Error: " + e.Message);
               var response = new Response();
               response.isBase64Encoded = true;
               response.statusCode = "500";
               response.body = "Error processing request: " + ". Error Details: " + e.Message + e.StackTrace;
               return response;
           }
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
               NullValueHandling = NullValueHandling.Ignore
           });
       }
    }

    public class Response
    {
      public bool isBase64Encoded { get; set; }
      public string statusCode { get; set; }
      public string headers { get; set; }
      public string body { get; set; }
    }
}
