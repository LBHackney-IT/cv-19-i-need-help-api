using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CV19INeedHelp.UseCases.V2;
using CV19INeedHelp.Gateways.V2;
using CV19INeedHelp.Models.V1;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace CV19INeedHelpTest.UseCases.V2
{
    [TestFixture]
    public class GetHelpRequestsUseCaseTest
    {
        private IGetHelpRequestsUseCase _classUnderTest;
        private Mock<IINeedHelpGateway> _fakeGateway;
        private readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new GetHelpRequestsUseCase(_fakeGateway.Object);
        }

        [TestCase("123", "test", false, "name", "last", "address")]
        [TestCase("327", "another_test", true, "anotherName", "surname", "my street")]
        public void CanCallTheDatabaseGetAllMethodWithAllQueryParameters(string uprn, string postcode, bool isMaster, string firstName, string lastName, string address)
        {
            _classUnderTest.GetHelpRequests(uprn, postcode,address, firstName, lastName, isMaster);
            _fakeGateway.Verify(m => m.QueryHelpRequests(uprn, postcode,address, firstName, lastName, isMaster));
        }

        [TestCase("123", "test", false, "name", "last", "address")]
        [TestCase("327", "another_test", true, "anotherName", "surname", "my street")]
        public void ReturnsTheListOfHelpRequestsFromTheGateway(string uprn, string postcode, bool isMaster, string firstName, string lastName, string address)
        {
            var responseData = _fixture.CreateMany<ResidentSupportAnnex>().ToList();
            _fakeGateway.Setup(x => x.QueryHelpRequests(uprn, postcode,address, firstName, lastName, isMaster)).Returns(responseData);
            var response = _classUnderTest.GetHelpRequests(uprn, postcode,address, firstName, lastName, isMaster);

            response.Should().BeEquivalentTo(responseData);
        }
    }
}
