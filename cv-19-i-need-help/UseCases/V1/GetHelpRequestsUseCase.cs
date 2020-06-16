using System;
using System.Collections.Generic;
using System.Linq;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using Amazon.Lambda.Core;
using CV19INeedHelp.Boundary.V1.Responses;
using Newtonsoft.Json;
using Npgsql;

namespace CV19INeedHelp.UseCases.V1
{
    public class GetHelpRequestsUseCase : IGetHelpRequestsUseCase
    {
        private readonly IINeedHelpGateway _iNeedHelpGateway;

        public GetHelpRequestsUseCase(IINeedHelpGateway iNeedHelpGateway)
        {
            _iNeedHelpGateway = iNeedHelpGateway;
        }

        public List<ResidentSupportAnnex> GetHelpRequests(string uprn, string postcode, bool isMaster)
        {
            return _iNeedHelpGateway.GetHelpRequestsForUprn(uprn, postcode, isMaster);
        }

        public List<ResidentSupportAnnex> GetHelpRequestExceptions()
        {
            return _iNeedHelpGateway.GetRequestExceptions();
        }

        public AnnexSummaryResponse GetHelpRequestsSummary()
        {
            return _iNeedHelpGateway.GetHelpRequestsSummary();
        }
    }
}