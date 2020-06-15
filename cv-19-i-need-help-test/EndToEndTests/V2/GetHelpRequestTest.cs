using System;
using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using AutoFixture;
using CV19INeedHelp.Boundary.V2;
using CV19INeedHelp.Boundary.V2.Responses;
using CV19INeedHelp.Data.V1;
using CV19INeedHelp.Helpers.V2;
using CV19INeedHelp.Models.V1;
using CV19INeedHelpTest.TestHelpers;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CV19INeedHelpTest.EndToEndTests.V2
{
    [TestFixture]
    public class GetHelpRequestTest : DatabaseTests
    {
        private Handler _handler;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _handler = new Handler();

            _fixture = new Fixture();
            CustomizeFixture.V2ResidentResponseParsable(_fixture);

            CustomizeAssertions.ApproximationDateTime();
        }

        [Test]
        public void ReturnsCamelCasedResponse()
        {
            var helpRequest = new ResidentSupportAnnex { Id = 1 };
            DbContext.ResidentSupportAnnex.Add(helpRequest);
            DbContext.SaveChanges();

            var response = _handler.GetHelpRequest(new APIGatewayProxyRequest
            {
                PathParameters = new Dictionary<string, string> {{"id", "1"}}
            }, null);

            response.StatusCode.Should().Be(200);
            response.Body.Should().BeEquivalentTo(@"{
  ""id"": 1,
  ""isDuplicate"": ""FALSE"",
  ""recordStatus"": ""MASTER""
}");
        }

        [Test]
        public void WithNoMatchingHelpRequest_ReturnsNotFound()
        {
            var helpRequest = new ResidentSupportAnnex { Id = 1 };
            DbContext.ResidentSupportAnnex.Add(helpRequest);
            DbContext.SaveChanges();

            var response = _handler.GetHelpRequest(new APIGatewayProxyRequest
            {
                PathParameters = new Dictionary<string, string> {{"id", "2"}}
            }, null);

            response.StatusCode.Should().Be(404);
        }

        [Test]
        public void QueryByHelpRequestId()
        {
            var helpRequest = _fixture.Create<ResidentSupportAnnex>();
            helpRequest.Id = 1;
            DbContext.ResidentSupportAnnex.Add(helpRequest);
            DbContext.SaveChanges();

            var response = _handler.GetHelpRequest(new APIGatewayProxyRequest
            {
                PathParameters = new Dictionary<string, string> {{"id", "1"}}
            }, null);

            response.StatusCode.Should().Be(200);

            var responseBody = response.Body;
            var deserializedBody = JsonConvert.DeserializeObject<ResidentSupportAnnexResponse>(responseBody);
            var expectedResponse = helpRequest.ToResponse();

            AssertHelpRequestsEquivalence(deserializedBody, expectedResponse);
        }

        private static void AssertHelpRequestsEquivalence(ResidentSupportAnnexResponse received, ResidentSupportAnnexResponse expected)
        {
            received.Should().BeEquivalentTo(expected, options => options
                .Excluding(r => r.IsDuplicate)
                .Excluding(r => r.RecordStatus)
                .Excluding(r => r.LastConfirmedFoodDelivery));
            received.IsDuplicate.Should().BeEquivalentTo("FALSE");
            received.RecordStatus.Should().BeEquivalentTo("MASTER");
            received.LastConfirmedFoodDelivery.Substring(0, 10).Should()
                .Be(expected.LastConfirmedFoodDelivery.Substring(0, 10));
        }
    }
}
