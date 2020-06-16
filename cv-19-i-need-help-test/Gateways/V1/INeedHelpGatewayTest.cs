using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CV19INeedHelp.Data.V1;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CV19INeedHelpTest.Gateways.V1
{
    [TestFixture]
    public class INeedHelpGatewayTest
    {
        private INeedHelpGateway _classUnderTest;
        private readonly Fixture _fixture = new Fixture();
        private Cv19SupportDbContext _context;
        private string _connectionString;
        
        [SetUp]
        public void SetUp()
        {
            _connectionString = Environment.GetEnvironmentVariable("CV_19_DB_CONNECTION");
            const string connectionString = "Host=localhost;Database=i-need-help-test;Username=postgres;Password=mypassword";
            _context= new Cv19SupportDbContext(_connectionString ?? connectionString);
            ClearResidentSupportAnnexTable();
            if (_connectionString == null) Environment.SetEnvironmentVariable("CV_19_DB_CONNECTION", connectionString);
            _classUnderTest = new INeedHelpGateway(_context);
        }
        
        [TearDown]
        public void ResetDb()
        {
            Environment.SetEnvironmentVariable("CV_19_DB_CONNECTION", _connectionString);
            ClearResidentSupportAnnexTable();
            ClearDeliveryBatchTable();
            ClearDeliveryReportDataTable();
        }

        [Test]
        public void WhenCreatingADeliveryScheduleReturnAListOfDeliveriesMeetingRequiredCriteria()
        {
            var annexRecord = _fixture.Create<ResidentSupportAnnex>();
            //annex record should not have a confirmed delivery
            annexRecord.LastConfirmedFoodDelivery = null;
            //annex record should have an ongoing food need of true
            annexRecord.OngoingFoodNeed = true;
            InsertIntoResidentSupportAnnexTable(annexRecord);
            var response = _classUnderTest.CreateDeliverySchedule(10, "test");
            response.Count.Should().Be(1);
            response.First().AnnexId.Should().Be(annexRecord.Id);
        }

        [Test]
        public void WhenSuppliedADateReturnsTheFirstRecordWithThatDate()
        {
            var batch = _fixture.Create<DeliveryBatch>();
            batch.DeliveryDate = DateTime.Now.AddDays(1);
            InsertIntoDeliveryBatchTable(batch);
            var response = _classUnderTest.FindExistingBatchForDate(DateTime.Now.AddDays(1));
            response.Should().BeEquivalentTo(batch);
        }
        
        [Test]
        public void WhenSuppliedADateNotRecordedReturnsNull()
        {
            var batch = _fixture.Create<DeliveryBatch>();
            batch.DeliveryDate = DateTime.Now.AddDays(1);
            InsertIntoDeliveryBatchTable(batch);
            var response = _classUnderTest.FindExistingBatchForDate(DateTime.Now);
            response.Should().BeNull();
        }

        [Test]
        public void WhenIdOfExistingBatchRecordIsSuppliedReturnsWithBatchRecord()
        {
            var batch = _fixture.Create<DeliveryBatch>();
            InsertIntoDeliveryBatchTable(batch);
            var response = _classUnderTest.GetBatchById(batch.Id);
            response.Should().BeEquivalentTo(batch);
        }
        
        [Test]
        public void WhenIdIsSuppliedThatDoesNotExistReturnsWithNull()
        {
            var batch = _fixture.Create<DeliveryBatch>();
            InsertIntoDeliveryBatchTable(batch);
            var response = _classUnderTest.GetBatchById(batch.Id+10);
            response.Should().BeNull();
        }

        private void ClearResidentSupportAnnexTable()
        {
            var addedEntities = _context.ResidentSupportAnnex;
            _context.ResidentSupportAnnex.RemoveRange(addedEntities);
            _context.SaveChanges();
        }

        private void ClearDeliveryBatchTable()
        {
            var addedEntities = _context.DeliveryBatch;
            _context.DeliveryBatch.RemoveRange(addedEntities);
            _context.SaveChanges();
        }

        private void ClearDeliveryReportDataTable()
        {
            var addedEntities = _context.DeliveryReportData;
            _context.DeliveryReportData.RemoveRange(addedEntities);
            _context.SaveChanges();
        }

        private void InsertIntoResidentSupportAnnexTable(ResidentSupportAnnex request)
        {
            _context.ResidentSupportAnnex.Add(request);
            _context.SaveChanges();
        }
        
        private void InsertIntoDeliveryBatchTable(DeliveryBatch batchRecord)
        {
            _context.DeliveryBatch.Add(batchRecord);
            _context.SaveChanges();
        }
    }
}