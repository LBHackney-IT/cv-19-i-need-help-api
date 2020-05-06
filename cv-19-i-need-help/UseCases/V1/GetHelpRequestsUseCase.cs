using System;
using System.Collections.Generic;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

namespace CV19INeedHelp.UseCases.V1
{
    public class GetHelpRequestsUseCase : IGetHelpRequestsUseCase
    {
        private readonly IINeedHelpGateway _iNeedHelpGateway;

        public GetHelpRequestsUseCase(IINeedHelpGateway iNeedHelpGateway)
        {
            _iNeedHelpGateway = iNeedHelpGateway;
        }

        public List<ResidentSupportAnnex> GetHelpRequests(string uprn, bool isMaster)
        {
            if (string.IsNullOrEmpty(uprn))
            {
                throw new ArgumentException("UPRN must be provided.");
            }
            return _iNeedHelpGateway.GetHelpRequestsForUprn(uprn, isMaster);
        }

        public List<ResidentSupportAnnex> GetHelpRequestExceptions()
        {
            return _iNeedHelpGateway.GetRequestExceptions();
        }
    }
}