using System;
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
        [TestCase]
        public void InsertMethodShouldNotBeCalledWithoutParameter()
        {
//            string connectionString = string.Empty;
//            var data = new OrganisationsNeedingVolunteers() ;
//            IOrganisationVolunteerGateway classUnderTest = new OrganisationVolunteerGateway(connectionString);
//            var resp = classUnderTest.Insert(data);
//            Assert.AreEqual(1,resp);
        }
        
        public void WhenCreatingAResidentRequestGivenValidParametersReturnAValidObject()
        {
            //Arrange
            string _connectionString = "blah";
            var fakeDbContext = new Mock<Cv19SupportDbContext>(_connectionString);

            IINeedHelpGateway classUnderTest = new INeedHelpGateway(fakeDbContext.Object);

        }
    }
}