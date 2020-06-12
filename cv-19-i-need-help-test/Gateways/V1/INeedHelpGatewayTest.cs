using System;
using AutoFixture;
using CV19INeedHelp.Data.V1;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
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
        }

        public void WhenCreatingADeliveryBatchReturnAValidObject()
        {
            
        }
        
        private void ClearResidentSupportAnnexTable()
        {
            var addedEntities = _context.ResidentSupportAnnex;
            _context.ResidentSupportAnnex.RemoveRange(addedEntities);
            _context.SaveChanges();
        }
    }
}