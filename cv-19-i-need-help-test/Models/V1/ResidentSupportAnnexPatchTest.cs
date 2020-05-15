using System;
using CV19INeedHelp.Models.V1;
using FluentAssertions;
using NUnit.Framework;

namespace CV19INeedHelpTest.Models.V1
{
    [TestFixture]
    public class ResidentSupportAnnexPatchTest
    {
        [TestCase]
        public void ResidentSupportAnnexPatchShouldHaveCorrectProperties()
        {
            Type type = typeof(ResidentSupportAnnexPatch);
            type.GetProperties().Length.Should().Be(12);
            ResidentSupportAnnexPatch _classUnderTest = new ResidentSupportAnnexPatch
            {
                IsDuplicate = "FALSE",
                OngoingFoodNeed = true,
                DobDay = "01",
                DobMonth = "02",
                DobYear = "1992",
                ContactTelephoneNumber = "123",
                ContactMobileNumber = "123",
                NumberOfPeopleInHouse = "1",
                LastConfirmedFoodDelivery = DateTime.Now,
                RecordStatus = "MASTER",
                DeliveryNotes = "Test",
                CaseNotes = "Test"
            };
            Assert.That(_classUnderTest, Has.Property("IsDuplicate").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("OngoingFoodNeed").InstanceOf(typeof(bool)));
            Assert.That(_classUnderTest, Has.Property("DobDay").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("DobMonth").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("DobYear").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("ContactTelephoneNumber").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("ContactMobileNumber").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("NumberOfPeopleInHouse").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("LastConfirmedFoodDelivery").InstanceOf(typeof(DateTime)));
            Assert.That(_classUnderTest, Has.Property("RecordStatus").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("DeliveryNotes").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("CaseNotes").InstanceOf(typeof(string)));
        }
    }
}