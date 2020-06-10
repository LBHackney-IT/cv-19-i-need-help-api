using System;
using CV19INeedHelp.Data.V1;
using NUnit.Framework;

namespace CV19INeedHelpTest.EndToEndTests.V2
{
    public class DatabaseTests
    {
        private string _currentConnStr;
        protected Cv19SupportDbContext DbContext;

        [SetUp]
        public void SetDbContext()
        {
            _currentConnStr = Environment.GetEnvironmentVariable("CV_19_DB_CONNECTION");
            const string connectionString = "Host=localhost;Database=i-need-help-test;Username=postgres;Password=mypassword";
            DbContext = new Cv19SupportDbContext(_currentConnStr ?? connectionString);
            ClearResidentSupportAnnexTable();
            if (_currentConnStr == null) Environment.SetEnvironmentVariable("CV_19_DB_CONNECTION", connectionString);
        }

        [TearDown]
        public void ResetDb()
        {
            Environment.SetEnvironmentVariable("CV_19_DB_CONNECTION", _currentConnStr);
            ClearResidentSupportAnnexTable();
        }

        private void ClearResidentSupportAnnexTable()
        {
            var addedEntities = DbContext.ResidentSupportAnnex;
            DbContext.ResidentSupportAnnex.RemoveRange(addedEntities);
            DbContext.SaveChanges();
        }
    }
}