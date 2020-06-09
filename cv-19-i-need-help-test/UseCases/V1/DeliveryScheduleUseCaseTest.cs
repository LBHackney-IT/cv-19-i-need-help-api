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
        private Mock<IDriveHelper> _fakeDriveHelper;
        private readonly List<ResidentSupportAnnex> _responseData = new List<ResidentSupportAnnex>()
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

        private readonly List<DeliveryReportItem> _reportData = new List<DeliveryReportItem>()
        {
            new DeliveryReportItem()
            {
                AnnexId = 3,
                NumberOfPackages = 1,
                AnyFoodHouseholdCannotEat = "Test",
                BatchId = 1,
                FullName = "Test",
                FullAddress = "Test",
                Postcode = "Test",
                Uprn = "Test",
                TelephoneNumber = "Test",
                MobileNumber = "Test",
                DeliveryDate = DateTime.Now,
                LastConfirmedDeliveryDate = DateTime.Now.AddDays(-7),
                DeliveryNotes = "Test"
            }
        };

        [TestCase]
        public void CanCallTheDatabaseCreateDeliveryScheduleMethodIfConfirmed()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _fakeDriveHelper = new Mock<IDriveHelper>();
            _classUnderTest = new DeliveryScheduleUseCase(_fakeGateway.Object, _fakeDriveHelper.Object);
            var limit = 10;
            var spreadsheet = "test";
            _fakeDriveHelper.Setup(x => x.CreateSpreadsheet(It.IsAny<string>())).Returns(spreadsheet);
            _fakeGateway.Setup(s => s.CreateDeliverySchedule(limit,spreadsheet)).Returns(_reportData);
            _classUnderTest.CreateDeliverySchedule(limit,true);
            _fakeGateway.Verify(m => m.CreateDeliverySchedule(limit,spreadsheet), Times.Once);
            _fakeGateway.Verify(m => m.CreateTemporaryDeliveryData(limit), Times.Never);
        }
        
        [TestCase]
        public void CanCallTheDatabaseCreateTemporaryDeliveryDataMethodIfNotConfirmed()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new DeliveryScheduleUseCase(_fakeGateway.Object, new DriveHelper());
            var limit = 10;
            var spreadsheet = "test";
            _fakeGateway.Setup(s => s.CreateTemporaryDeliveryData(limit)).Returns(_responseData);
            _classUnderTest.CreateDeliverySchedule(limit, false);
            _fakeGateway.Verify(m => m.CreateTemporaryDeliveryData(limit), Times.Once);
            _fakeGateway.Verify(m => m.CreateDeliverySchedule(limit, spreadsheet), Times.Never);
        }
        
        [TestCase]
        public void CanCallTheFormatHelperFormatDraftOutputMethodIfNotConfirmed()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new DeliveryScheduleUseCase(_fakeGateway.Object, new DriveHelper());
            var limit = 10;
            _fakeGateway.Setup(s => s.CreateTemporaryDeliveryData(limit)).Returns(_responseData);
            _classUnderTest.CreateDeliverySchedule(limit, false);
        }
        
        [TestCase]
        public void CallingCreateDeliveryScheduleWithConfirmedFalseReturnsAListOfFoodDeliveryDraftObjects()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new DeliveryScheduleUseCase(_fakeGateway.Object, new DriveHelper());
            var limit = 10;
            _fakeGateway.Setup(s => s.CreateTemporaryDeliveryData(limit)).Returns(_responseData);
            var response = _classUnderTest.CreateDeliverySchedule(limit, false);
            response.Should().BeOfType(typeof(List<FoodDeliveryDraft>));
        }
    }
}