using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CV19INeedHelp.Models.V1;
using CV19INeedHelp.Data.V1;

namespace CV19INeedHelp.Gateways.V2
{
    public class INeedHelpGateway : IINeedHelpGateway
    {
        private readonly Cv19SupportDbContext _dbContext;
        public INeedHelpGateway(Cv19SupportDbContext context)
        {
            _dbContext = context;
        }

        public List<ResidentSupportAnnex> QueryHelpRequests(string uprn, string postcode, string address, string firstName, string lastName, bool isMaster)
        {
            Expression<Func<ResidentSupportAnnex, bool>> queryUprn = x =>
                x.Uprn == uprn;

            Expression<Func<ResidentSupportAnnex, bool>> queryPostcode = x =>
                string.IsNullOrWhiteSpace(postcode)
                || x.Postcode.Replace(" ", "").ToUpper().Contains(postcode.Replace(" ", "").ToUpper());

            var response = _dbContext.ResidentSupportAnnex
                .Where(uprn != null ?  queryUprn : queryPostcode)
                .ToList()
                .Where(x => x.RecordStatus == "MASTER" || !isMaster);

            return response.ToList();
        }
    }
}