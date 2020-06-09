using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetHelpRequestsTest
    {
        private string _currentConnStr;
        private Handler _handler;
        private Fixture _fixture;
        private Cv19SupportDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            _currentConnStr = Environment.GetEnvironmentVariable("CV_19_DB_CONNECTION");
            const string connectionString = "Host=localhost;Database=i-need-help-test;Username=postgres;Password=mypassword";
            _dbContext = new Cv19SupportDbContext(_currentConnStr ?? connectionString);
            if (_currentConnStr == null) Environment.SetEnvironmentVariable("CV_19_DB_CONNECTION", connectionString);
            _fixture = new Fixture();
            CustomizeFixture.V2ResidentResponseParsable(_fixture);
            _handler = new Handler();

            AssertionOptions.AssertEquivalencyUsing(options =>
            {
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>();
                options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTimeOffset>();
                return options;
            });
        }

        [TearDown]
        public void TearDown()
        {
            Environment.SetEnvironmentVariable("CV_19_DB_CONNECTION", _currentConnStr);
            var addedEntities = _dbContext.ResidentSupportAnnex;
            _dbContext.ResidentSupportAnnex.RemoveRange(addedEntities);
            _dbContext.SaveChanges();
        }

        [Test]
        public void WithNoQueryParameters_ReturnsEverythingInResidentSupportAnnex()
        {
            var helpRequests = _fixture.CreateMany<ResidentSupportAnnex>().ToList();
            _dbContext.ResidentSupportAnnex.AddRange(helpRequests);
            _dbContext.SaveChanges();

            var response = _handler.GetHelpRequests(new APIGatewayProxyRequest(), null);

            response.StatusCode.Should().Be(200);

            var responseBody = response.Body;
            var deserializedBody = JsonConvert.DeserializeObject<List<ResidentSupportAnnexResponse>>(responseBody);

            var expectedResponse = helpRequests.Select(x => x.ToResponse());
            AssertHelpRequestsEquivalence(deserializedBody, expectedResponse);
        }

        [Test]
        public void ReturnsCamelCasedResponse()
        {
            var helpRequest = new ResidentSupportAnnex
            {
                Id = 1,
            };
            _dbContext.ResidentSupportAnnex.Add(helpRequest);
            _dbContext.SaveChanges();
            var response = _handler.GetHelpRequests(new APIGatewayProxyRequest(), null);
            response.StatusCode.Should().Be(200);
            response.Body.Should().BeEquivalentTo(@"[
  {
    ""id"": 1,
    ""isDuplicate"": ""FALSE"",
    ""recordStatus"": ""MASTER""
  }
]");
        }

        [Test]
        public void QueryByUprn()
        {
            var helpRequests = _fixture.CreateMany<ResidentSupportAnnex>().ToList();
            helpRequests.First().Uprn = "to-search-for";
            _dbContext.ResidentSupportAnnex.AddRange(helpRequests);
            _dbContext.SaveChanges();

            var request = new APIGatewayProxyRequest
            {
                QueryStringParameters = new Dictionary<string, string> {{"uprn", "to-search-for"}}
            };
            var response = _handler.GetHelpRequests(request, null);

            response.StatusCode.Should().Be(200);

            var responseBody = response.Body;
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
            _dbContext.ResidentSupportAnnex.AddRange(helpRequests);
            _dbContext.SaveChanges();

            var request = new APIGatewayProxyRequest
            {
                QueryStringParameters = new Dictionary<string, string> {{"postcode", "return-me"}}
            };
            var response = _handler.GetHelpRequests(request, null);

            response.StatusCode.Should().Be(200);

            var responseBody = response.Body;
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