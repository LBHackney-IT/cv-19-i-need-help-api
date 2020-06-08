using System;
using System.Linq;
using AutoFixture;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Helpers.V1;
using CV19INeedHelp.Models.V1;
using FluentAssertions;
using NUnit.Framework;

namespace CV19INeedHelpTest.Models.V1
{
    [TestFixture]
    public class ResidentSupportAnnexTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void ResidentSupportAnnexShouldHaveCorrectProperties()
        {
            Type type = typeof(ResidentSupportAnnex);
            type.GetProperties().Length.Should().Be(50);
            ResidentSupportAnnex classUnderTest = new ResidentSupportAnnex
            {
                Id = 1,
                IsDuplicate = "FALSE",
                OngoingFoodNeed = true,
                OngoingPrescriptionNeed = true,
                FormId = "1",
                FormVersion = "1",
                DateTimeRecorded = DateTime.Now,
                FirstName = "Test",
                LastName = "Test",
                DobDay = "01",
                DobMonth = "02",
                DobYear = "1992",
                Postcode = "E5",
                Uprn = "100435",
                AddressFirstLine = "test",
                AddressSecondLine = "test",
                AddressThirdLine = "test",
                Ward = "test",
                ContactTelephoneNumber = "123",
                ContactMobileNumber = "123",
                EmailAddress = "test",
                IsOnBehalf = false,
                OnBehalfFirstName = "test",
                OnBehalfLastName = "test",
                OnBehalfEmailAddress = "test",
                OnBehalfContactNumber = "test",
                RelationshipWithResident = "test",
                AnythingElse = "test",
                GpSurgeryDetails = "test",
                FoodNeed = true,
                NumberOfPeopleInHouse = "1",
                DaysWorthOfFood = "0",
                AnyFoodHouseholdCannotEat = "test",
                StrugglingToPayForFood = true,
                IsPharmacistAbleToDeliver = false,
                IsPackageOfCareAsc = true,
                NameAddressPharmacist = "test",
                IsUrgentFoodRequired = false,
                DaysWorthOfMedicine = "1",
                IsUrgentMedicineRequired = false,
                IsAddressConfirmed = true,
                IsHouseholdHelpAvailable = true,
                IsUrgentFood = false,
                IsUrgentPrescription = true,
                AnyHelpAvailable = true,
                IsAnyAgedUnder15 = false,
                LastConfirmedFoodDelivery = DateTime.Now,
                RecordStatus = "MASTER",
                DeliveryNotes = "Test",
                CaseNotes = "Test"
            };
            Assert.That(classUnderTest, Has.Property("Id").InstanceOf(typeof(Int32)));
            Assert.That(classUnderTest, Has.Property("IsDuplicate").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("OngoingFoodNeed").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("OngoingPrescriptionNeed").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("FormId").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("FormVersion").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("DateTimeRecorded").InstanceOf(typeof(DateTime)));
            Assert.That(classUnderTest, Has.Property("FirstName").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("LastName").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("DobDay").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("DobMonth").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("DobYear").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("Postcode").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("Uprn").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("AddressFirstLine").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("AddressSecondLine").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("AddressThirdLine").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("Ward").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("ContactTelephoneNumber").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("ContactMobileNumber").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("EmailAddress").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("IsOnBehalf").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("OnBehalfFirstName").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("OnBehalfLastName").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("OnBehalfEmailAddress").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("OnBehalfContactNumber").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("RelationshipWithResident").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("AnythingElse").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("GpSurgeryDetails").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("FoodNeed").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("NumberOfPeopleInHouse").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("DaysWorthOfFood").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("AnyFoodHouseholdCannotEat").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("StrugglingToPayForFood").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("IsPharmacistAbleToDeliver").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("IsPackageOfCareAsc").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("NameAddressPharmacist").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("IsUrgentFoodRequired").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("DaysWorthOfMedicine").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("IsUrgentMedicineRequired").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("IsAddressConfirmed").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("IsHouseholdHelpAvailable").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("IsUrgentFood").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("IsUrgentPrescription").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("AnyHelpAvailable").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("IsAnyAgedUnder15").InstanceOf(typeof(bool)));
            Assert.That(classUnderTest, Has.Property("LastConfirmedFoodDelivery").InstanceOf(typeof(DateTime)));
            Assert.That(classUnderTest, Has.Property("RecordStatus").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("DeliveryNotes").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("CaseNotes").InstanceOf(typeof(string)));
        }

        [Test]
        public void ResidentSupportAnnexMapsToV1ResponseObjectWithAllFieldIdentical()
        {
            var classUnderTest = _fixture.Create<ResidentSupportAnnex>();

            var mappedResponse = classUnderTest.ToResponse();
            mappedResponse.Should().BeEquivalentTo(classUnderTest);
            mappedResponse.GetType().Should().Be<ResidentSupportAnnexResponse>();
        }

        [Test]
        public void ListOfResidentSupportAnnexMapsToListOfV1ResponseObjectWithAllFieldIdentical()
        {
            var classUnderTest = _fixture.CreateMany<ResidentSupportAnnex>();

            var mappedResponses = classUnderTest.ToResponse();
            mappedResponses.Count.Should().Be(classUnderTest.Count());
            mappedResponses.ForEach(response =>
            {
                classUnderTest.Should().ContainEquivalentOf(response);
                response.GetType().Should().Be<ResidentSupportAnnexResponse>();
            });
        }
    }
}