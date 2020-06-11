using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CV19INeedHelp.Gateways.V2;
using CV19INeedHelp.Models.V1;
using CV19INeedHelpTest.EndToEndTests.V2;
using FluentAssertions;
using NUnit.Framework;

namespace CV19INeedHelpTest.Gateways.V2
{
    [TestFixture]
    public class INeedHelpGatewayTest : DatabaseTests
    {
        private INeedHelpGateway _classUnderTest;
        private readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new INeedHelpGateway(DbContext);
        }

        [Test]
        public void QueryHelpRequests_GivenNullQueryParameters_ShouldReturnAllRequests()
        {
            var requests = _fixture.CreateMany<ResidentSupportAnnex>();
            InsertIntoResidentSupportAnnexTable(requests);

            var response = _classUnderTest.QueryHelpRequests(null, null, null, null, null, false);
            response.Should().BeEquivalentTo(requests);
        }

        [TestCase("to look for")]
        [TestCase("another uprn")]
        public void QueryHelpRequests_GivenUprnQuery_ShouldReturnMatchingRequests(string uprn)
        {
            var requests = _fixture.CreateMany<ResidentSupportAnnex>();
            requests.First().Uprn = uprn;
            InsertIntoResidentSupportAnnexTable(requests);

            var response = _classUnderTest.QueryHelpRequests(uprn, null, null, null, null, false);
            response.Should().BeEquivalentTo(new List<ResidentSupportAnnex>{requests.First()});
        }

        [TestCase("my postcode")]
        [TestCase("postcode no2")]
        public void QueryHelpRequests_GivenPostcodeQuery_ShouldReturnMatchingRequests(string postcode)
        {
            var requests = _fixture.CreateMany<ResidentSupportAnnex>();
            requests.Last().Postcode = postcode;
            InsertIntoResidentSupportAnnexTable(requests);

            var response = _classUnderTest.QueryHelpRequests(null, postcode, null, null, null, false);
            response.Should().BeEquivalentTo(new List<ResidentSupportAnnex>{requests.Last()});
        }

        [TestCase("My street")]
        [TestCase("My house name")]
        public void QueryHelpRequests_GivenAddressQuery_ShouldReturnMatchingRequests(string lastName)
        {
            var requests = _fixture.CreateMany<ResidentSupportAnnex>();
            requests.Last().AddressFirstLine = lastName;
            InsertIntoResidentSupportAnnexTable(requests);

            var response = _classUnderTest.QueryHelpRequests(null, null, lastName, null, null, false);
            response.Should().BeEquivalentTo(new List<ResidentSupportAnnex>{requests.Last()});
        }

        [TestCase("my uprn", "my postcode")]
        [TestCase("this uprn", "not this postcode")]
        public void QueryHelpRequests_GivenPostcodeAndUprnQueries_ShouldReturnRequestMatchingTheUprn(string uprn, string postcode)
        {
            var requests = _fixture.CreateMany<ResidentSupportAnnex>(4).ToList();
            requests.ElementAt(0).Uprn = uprn;
            requests.ElementAt(1).Postcode = postcode;
            requests.ElementAt(2).Uprn = uprn;
            InsertIntoResidentSupportAnnexTable(requests);

            var response = _classUnderTest.QueryHelpRequests(uprn, postcode, null, null, null, false);
            response.Should().BeEquivalentTo(new List<ResidentSupportAnnex>{requests.ElementAt(0), requests.ElementAt(2)});
        }

        [Test]
        public void QueryHelpRequests_GivenMasterQueryAsTrue_ShouldOnlyReturnMasterRecords()
        {
            var requests = _fixture.Build<ResidentSupportAnnex>()
                .With(x => x.RecordStatus, "MASTER")
                .CreateMany(4).ToList();
            requests.ElementAt(2).RecordStatus = "DUPLICATE";
            InsertIntoResidentSupportAnnexTable(requests);

            var response = _classUnderTest.QueryHelpRequests(null, null, null, null, null, true);

            response.Count.Should().Be(3);
            response.Should().BeEquivalentTo(new List<ResidentSupportAnnex>
            {
                requests.ElementAt(0), requests.ElementAt(1), requests.ElementAt(3)
            });
        }

        [TestCase("uprn")]
        [TestCase("uprn2")]
        public void QueryHelpRequests_GivenMasterAndUprnQueries_ShouldOnlyReturnMatchingRequestsWhichAreMasterRecords(string uprn)
        {
            var requests = _fixture.Build<ResidentSupportAnnex>()
                .With(x => x.RecordStatus, "MASTER")
                .CreateMany(4).ToList();
            requests.ElementAt(2).RecordStatus = "DUPLICATE";
            requests.ElementAt(2).Uprn = uprn;
            requests.ElementAt(1).Uprn = uprn;
            InsertIntoResidentSupportAnnexTable(requests);

            var response = _classUnderTest.QueryHelpRequests(uprn, null, null, null, null, true);
            response.Should().BeEquivalentTo(new List<ResidentSupportAnnex>
            {
                requests.ElementAt(1),
            });
        }

        [TestCase("my name")]
        [TestCase("your name")]
        public void QueryHelpRequests_GivenFirstNameQuery_ShouldReturnMatchingRequests(string firstName)
        {
            var requests = _fixture.CreateMany<ResidentSupportAnnex>();
            requests.Last().FirstName = firstName;
            InsertIntoResidentSupportAnnexTable(requests);

            var response = _classUnderTest.QueryHelpRequests(null, null, null, firstName, null, false);
            response.Should().BeEquivalentTo(new List<ResidentSupportAnnex>{requests.Last()});
        }

        [TestCase("last name")]
        [TestCase("surname")]
        public void QueryHelpRequests_GivenLastNameQuery_ShouldReturnMatchingRequests(string lastName)
        {
            var requests = _fixture.CreateMany<ResidentSupportAnnex>();
            requests.Last().LastName = lastName;
            InsertIntoResidentSupportAnnexTable(requests);

            var response = _classUnderTest.QueryHelpRequests(null, null, null, null, lastName, false);
            response.Should().BeEquivalentTo(new List<ResidentSupportAnnex>{requests.Last()});
        }

        private void InsertIntoResidentSupportAnnexTable(IEnumerable<ResidentSupportAnnex> requests)
        {
            DbContext.ResidentSupportAnnex.AddRange(requests);
            DbContext.SaveChanges();
        }
    }
}