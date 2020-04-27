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
        private readonly Mock<IINeedHelpGateway> _fakeGateway;

        public GetHelpRequestsUseCaseTest()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new GetHelpRequestsUseCase(_fakeGateway.Object);
        }
        
        [TestCase]
        public void CanCallTheDatabaseGetAllMethod()
        {
            string uprn = "";
            _classUnderTest.GetHelpRequests(uprn);
            _fakeGateway.Verify(m => m.GetHelpRequestsForUprn(uprn), Times.Once);
        }
    }
}