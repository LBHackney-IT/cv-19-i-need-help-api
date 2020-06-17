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
            ClearBankHolidays();
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

        [Test]
        public void WhenSummaryMethodIsCalledItReturnsActiveCases()
        {
            var rec1 = _fixture.Create <ResidentSupportAnnex>();
            rec1.OngoingFoodNeed = true;
            rec1.IsDuplicate = "FALSE";
            rec1.RecordStatus = "MASTER";
            InsertIntoResidentSupportAnnexTable(rec1);
            var rec2 = _fixture.Create <ResidentSupportAnnex>();
            rec2.OngoingFoodNeed = true;
            rec2.IsDuplicate = "FALSE";
            rec2.RecordStatus = "MASTER";
            InsertIntoResidentSupportAnnexTable(rec2);
            var rec3 = _fixture.Create <ResidentSupportAnnex>();
            rec3.OngoingFoodNeed = false;
            rec3.IsDuplicate = "TRUE";
            rec3.RecordStatus = "DUPLICATE";
            InsertIntoResidentSupportAnnexTable(rec3);
            var response = _classUnderTest.GetHelpRequestsSummary();
            response.ActiveCases.Should().Be(2);
        }

        [Test]
        public void CanGetNextBankHolidayFromDBIfPresent()
        {
            var holiday1 = _fixture.Create<BankHoliday>();
            holiday1.Date = DateTime.Today.AddDays(1);
            InsertIntoBankHolidaysTable(holiday1);
            var holiday2 = _fixture.Create<BankHoliday>();
            holiday2.Date = DateTime.Today.AddDays(2);
            InsertIntoBankHolidaysTable(holiday2);
            var response = _classUnderTest.GetNextBankHoliday(DateTime.Today);
            response.Date.Should().Be(DateTime.Today.AddDays(1));
        }
        
        [Test]
        public void DoesNotReturnBankHolidayAlreadyGone()
        {
            var holiday1 = _fixture.Create<BankHoliday>();
            holiday1.Date = DateTime.Today.AddDays(1);
            InsertIntoBankHolidaysTable(holiday1);
            var holiday2 = _fixture.Create<BankHoliday>();
            holiday2.Date = DateTime.Today.AddDays(2);
            InsertIntoBankHolidaysTable(holiday2);
            var holiday3 = _fixture.Create<BankHoliday>();
            holiday3.Date = DateTime.Today.AddDays(-1);
            InsertIntoBankHolidaysTable(holiday3);
            var response = _classUnderTest.GetNextBankHoliday(DateTime.Today);
            response.Date.Should().Be(DateTime.Today.AddDays(1));
        }

        [Test]
        public void ReturnsNullIfNoBankHolidayDates()
        {
            var response = _classUnderTest.GetNextBankHoliday(DateTime.Today);
            response.Should().BeNull();
        }
        
        [Test]
        public void ReturnsNullIfBankHolidaysAlreadyGone()
        {
            var holiday1 = _fixture.Create<BankHoliday>();
            holiday1.Date = DateTime.Today.AddDays(-1);
            InsertIntoBankHolidaysTable(holiday1);
            var holiday2 = _fixture.Create<BankHoliday>();
            holiday2.Date = DateTime.Today.AddDays(-2);
            InsertIntoBankHolidaysTable(holiday2);
            var holiday3 = _fixture.Create<BankHoliday>();
            holiday3.Date = DateTime.Today.AddDays(-3);
            InsertIntoBankHolidaysTable(holiday3);
            var response = _classUnderTest.GetNextBankHoliday(DateTime.Today);
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

        private void ClearBankHolidays()
        {
            var addedEntities = _context.BankHolidays;
            _context.BankHolidays.RemoveRange(addedEntities);
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

        private void InsertIntoBankHolidaysTable(BankHoliday holiday)
        {
            _context.BankHolidays.Add(holiday);
            _context.SaveChanges();
        }
    }
}