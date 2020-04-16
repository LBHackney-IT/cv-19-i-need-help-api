using System;
using CV19INeedHelp.Models.V1;
using CV19INeedHelp.Data.V1;

namespace CV19INeedHelp.Gateways.V1
{
    public class OrganisationVolunteerGateway : IOrganisationVolunteerGateway
    {
        private readonly string _connectionString;
        private readonly Cv19SupportDbContext _dbContext;
        public OrganisationVolunteerGateway(string connectionString)
        {
            _connectionString = connectionString;
            _dbContext = new Cv19SupportDbContext(_connectionString);
        }

        public int GetAllHelpRequests()
        {
            throw new NotImplementedException();
            //_dbContext.OrganisationsNeedingVolunteers.Add();
            //return response;
        }
    }
}