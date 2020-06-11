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
            Expression<Func<ResidentSupportAnnex, bool>> queryUprn = x => x.Uprn == uprn;

            Expression<Func<ResidentSupportAnnex, bool>> queryAddress = x =>
                (string.IsNullOrWhiteSpace(postcode)
                    || x.Postcode.Replace(" ", "").ToUpper().Equals(postcode.Replace(" ", "").ToUpper()))
                && (string.IsNullOrWhiteSpace(address)
                    || x.AddressFirstLine.Replace(" ", "").ToUpper().Contains(address.Replace(" ", "").ToUpper()));

            Expression<Func<ResidentSupportAnnex, bool>> queryFirstName = x =>
                string.IsNullOrWhiteSpace(firstName)
                 || x.FirstName.Replace(" ", "").ToUpper().Equals(firstName.Replace(" ", "").ToUpper());

            Expression<Func<ResidentSupportAnnex, bool>> queryLastName = x =>
                string.IsNullOrWhiteSpace(lastName)
                || x.LastName.Replace(" ", "").ToUpper().Equals(lastName.Replace(" ", "").ToUpper());

            return _dbContext.ResidentSupportAnnex
                .Where(uprn != null ?  queryUprn : queryAddress)
                .Where(queryFirstName)
                .Where(queryLastName)
                .ToList()
                .Where(x => x.RecordStatus == "MASTER" || !isMaster)
                .ToList();
        }
    }
}