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
    public class DeliveryReportItemTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void DeliveryReportItemShouldHaveCorrectProperties()
        {
            Type type = typeof(DeliveryReportItem);
            type.GetProperties().Length.Should().Be(14);
            DeliveryReportItem classUnderTest = new DeliveryReportItem
            {
                Id = 1,
                AnnexId = 10,
                NumberOfPackages = 10,
                FullName = "Test",
                TelephoneNumber = "Test",
                MobileNumber = "Test",
                FullAddress = "Test",
                Postcode = "E5",
                Uprn = "100435",
                AnyFoodHouseholdCannotEat = "test",
                DeliveryNotes = "Test",
                
            };
            Assert.That(classUnderTest, Has.Property("Id").InstanceOf(typeof(Int32)));
            Assert.That(classUnderTest, Has.Property("AnnexId").InstanceOf(typeof(Int32)));
            Assert.That(classUnderTest, Has.Property("NumberOfPackages").InstanceOf(typeof(Int32)));
            Assert.That(classUnderTest, Has.Property("FullName").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("TelephoneNumber").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("MobileNumber").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("FullAddress").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("Postcode").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("Uprn").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("AnyFoodHouseholdCannotEat").InstanceOf(typeof(string)));
            Assert.That(classUnderTest, Has.Property("DeliveryNotes").InstanceOf(typeof(string)));
        }

        [Test]
        public void DeliveryReportItemMapsToV1ResponseObjectWithAllFieldIdentical()
        {
            var classUnderTest = _fixture.Create<DeliveryReportItem>();

            var mappedResponse = classUnderTest.ToResponse();
            mappedResponse.Should().BeEquivalentTo(classUnderTest);
            mappedResponse.GetType().Should().Be<DeliveryReportItemResponse>();
        }

        [Test]
        public void ListOfDeliveryReportItemsMapsToListOfV1ResponseObjectWithAllFieldIdentical()
        {
            var classUnderTest = _fixture.CreateMany<DeliveryReportItem>();

            var mappedResponses = classUnderTest.ToResponse();
            mappedResponses.Count.Should().Be(classUnderTest.Count());
            mappedResponses.ForEach(response =>
            {
                classUnderTest.Should().ContainEquivalentOf(response);
                response.GetType().Should().Be<DeliveryReportItemResponse>();
            });
        }
    }
}