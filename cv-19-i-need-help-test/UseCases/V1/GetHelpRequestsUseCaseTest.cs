using System;
using System.Collections.Generic;
using CV19INeedHelp.UseCases.V1;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace CV19INeedHelpTest.UseCases.V1
{
    [TestFixture]
    public class GetHelpRequestsUseCaseTest
    {
        private readonly IGetHelpRequestsUseCase _classUnderTest;
        private readonly Mock<IINeedHelpGateway> _fakeGateway;
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
                    LastConfirmedFoodDelivery = null,
                    RecordStatus = "MASTER"
                }
            };

        public GetHelpRequestsUseCaseTest()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new GetHelpRequestsUseCase(_fakeGateway.Object);
        }
        
        [TestCase]
        public void CanCallTheDatabaseGetAllMethod()
        {
            string uprn = "123";
            string exceptions = "false";
            _classUnderTest.GetHelpRequests(uprn, exceptions);
            _fakeGateway.Verify(m => m.GetHelpRequestsForUprn(uprn, exceptions), Times.Once);
        }
        
        [TestCase]
        public void CanRaiseErrorIfNoUprnorExceptionFlagIsProvided()
        {
            string uprn = "";
            string exceptions = "";
            Assert.Throws<ArgumentException>(() =>_classUnderTest.GetHelpRequests(uprn, exceptions));
        }
        
        [TestCase("123", "false")]
        public void GetsAListOfHelpRequestsIfValidUprnIsProvided(string uprn, string exceptions)
        {
            _fakeGateway.Setup(x => x.GetHelpRequestsForUprn(uprn, exceptions)).Returns(response_data);
            var response = _classUnderTest.GetHelpRequests(uprn, exceptions);
            Assert.AreEqual(response, response_data);
            response.Should().Equal(response_data);
        }

        
        
    }
}