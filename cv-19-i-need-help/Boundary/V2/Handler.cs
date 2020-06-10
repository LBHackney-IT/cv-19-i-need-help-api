using System;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using CV19INeedHelp.Boundary.Exceptions;
using CV19INeedHelp.Boundary.V2.Responses;
using CV19INeedHelp.Data.V1;
using CV19INeedHelp.Helpers.V2;
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
           var getRequestGateway = new CV19INeedHelp.Gateways.V2.INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
           var getRequestsUseCase = new CV19INeedHelp.UseCases.V2.GetHelpRequestsUseCase(getRequestGateway);

           var master = QueryValueGiven("master", request) && bool.TryParse(request.QueryStringParameters["master"], out var v)
               ? SetValue(v, "master")
               : ValueNotSet<bool>("master");
           var uprn = GetQueryParameter("uprn", request);
           var postcode = GetQueryParameter("postcode", request);
           var address = GetQueryParameter("address", request);
           var firstName = GetQueryParameter("first_name", request);
           var lastName = GetQueryParameter("last_name", request);

           try
           {
               var requests = new ResidentSupportAnnexResponseList
               {
                   HelpRequests = getRequestsUseCase
                       .GetHelpRequests(uprn, postcode, address, firstName, lastName, master).ToResponse(),
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
           catch (InvalidQueryParameter e)
           {
               return new APIGatewayProxyResponse
               {
                   IsBase64Encoded = true,
                   StatusCode = 400,
                   Body = "Error processing request: " + e.Message
               };
           }
           catch (Exception e)
           {
               return SendServerErrorResponse(e);
           }
       }

       private static APIGatewayProxyResponse SendServerErrorResponse(Exception e)
       {
           LambdaLogger.Log("Error: " + e.Message);
           return new APIGatewayProxyResponse
           {
               IsBase64Encoded = true,
               StatusCode = 500,
               Body = "Error processing request: " + ". Error Details: " + e.Message + e.StackTrace
           };
       }

       private static string GetQueryParameter(string keyName, APIGatewayProxyRequest request)
       {
           return QueryValueGiven(keyName, request)
               ? SetValue(request.QueryStringParameters[keyName], keyName)
               : ValueNotSet<string>(keyName);
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
