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
    public class DeliveryBatchTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void DeliveryBatchShouldHaveCorrectProperties()
        {
            Type type = typeof(DeliveryBatch);
            type.GetProperties().Length.Should().Be(4);
            DeliveryBatch classUnderTest = new DeliveryBatch()
            {
                Id = 1,
                DeliveryDate = DateTime.Now,
                DeliveryPackages = 10,
                ReportFileId = "Test"
            };
            Assert.That(classUnderTest, Has.Property("Id").InstanceOf(typeof(Int32)));
            Assert.That(classUnderTest, Has.Property("DeliveryDate").InstanceOf(typeof(DateTime)));
            Assert.That(classUnderTest, Has.Property("DeliveryPackages").InstanceOf(typeof(int)));
            Assert.That(classUnderTest, Has.Property("ReportFileId").InstanceOf(typeof(string)));
        }

        [Test]
        public void DeliveryBatchMapsToV1ResponseObjectWithAllFieldIdentical()
        {
            var classUnderTest = _fixture.Create<DeliveryBatch>();

            var mappedResponse = classUnderTest.ToResponse();
            mappedResponse.Should().BeEquivalentTo(classUnderTest);
            mappedResponse.GetType().Should().Be<DeliveryBatchResponse>();
        }
    }
}