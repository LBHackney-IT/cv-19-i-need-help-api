using System;
using CV19INeedHelp.Models.V1;
using FluentAssertions;
using NUnit.Framework;

namespace CV19INeedHelpTest.Models.V1
{
    [TestFixture]
    public class FoodDeliveryDraftTest
    {
        [TestCase]
        public void FoodDeliveryDraftShouldHaveCorrectProperties()
        {
            Type type = typeof(FoodDeliveryDraft);
            type.GetProperties().Length.Should().Be(6);
            FoodDeliveryDraft _classUnderTest = new FoodDeliveryDraft
            {
                AnnexId = 1,
                Name = "Testing",
                Address = "Testing",
                Postcode = "Test",
                Uprn = "12345678",
                DeliveryDate = DateTime.Now
            };
            Assert.That(_classUnderTest, Has.Property("AnnexId").InstanceOf(typeof(Int32)));
            Assert.That(_classUnderTest, Has.Property("Name").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("Address").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("Postcode").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("Uprn").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("DeliveryDate").InstanceOf(typeof(DateTime)));
        }
    }
}