using System;
using System.Linq;
using AutoFixture;
using CV19INeedHelp.Boundary.V2.Responses;
using CV19INeedHelp.Helpers.V2;
using CV19INeedHelp.Models.V1;
using CV19INeedHelpTest.TestHelpers;
using FluentAssertions;
using NUnit.Framework;

namespace CV19INeedHelpTest.Models.V2
{
    public class ResidentSupportAnnexTest
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            CustomizeFixture.V2ResidentResponseParsable(_fixture);
        }

        [Test]
        public void ResidentSupportAnnexMapsToV2ResponseObject()
        {
            var classUnderTest = _fixture.Create<ResidentSupportAnnex>();

            var mappedResponse = classUnderTest.ToResponse();
            mappedResponse.GetType().Should().Be<ResidentSupportAnnexResponse>();
            AssertAllFieldsHaveCorrectlyMapped(mappedResponse, classUnderTest);
        }

        [Test]
        public void ResidentSupportAnnexMapToV2WithMinimalInformation()
        {
            var classUnderTest = new ResidentSupportAnnex { Id = 1 };
            classUnderTest.ToResponse().Should().BeEquivalentTo(new ResidentSupportAnnexResponse
            {
                Id = 1
            });
        }

        [Test]
        public void ListOfResidentSupportAnnexMapsToListOfV2ResponseObjectsWithAllFieldIdentical()
        {
            var classUnderTest = _fixture.CreateMany<ResidentSupportAnnex>().ToList();

            var mappedResponses = classUnderTest.ToResponse();
            mappedResponses.Count.Should().Be(classUnderTest.Count());
            mappedResponses.ForEach(response =>
            {
                var original = classUnderTest.First(r => r.Id == response.Id);
                AssertAllFieldsHaveCorrectlyMapped(response, original);
                response.GetType().Should().Be<ResidentSupportAnnexResponse>();
            });
        }

        private static void AssertAllFieldsHaveCorrectlyMapped(ResidentSupportAnnexResponse mappedResponse,
            ResidentSupportAnnex classUnderTest)
        {
            mappedResponse.Id.Should().Be(classUnderTest.Id);
            mappedResponse.IsDuplicate.Should().Be(classUnderTest.IsDuplicate);
            mappedResponse.OngoingFoodNeed.Should().Be(classUnderTest.OngoingFoodNeed);
            mappedResponse.OngoingPrescriptionNeed.Should().Be(classUnderTest.OngoingPrescriptionNeed);
            mappedResponse.FormId.Should().Be(int.Parse(classUnderTest.FormId));
            mappedResponse.FormVersion.Should().Be(classUnderTest.FormVersion);
            mappedResponse.DateTimeRecorded.Should().Be(classUnderTest.DateTimeRecorded);
            mappedResponse.FirstName.Should().Be(classUnderTest.FirstName);
            mappedResponse.LastName.Should().Be(classUnderTest.LastName);
            mappedResponse.DateOfBirth.Should().Be(new DateTime(1960, 11, 13));
            mappedResponse.Postcode.Should().Be(classUnderTest.Postcode);
            mappedResponse.Uprn.Should().Be(classUnderTest.Uprn);
            mappedResponse.Ward.Should().Be(classUnderTest.Ward);
            mappedResponse.AddressFirstLine.Should().Be(classUnderTest.AddressFirstLine);
            mappedResponse.AddressSecondLine.Should().Be(classUnderTest.AddressSecondLine);
            mappedResponse.AddressThirdLine.Should().Be(classUnderTest.AddressThirdLine);
            mappedResponse.ContactTelephoneNumber.Should().Be(classUnderTest.ContactTelephoneNumber);
            mappedResponse.ContactMobileNumber.Should().Be(classUnderTest.ContactMobileNumber);
            mappedResponse.EmailAddress.Should().Be(classUnderTest.EmailAddress);
            mappedResponse.IsOnBehalf.Should().Be(classUnderTest.IsOnBehalf);
            mappedResponse.OnBehalfFirstName.Should().Be(classUnderTest.OnBehalfFirstName);
            mappedResponse.OnBehalfLastName.Should().Be(classUnderTest.OnBehalfLastName);
            mappedResponse.OnBehalfEmailAddress.Should().Be(classUnderTest.OnBehalfEmailAddress);
            mappedResponse.OnBehalfContactNumber.Should().Be(classUnderTest.OnBehalfContactNumber);
            mappedResponse.RelationshipWithResident.Should().Be(classUnderTest.RelationshipWithResident);
            mappedResponse.AnythingElse.Should().Be(classUnderTest.AnythingElse);
            mappedResponse.GpSurgeryDetails.Should().Be(classUnderTest.GpSurgeryDetails);
            mappedResponse.FoodNeed.Should().Be(classUnderTest.FoodNeed);
            mappedResponse.NumberOfPeopleInHouse.Should().Be(int.Parse(classUnderTest.NumberOfPeopleInHouse));
            mappedResponse.DaysWorthOfFood.Should().Be(int.Parse(classUnderTest.DaysWorthOfFood));
            mappedResponse.AnyFoodHouseholdCannotEat.Should().Be(classUnderTest.AnyFoodHouseholdCannotEat);
            mappedResponse.StrugglingToPayForFood.Should().Be(classUnderTest.StrugglingToPayForFood);
            mappedResponse.IsPharmacistAbleToDeliver.Should().Be(classUnderTest.IsPharmacistAbleToDeliver);
            mappedResponse.NameAddressPharmacist.Should().Be(classUnderTest.NameAddressPharmacist);
            mappedResponse.IsPackageOfCareAsc.Should().Be(classUnderTest.IsPackageOfCareAsc);
            mappedResponse.IsUrgentFoodRequired.Should().Be(classUnderTest.IsUrgentFoodRequired);
            mappedResponse.DaysWorthOfMedicine.Should().Be(int.Parse(classUnderTest.DaysWorthOfMedicine));
            mappedResponse.IsUrgentMedicineRequired.Should().Be(classUnderTest.IsUrgentMedicineRequired);
            mappedResponse.IsAddressConfirmed.Should().Be(classUnderTest.IsAddressConfirmed);
            mappedResponse.IsHouseholdHelpAvailable.Should().Be(classUnderTest.IsHouseholdHelpAvailable);
            mappedResponse.IsUrgentFood.Should().Be(classUnderTest.IsUrgentFood);
            mappedResponse.IsUrgentPrescription.Should().Be(classUnderTest.IsUrgentPrescription);
            mappedResponse.AnyHelpAvailable.Should().Be(classUnderTest.AnyHelpAvailable);
            mappedResponse.IsAnyAgedUnder15.Should().Be(classUnderTest.IsAnyAgedUnder15);
            mappedResponse.LastConfirmedFoodDelivery.Should().Be(classUnderTest.LastConfirmedFoodDelivery);
            mappedResponse.RecordStatus.Should().Be(classUnderTest.RecordStatus);
            mappedResponse.DeliveryNotes.Should().Be(classUnderTest.DeliveryNotes);
            mappedResponse.CaseNotes.Should().Be(classUnderTest.CaseNotes);
        }
    }
}