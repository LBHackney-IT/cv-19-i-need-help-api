using System;
using System.Collections.Generic;
using CV19INeedHelp.UseCases.V1;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using CV19INeedHelp.UseCases.V1;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace CV19INeedHelpTest.UseCases.V1
{
    [TestFixture]
    public class DeliveryScheduleUseCaseTest
    {
        private readonly IDeliveryScheduleUseCase _classUnderTest;
        private readonly Mock<IINeedHelpGateway> _fakeGateway;

        public DeliveryScheduleUseCaseTest()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new DeliveryScheduleUseCase(_fakeGateway.Object);
        }
        
        [TestCase]
        public void CanCallTheDatabaseCreateDeliveryScheduleMethodIfConfirmed()
        {
            var limit = 10;
            var confirmed = true;
            _classUnderTest.CreateDeliverySchedule(limit, confirmed);
            _fakeGateway.Verify(m => m.CreateDeliverySchedule(limit), Times.Once);
            _fakeGateway.Verify(m => m.CreateTemporaryDeliveryData(limit), Times.Never);
        }
        
        public void CanCallTheDatabaseCreateTemporaryDeliveryDataMethodIfNotConfirmed()
        {
            var limit = 10;
            var confirmed = false;
            _classUnderTest.CreateDeliverySchedule(limit, confirmed);
            _fakeGateway.Verify(m => m.CreateTemporaryDeliveryData(limit), Times.Once);
            _fakeGateway.Verify(m => m.CreateDeliverySchedule(limit), Times.Never);
        }


        // [TestCase("123", "test", false)]
        // public void GetsAListOfHelpRequestsIfValidUprnIsProvided(string uprn, string postcode, bool isMaster)
        // {
        //     _fakeGateway.Setup(x => x.GetHelpRequestsForUprn(uprn, postcode, isMaster)).Returns(response_data);
        //     var response = _classUnderTest.GetHelpRequests(uprn, postcode, isMaster);
        //     Assert.AreEqual(response, response_data);
        //     response.Should().Equal(response_data);
        // }

        
        
    }
}