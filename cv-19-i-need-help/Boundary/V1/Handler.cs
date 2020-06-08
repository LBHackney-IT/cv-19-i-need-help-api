using System;
using System.Linq;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Data.V1;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Helpers.V1;
using CV19INeedHelp.Models.V1;
using CV19INeedHelp.UseCases.V1;
using Newtonsoft.Json;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CV19INeedHelp.Boundary.V1
{
    public class Handler
    {
       private readonly string _connectionString;
       
       public Handler()
       {
           _connectionString = Environment.GetEnvironmentVariable("CV_19_DB_CONNECTION");
       }
       public Response GetHelpRequests(APIGatewayProxyRequest request, ILambdaContext context)
       {
           var getRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
           var getRequestObject = new GetHelpRequestsUseCase(getRequestGateway);
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
               var resp = getRequestObject.GetHelpRequests(uprn, postcode, master)
                   .Select(x => x.ToResponse());
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
       
       public Response GetHelpRequestExceptions(APIGatewayProxyRequest request, ILambdaContext context)
       {
           var getRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
           var getRequestObject = new GetHelpRequestsUseCase(getRequestGateway);
           //LambdaLogger.Log(("Begin request"));
           try
           {
               var resp = getRequestObject.GetHelpRequestExceptions();
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
       
       public Response UpdateHelpRequest(APIGatewayProxyRequest request, ILambdaContext context)
       {
           var getRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
           var updateRequestObject = new UpdateHelpRequestUseCase(getRequestGateway);
           var requestParams = request.PathParameters;
           var requestData = JsonConvert.DeserializeObject<ResidentSupportAnnexResponse>(request.Body);
           var requestId = Int32.Parse(requestParams["id"]);
           try
           {
               updateRequestObject.UpdateHelpRequest(requestId, requestData.ToModel());
               LambdaLogger.Log(("Record update success"));
               return new Response
               {
                   isBase64Encoded = true,
                   statusCode = "200",
                   body = "Record Updated"
               };
           }
           catch(Exception e)
           {
               LambdaLogger.Log("Error: " + e.Message);
               return new Response
               {
                   isBase64Encoded = true,
                   statusCode = "500",
                   body = "Error processing request: " + ". Error Details: " + e.Message + e.StackTrace
               };
           }
       }

       public Response PatchHelpRequest(APIGatewayProxyRequest request, ILambdaContext context)
       {
           var getRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
           var updateRequestObject = new UpdateHelpRequestUseCase(getRequestGateway);
           var request_params = request.PathParameters;
           var request_data = JsonConvert.DeserializeObject<ResidentSupportAnnexPatch>(request.Body);
           var request_id = Int32.Parse(request_params["id"]);
           try
           {
               LambdaLogger.Log(request.Body);
               updateRequestObject.PatchHelpRequest(request_id, request_data);
               LambdaLogger.Log(("Record update success"));
               var response = new Response();
               response.isBase64Encoded = true;
               response.statusCode = "200";
               response.body = "Record Updated";
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

       public Response GenerateDeliverySchedule(APIGatewayProxyRequest request, ILambdaContext context)
       {
           var getRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
           var deliveryScheduleObject = new DeliveryScheduleUseCase(getRequestGateway);
           try
           {
               var request_params = request.QueryStringParameters;
               var limit = Int32.Parse(request_params["limit"]);
               var confirmed = bool.Parse(request_params["confirmed"]);
               var resp = deliveryScheduleObject.CreateDeliverySchedule(limit, confirmed);
               LambdaLogger.Log(("Records retrieval success: " + resp.ToString()));
               var response = new Response();
               response.isBase64Encoded = true;
               response.statusCode = "200";
               response.body = JsonConvert.SerializeObject(resp);
               return response;
           }
           catch (Exception e)
           {
               LambdaLogger.Log("Error: " + e.Message);
               var response = new Response();
               response.isBase64Encoded = true;
               response.statusCode = "500";
               response.body = "Error processing request: " + ". Error Details: " + e.Message + e.StackTrace;
               return response;
           }
       }

        public Response GetFoodDeliveriesRequestForForm(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var getRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
            var getRequestsForFormObject = new GetFoodDeliveriesForFormUseCase(getRequestGateway);
            try
            {
                var request_params = request.PathParameters;
                var request_id = Int32.Parse(request_params["id"]);
                var resp = getRequestsForFormObject.GetFoodDeliveriesForForm(request_id);
                LambdaLogger.Log(("Records retrieval success: " + resp.ToString()));
                var response = new Response();
                response.isBase64Encoded = true;
                response.statusCode = "200";
                response.body = JsonConvert.SerializeObject(resp);
                return response;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("Error: " + e.Message);
                var response = new Response();
                response.isBase64Encoded = true;
                response.statusCode = "500";
                response.body = "Error processing request: " + ". Error Details: " + e.Message + e.StackTrace;
                return response;
            }
        }
        
        public Response CreateFoodDeliveryRequest(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var createRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
            var createRequestObject = new CreateFoodDeliveryUseCase(createRequestGateway);
            try
            {
                var request_params = request.PathParameters;
                var data = JsonConvert.DeserializeObject<FoodDelivery>(request.Body);
                var resp = createRequestObject.CreateFoodDelivery(data);
                LambdaLogger.Log(("Records retrieval success: " + resp.ToString()));
                var response = new Response();
                response.isBase64Encoded = true;
                response.statusCode = "200";
                response.body = JsonConvert.SerializeObject(resp);
                return response;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("Error: " + e.Message);
                var response = new Response();
                response.isBase64Encoded = true;
                response.statusCode = "500";
                response.body = "Error processing request: " + ". Error Details: " + e.Message + e.StackTrace;
                return response;
            }
        }
        
        public Response UpdateFoodDeliveryRequest(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var updateRequestGateway = new INeedHelpGateway(new Cv19SupportDbContext(_connectionString));
            var updateRequestObject = new UpdateFoodDeliveryUseCase(updateRequestGateway);
            try
            {
                var request_params = request.PathParameters;
                var data = JsonConvert.DeserializeObject<FoodDelivery>(request.Body);
                updateRequestObject.UpdateFoodDelivery(data);
                LambdaLogger.Log(("Records update success."));
                var response = new Response();
                response.isBase64Encoded = true;
                response.statusCode = "200";
                return response;
            }
            catch (Exception e)
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
