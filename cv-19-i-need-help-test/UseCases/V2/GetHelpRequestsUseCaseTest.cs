using System;
using System.Linq;
using AutoFixture;
using CV19INeedHelp.Boundary.Exceptions;
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

        [TestCase("123", "BA2 2TH", false, "name", "last", "address")]
        [TestCase("327", "E8 1JJ", true, "anotherName", "surname", "my street")]
        public void CanCallTheDatabaseGetAllMethodWithAllQueryParameters(string uprn, string postcode, bool isMaster, string firstName, string lastName, string address)
        {
            _classUnderTest.GetHelpRequests(uprn, postcode,address, firstName, lastName, isMaster);
            _fakeGateway.Verify(m => m.QueryHelpRequests(uprn, postcode,address, firstName, lastName, isMaster));
        }

        [TestCase("123", "BA2 2TH", false, "name", "last", "address")]
        [TestCase("327", "E8 1JJ", true, "anotherName", "surname", "my street")]
        public void ReturnsTheListOfHelpRequestsFromTheGateway(string uprn, string postcode, bool isMaster, string firstName, string lastName, string address)
        {
            var responseData = _fixture.CreateMany<ResidentSupportAnnex>().ToList();
            _fakeGateway.Setup(x => x.QueryHelpRequests(uprn, postcode,address, firstName, lastName, isMaster)).Returns(responseData);
            var response = _classUnderTest.GetHelpRequests(uprn, postcode,address, firstName, lastName, isMaster);

            response.Should().BeEquivalentTo(responseData);
        }

        [TestCase("not a number")]
        [TestCase("77hh")]
        public void IfUprnQueryIdNonNumericThrowsInvalidQueryParameter(string invalidUprn)
        {
            Action testDelegate =
                () => _classUnderTest.GetHelpRequests(invalidUprn, null, null, null, null, false);
            testDelegate.Should().Throw<InvalidQueryParameter>().WithMessage("The UPRN given is invalid. UPRN must only have numeric characters");
        }

        [TestCase("1")]
        [TestCase("BA56 6Y")]
        [TestCase("B 7JI")]
        [TestCase("BA56 YTH")]
        [TestCase("BA 6YU")]
        [TestCase("BAY 6IY")]
        [TestCase("BA56 YH")]
        [TestCase("6A56 6YH")]
        [TestCase("B656 6YU")]
        [TestCase("BHHHH656 6YU")]
        [TestCase("BH6 6YUuuu")]
        [TestCase("Q33 8TH")]
        public void IfPostcodeIsInvalidThrowsInvalidQueryParameter(string invalidPostcode)
        {
            Action testDelegate =
                () => _classUnderTest.GetHelpRequests(null, invalidPostcode, null, null, null, false);
            testDelegate.Should().Throw<InvalidQueryParameter>().WithMessage("The Postcode given does not have a valid format");
        }

        [TestCase("E8 1JJ")]
        [TestCase("E8T 1JJ")]
        [TestCase("EY98 1JJ")]
        [TestCase("EH1A 1JJ")]
        [TestCase("EH8 1JJ")]
        [TestCase("E87 1JJ")]
        [TestCase("E87  1JJ")]
        [TestCase("e87  1Jj")]
        public void IfPostcodeIsValidCallTheGateway(string validPostcode)
        {
            _classUnderTest.GetHelpRequests(null, validPostcode, null, null, null, false);
            _fakeGateway.Verify(m => m.QueryHelpRequests(null, validPostcode,null, null, null, false));
        }
    }
}
