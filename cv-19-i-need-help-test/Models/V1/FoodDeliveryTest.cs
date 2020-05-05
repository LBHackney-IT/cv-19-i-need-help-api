using System;
using CV19INeedHelp.Models.V1;
using FluentAssertions;
using NUnit.Framework;

namespace CV19INeedHelpTest.Models.V1
{
    [TestFixture]
    public class FoodDeliveryTest
    {
        [TestCase]
        public void FoodDeliveryRecordShouldHaveCorrectProperties()
        {
            Type type = typeof(FoodDelivery);
            type.GetProperties().Length.Should().Be(10);
            FoodDelivery _classUnderTest = new FoodDelivery
            {
                Id = 1,
                AnnexId = 123,
                ScheduledDeliveryDate = DateTime.Now,
                DeliveryConfirmed = true,
                ReasonForNonDelivery = "TEST",
                UPRN = "TEST",
                IsThisFirstDelivery = false,
                RepeatDelivery = 1,
                HouseholdSize = 1,
                FoodPackages = 1
            };
            Assert.That(_classUnderTest, Has.Property("Id").InstanceOf(typeof(Int32)));
            Assert.That(_classUnderTest, Has.Property("AnnexId").InstanceOf(typeof(Int32)));
            Assert.That(_classUnderTest, Has.Property("ScheduledDeliveryDate").InstanceOf(typeof(DateTime)));
            Assert.That(_classUnderTest, Has.Property("DeliveryConfirmed").InstanceOf(typeof(bool)));
            Assert.That(_classUnderTest, Has.Property("ReasonForNonDelivery").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("UPRN").InstanceOf(typeof(string)));
            Assert.That(_classUnderTest, Has.Property("IsThisFirstDelivery").InstanceOf(typeof(bool)));
            Assert.That(_classUnderTest, Has.Property("HouseholdSize").InstanceOf(typeof(Int32)));
            Assert.That(_classUnderTest, Has.Property("FoodPackages").InstanceOf(typeof(Int32)));
        }
    }
}