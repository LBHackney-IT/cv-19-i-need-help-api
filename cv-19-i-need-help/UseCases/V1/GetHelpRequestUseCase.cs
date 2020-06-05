using System;
using System.Collections.Generic;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

namespace CV19INeedHelp.UseCases.V1
{
    public class GetHelpRequestUseCase : IGetHelpRequestUseCase
    {
        private readonly IINeedHelpGateway _iNeedHelpGateway;

        public GetHelpRequestUseCase(IINeedHelpGateway iNeedHelpGateway)
        {
            _iNeedHelpGateway = iNeedHelpGateway;
        }

        public ResidentSupportAnnexResponse GetHelpRequest(int id)
        {
            return _iNeedHelpGateway.GetSingleHelpRequest(id).ToResponse();
        }
    }
}