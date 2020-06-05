using System;
using System.Collections.Generic;
using CV19INeedHelp.UseCases.V1;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Helpers.V1;
using CV19INeedHelp.Models;
using CV19INeedHelp.Models.V1;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace CV19INeedHelpTest.UseCases.V1
{
    [TestFixture]
    public class DeliveryScheduleUseCaseTest
    {
        private IDeliveryScheduleUseCase _classUnderTest;
        private Mock<IINeedHelpGateway> _fakeGateway;
        private readonly List<ResidentSupportAnnex> response_data = new List<ResidentSupportAnnex>()
            {
                new ResidentSupportAnnex()
                {
                    Id = 3,
                    IsDuplicate = "FALSE",
                    OngoingFoodNeed = false,
                    OngoingPrescriptionNeed = null,
                    FormId = "5",
                    FormVersion = "V1",
                    DateTimeRecorded = DateTime.Now,
                    FirstName = "test",
                    LastName = "test",
                    DobMonth = null,
                    DobYear = null,
                    DobDay = null,
                    Postcode = "E8 4LW",
                    Uprn = "100021059008",
                    Ward = null,
                    AddressFirstLine = "test",
                    AddressSecondLine = "test",
                    AddressThirdLine = "test",
                    IsOnBehalf = null,
                    OnBehalfFirstName = null,
                    OnBehalfLastName = null,
                    ContactTelephoneNumber = "2072494986",
                    ContactMobileNumber = null,
                    EmailAddress = "TESTING***",
                    OnBehalfEmailAddress = null,
                    OnBehalfContactNumber = null,
                    RelationshipWithResident = null,
                    AnythingElse = null,
                    GpSurgeryDetails = null,
                    FoodNeed = null,
                    NumberOfPeopleInHouse = "1",
                    DaysWorthOfFood = null,
                    AnyFoodHouseholdCannotEat = null,
                    StrugglingToPayForFood = null,
                    IsPharmacistAbleToDeliver = null,
                    NameAddressPharmacist = null,
                    IsPackageOfCareAsc = null,
                    IsUrgentFoodRequired = null,
                    DaysWorthOfMedicine = null,
                    IsUrgentMedicineRequired = null,
                    IsAddressConfirmed = null,
                    IsHouseholdHelpAvailable = null,
                    IsUrgentFood = null,
                    IsUrgentPrescription = null,
                    AnyHelpAvailable = null,
                    IsAnyAgedUnder15 = null,
                    LastConfirmedFoodDelivery = DateTime.Now.AddDays(-7),
                    RecordStatus = "MASTER"
                }
            };

        [TestCase]
        // public void CanCallTheDatabaseCreateDeliveryScheduleMethodIfConfirmed()
        // {
        //     _fakeGateway = new Mock<IINeedHelpGateway>();
        //     _classUnderTest = new DeliveryScheduleUseCase(_fakeGateway.Object);
        //     var limit = 10;
        //     var confirmed = true;
        //     _fakeGateway.Setup(s => s.CreateDeliverySchedule(limit)).Returns(response_data);
        //     _classUnderTest.CreateDeliverySchedule(limit, confirmed);
        //     _fakeGateway.Verify(m => m.CreateDeliverySchedule(limit), Times.Once);
        //     _fakeGateway.Verify(m => m.CreateTemporaryDeliveryData(limit), Times.Never);
        // }
        
        [TestCase]
        public void CanCallTheDatabaseCreateTemporaryDeliveryDataMethodIfNotConfirmed()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new DeliveryScheduleUseCase(_fakeGateway.Object);
            var limit = 10;
            var spreadsheet = "test";
            var confirmed = false;
            _fakeGateway.Setup(s => s.CreateTemporaryDeliveryData(limit)).Returns(response_data);
            _classUnderTest.CreateDeliverySchedule(limit, confirmed);
            _fakeGateway.Verify(m => m.CreateTemporaryDeliveryData(limit), Times.Once);
            _fakeGateway.Verify(m => m.CreateDeliverySchedule(limit, spreadsheet), Times.Never);
        }
        
        [TestCase]
        public void CanCallTheFormatHelperFormatDraftOutputMethodIfNotConfirmed()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new DeliveryScheduleUseCase(_fakeGateway.Object);
            var limit = 10;
            var confirmed = false;
            _fakeGateway.Setup(s => s.CreateTemporaryDeliveryData(limit)).Returns(response_data);
            _classUnderTest.CreateDeliverySchedule(limit, confirmed);
        }
        
        [TestCase]
        public void CallingCreateDeliveryScheduleWithConfirmedFalseReturnsAListOfFoodDeliveryDraftObjects()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new DeliveryScheduleUseCase(_fakeGateway.Object);
            var limit = 10;
            var confirmed = false;
            _fakeGateway.Setup(s => s.CreateTemporaryDeliveryData(limit)).Returns(response_data);
            var response = _classUnderTest.CreateDeliverySchedule(limit, confirmed);
            response.Should().BeOfType(typeof(List<FoodDeliveryDraft>));
        }
    }
}