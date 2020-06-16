using System;
using System.Collections.Generic;
using System.Linq;
using CV19INeedHelp.UseCases.V1;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models;
using CV19INeedHelp.Models.V1;
using AutoFixture;
using CV19INeedHelp.Boundary.V1.Responses;
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
        private readonly Fixture _fixture = new Fixture();
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
                    LastConfirmedFoodDelivery = null,
                    RecordStatus = "MASTER"
                }
            };

        public GetHelpRequestsUseCaseTest()
        {
            _fakeGateway = new Mock<IINeedHelpGateway>();
            _classUnderTest = new GetHelpRequestsUseCase(_fakeGateway.Object);
        }

        [TestCase("123", "test", false)]
        [TestCase("327", "another_test", true)]
        public void CanCallTheDatabaseGetAllMethod(string uprn, string postcode, bool isMaster)
        {
            _fakeGateway.Setup(m => m.GetHelpRequestsForUprn(uprn, postcode, isMaster))
                .Returns(new List<ResidentSupportAnnex>()).Verifiable();
            _classUnderTest.GetHelpRequests(uprn, postcode, isMaster);
            _fakeGateway.Verify();
        }

        [TestCase("123", "test", false)]
        [TestCase("327", "another_test", true)]
        public void GetsAListOfHelpRequestsIfValidUprnIsProvided(string uprn, string postcode, bool isMaster)
        {
            _fakeGateway.Setup(x => x.GetHelpRequestsForUprn(uprn, postcode, isMaster)).Returns(_responseData);
            var response = _classUnderTest.GetHelpRequests(uprn, postcode, isMaster);

            response.Should().BeEquivalentTo(_responseData);
        }

        [Test]
        public void GetsSummaryDataWhenRequested()
        {
            var summaryResponse = _fixture.Create <AnnexSummaryResponse>();
            _fakeGateway.Setup(x => x.GetHelpRequestsSummary()).Returns(summaryResponse);
            var response = _classUnderTest.GetHelpRequestsSummary();
            response.ActiveCases.Should().Be(summaryResponse.ActiveCases);
        }
    }
}
