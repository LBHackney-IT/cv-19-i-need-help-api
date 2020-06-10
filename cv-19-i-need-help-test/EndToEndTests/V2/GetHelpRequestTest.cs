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
    public class GetHelpRequestTest
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
        public void ReturnsCamelCasedResponse()
        {
            var helpRequest = new ResidentSupportAnnex { Id = 1 };
            _dbContext.ResidentSupportAnnex.Add(helpRequest);
            _dbContext.SaveChanges();

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
        public void WithNoQueryParameters_ReturnsNotFound()
        {
            var helpRequest = new ResidentSupportAnnex { Id = 1 };
            _dbContext.ResidentSupportAnnex.Add(helpRequest);
            _dbContext.SaveChanges();

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
            _dbContext.ResidentSupportAnnex.Add(helpRequest);
            _dbContext.SaveChanges();

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
            Console.WriteLine(received);
            received.LastConfirmedFoodDelivery.Substring(0, 10).Should()
                .Be(expected.LastConfirmedFoodDelivery.Substring(0, 10));
        }
    }
}
