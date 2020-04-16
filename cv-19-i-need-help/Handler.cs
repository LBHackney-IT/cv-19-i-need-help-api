using Amazon.Lambda.Core;
using CV19INeedHelp.Data.V1;
using System;
using System.Net.Http.Headers;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using CV19INeedHelp.UseCases.V1;
using Newtonsoft.Json;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CV19INeedHelp
{
    public class Handler
    {
       public Response GetHelpRequests()
       {
           var connectionString = Environment.GetEnvironmentVariable("CV_19_DB_CONNECTION");
           var getRequestGateway = new OrganisationVolunteerGateway(connectionString);
           var getRequestObject = new GetHelpRequestsUseCase(getRequestGateway);
           try
           {
               var resp = getRequestObject.GetHelpRequests();
               LambdaLogger.Log(("Records retrieval success: " + resp.ToString()));
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
    }

    public class Response
    {
      public bool isBase64Encoded { get; set; }
      public string statusCode { get; set; }
      public string headers { get; set; }
      public string body { get; set; }

      public Response()
      {
      }
    }
}
