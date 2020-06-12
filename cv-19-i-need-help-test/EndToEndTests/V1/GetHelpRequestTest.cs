using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.APIGatewayEvents;
using AutoFixture;
using CV19INeedHelp.Boundary.V1;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Helpers.V1;
using CV19INeedHelp.Models.V1;
using CV19INeedHelpTest.EndToEndTests.V2;
using CV19INeedHelpTest.TestHelpers;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CV19INeedHelpTest.EndToEndTests.V1
{
    [TestFixture]
    public class GetHelpRequestTest : DatabaseTests
    {
        private Handler _handler;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _handler = new Handler();
            CustomizeAssertions.ApproximationDateTime();
        }

        [Test]
        public void QueryByHelpRequestId()
        {
            var helpRequests = _fixture.CreateMany<ResidentSupportAnnex>().ToList();
            helpRequests.First().Id = 1;
            DbContext.ResidentSupportAnnex.AddRange(helpRequests);
            DbContext.SaveChanges();

            var request = new APIGatewayProxyRequest
            {
                PathParameters = new Dictionary<string, string> {{"id", "1"}}
            };
            var response = _handler.GetHelpRequest(request, null);

            response.statusCode.Should().Be("200");

            var responseBody = response.body;
            var deserializedBody = JsonConvert.DeserializeObject<ResidentSupportAnnexResponse>(responseBody);

            var expectedResponse = helpRequests.First().ToResponse();
            AssertHelpRequestsEquivalence(deserializedBody, expectedResponse);
        }

        private static void AssertHelpRequestsEquivalence(ResidentSupportAnnexResponse received,
            ResidentSupportAnnexResponse expected)
        {
            received.Should().BeEquivalentTo(expected, options => options
                .Excluding(r => r.IsDuplicate)
                .Excluding(r => r.RecordStatus)
                .Excluding(r => r.LastConfirmedFoodDelivery));
            received.IsDuplicate.Should().BeEquivalentTo("FALSE");
            received.RecordStatus.Should().BeEquivalentTo("MASTER");
            received.LastConfirmedFoodDelivery.Should()
                .Be(expected.LastConfirmedFoodDelivery?.Date);
        }
    }
}
