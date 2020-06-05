using System;
using System.Collections.Generic;
using System.Linq;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using Amazon.Lambda.Core;
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

        public List<ResidentSupportAnnexResponse> GetHelpRequests(string uprn, string postcode, bool isMaster)
        {
            return _iNeedHelpGateway.GetHelpRequestsForUprn(uprn, postcode, isMaster)
                .Select(x => x.ToResponse()).ToList();
        }

        public List<ResidentSupportAnnexResponse> GetHelpRequestExceptions()
        {
            return _iNeedHelpGateway.GetRequestExceptions()
                .Select(x => x.ToResponse()).ToList();
        }
    }
}