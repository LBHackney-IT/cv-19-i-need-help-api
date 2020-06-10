using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.APIGatewayEvents;
using AutoFixture;
using CV19INeedHelp;
using CV19INeedHelp.Boundary.V1;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Data.V1;
using CV19INeedHelp.Helpers.V1;
using CV19INeedHelp.Models.V1;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CV19INeedHelpTest.EndToEndTests.V1
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
            const string connectionString =
                "Host=localhost;Database=i-need-help-test;Username=postgres;Password=mypassword";
            _dbContext = new Cv19SupportDbContext(_currentConnStr ?? connectionString);
            if (_currentConnStr == null) Environment.SetEnvironmentVariable("CV_19_DB_CONNECTION", connectionString);
            _fixture = new Fixture();
            _handler = new Handler();

            AssertionOptions.AssertEquivalencyUsing(options =>
            {
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>();
                options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation))
                    .WhenTypeIs<DateTimeOffset>();
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
        public void QueryByHelpRequestId()
        {
            var helpRequests = _fixture.CreateMany<ResidentSupportAnnex>().ToList();
            helpRequests.First().Id = 1;
            _dbContext.ResidentSupportAnnex.AddRange(helpRequests);
            _dbContext.SaveChanges();

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
