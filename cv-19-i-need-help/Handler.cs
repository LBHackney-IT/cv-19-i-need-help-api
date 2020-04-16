using Amazon.Lambda.Core;
using CV19INeedHelp.Data.V1;
using System;
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
               var response = getRequestObject.GetHelpRequests();
               LambdaLogger.Log(("Records retrieval success: " + response.ToString()));
               return new Response(response.ToString());
           }
           catch(Exception e)
           {
               LambdaLogger.Log("Error: " + e.Message);
               return new Response("Error processing request: " + ". Error Details: " + e.Message + e.StackTrace);
           }
       }
    }

    public class Response
    {
      public string Message {get; set;}

      public Response(string message){
//          {
//              string resp = string.Empty;
//              resp += "isBase64Encoded": true",
//              
//              "statusCode": httpStatusCode,
//              "headers": { "headerName": "headerValue", ... },
//              "body": message
//          }
        Message = message;
      }
    }
}
