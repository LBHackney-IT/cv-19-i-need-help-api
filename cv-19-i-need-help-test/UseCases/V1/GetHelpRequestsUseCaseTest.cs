using System.Collections.Generic;
using CV19INeedHelp.UseCases.V1;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using NUnit.Framework;
using Moq;

namespace CV19INeedHelpTest.UseCases.V1
{
    [TestFixture]
    public class GetHelpRequestsUseCaseTest
    {
        private readonly IGetHelpRequestsUseCase _classUnderTest;
        private readonly Mock<IOrganisationVolunteerGateway> _fakeGateway;

        public GetHelpRequestsUseCaseTest()
        {
            _fakeGateway = new Mock<IOrganisationVolunteerGateway>();
            _classUnderTest = new GetHelpRequestsUseCase(_fakeGateway.Object);
        }
        
        [TestCase]
        public void CanCallTheDatabaseGetAllMethod()
        {
            _classUnderTest.GetHelpRequests();
            _fakeGateway.Verify(m => m.GetAllHelpRequests(), Times.Once);
        }
    }
}