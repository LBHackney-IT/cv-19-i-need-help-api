using System;
using System.Collections.Generic;
using System.Linq;
using CV19INeedHelp.Models.V1;
using CV19INeedHelp.Data.V1;

namespace CV19INeedHelp.Gateways.V1
{
    public class INeedHelpGateway : IINeedHelpGateway
    {
        private readonly string _connectionString;
        private readonly Cv19SupportDbContext _dbContext;
        public INeedHelpGateway(string connectionString)
        {
            _connectionString = connectionString;
            _dbContext = new Cv19SupportDbContext(_connectionString);
        }

        public List<ResidentSupportAnnex> GetAllHelpRequests()
        {
            var response = _dbContext.ResidentSupportAnnex.ToList();
            return response;
        }
    }
}