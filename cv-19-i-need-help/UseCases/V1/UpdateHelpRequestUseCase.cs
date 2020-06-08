using System;
using System.Collections.Generic;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using Amazon.Lambda.Core;
using CV19INeedHelp.Boundary.V1.Responses;
using Newtonsoft.Json;

namespace CV19INeedHelp.UseCases.V1
{
    public class UpdateHelpRequestUseCase : IUpdateHelpRequestUseCase
    {
        private readonly IINeedHelpGateway _iNeedHelpGateway;

        public UpdateHelpRequestUseCase(IINeedHelpGateway iNeedHelpGateway)
        {
            _iNeedHelpGateway = iNeedHelpGateway;
        }

        public void UpdateHelpRequest(int id, ResidentSupportAnnexResponse data)
        {
            data.Id = id;
            _iNeedHelpGateway.UpdateHelpRequest(data.ToEntity());
        }

        public void PatchHelpRequest(int id, ResidentSupportAnnexPatch data_items)
        {
            _iNeedHelpGateway.PatchHelpRequest(id, data_items);
        }
    }
}