using System;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models.V1;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

namespace CV19INeedHelp.UseCases.V1
{
    public class GetHelpRequestsUseCase : IGetHelpRequestsUseCase
    {
        private readonly IOrganisationVolunteerGateway _organisationVolunteerGateway;

        public GetHelpRequestsUseCase(IOrganisationVolunteerGateway organisationVolunteerGateway)
        {
            _organisationVolunteerGateway = organisationVolunteerGateway;
        }

        public int GetHelpRequests()
        {
            return _organisationVolunteerGateway.GetAllHelpRequests();
        }
    }
}