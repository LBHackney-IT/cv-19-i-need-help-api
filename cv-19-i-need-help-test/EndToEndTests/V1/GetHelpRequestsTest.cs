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
    public class GetHelpRequestsTest : DatabaseTests
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
        public void WithNoQueryParameters_ReturnsEverythingInResidentSupportAnnex()
        {
            var helpRequests = _fixture.CreateMany<ResidentSupportAnnex>().ToList();
            DbContext.ResidentSupportAnnex.AddRange(helpRequests);
            DbContext.SaveChanges();

            var response = _handler.GetHelpRequests(new APIGatewayProxyRequest(), null);

            response.statusCode.Should().Be("200");

            var responseBody = response.body;
            var deserializedBody = JsonConvert.DeserializeObject<List<ResidentSupportAnnexResponse>>(responseBody);

            var expectedResponse = helpRequests.Select(x => x.ToResponse());
            AssertHelpRequestsEquivalence(deserializedBody, expectedResponse);
        }

        [Test]
        public void QueryByUprn()
        {
            var helpRequests = _fixture.CreateMany<ResidentSupportAnnex>().ToList();
            helpRequests.First().Uprn = "to-search-for";
            DbContext.ResidentSupportAnnex.AddRange(helpRequests);
            DbContext.SaveChanges();

            var request = new APIGatewayProxyRequest
            {
                QueryStringParameters = new Dictionary<string, string> {{"uprn", "to-search-for"}}
            };
            var response = _handler.GetHelpRequests(request, null);

            response.statusCode.Should().Be("200");

            var responseBody = response.body;
            var deserializedBody = JsonConvert.DeserializeObject<List<ResidentSupportAnnexResponse>>(responseBody);

            deserializedBody.Count.Should().Be(1);

            var expectedResponse = helpRequests.First().ToResponse();
            AssertHelpRequestsEquivalence(deserializedBody.First(), expectedResponse);
        }

        [Test]
        public void QueryByPostcode()
        {
            var helpRequests = _fixture.CreateMany<ResidentSupportAnnex>().ToList();
            helpRequests.Last().Postcode = "return-me";
            DbContext.ResidentSupportAnnex.AddRange(helpRequests);
            DbContext.SaveChanges();

            var request = new APIGatewayProxyRequest
            {
                QueryStringParameters = new Dictionary<string, string> {{"postcode", "return-me"}}
            };
            var response = _handler.GetHelpRequests(request, null);

            response.statusCode.Should().Be("200");

            var responseBody = response.body;
            var deserializedBody = JsonConvert.DeserializeObject<List<ResidentSupportAnnexResponse>>(responseBody);

            deserializedBody.Count.Should().Be(1);
            var expectedResponse = helpRequests.Last().ToResponse();
            AssertHelpRequestsEquivalence(deserializedBody.Last(), expectedResponse);
        }

        private static void AssertHelpRequestsEquivalence(ResidentSupportAnnexResponse received, ResidentSupportAnnexResponse expected)
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

        private static void AssertHelpRequestsEquivalence(List<ResidentSupportAnnexResponse> received, IEnumerable<ResidentSupportAnnexResponse> expected)
        {
            received.Should().BeEquivalentTo(expected, options => options
                .Excluding(r => r.IsDuplicate)
                .Excluding(r => r.RecordStatus)
                .Excluding(r => r.LastConfirmedFoodDelivery));
            received.ForEach(r => r.IsDuplicate.Should().BeEquivalentTo("FALSE"));
            received.ForEach(r => r.RecordStatus.Should().BeEquivalentTo("MASTER"));
        }
    }
}