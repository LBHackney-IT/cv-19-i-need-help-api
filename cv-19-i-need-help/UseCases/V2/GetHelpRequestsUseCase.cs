using System.Collections.Generic;
using CV19INeedHelp.Gateways.V2;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.UseCases.V2
{
    public class GetHelpRequestsUseCase : IGetHelpRequestsUseCase
    {
        private readonly IINeedHelpGateway _iNeedHelpGateway;

        public GetHelpRequestsUseCase(IINeedHelpGateway iNeedHelpGateway)
        {
            _iNeedHelpGateway = iNeedHelpGateway;
        }

        public List<ResidentSupportAnnex> GetHelpRequests(string uprn, string postcode, string address, string firstName, string lastName, bool isMaster)
        {
            return _iNeedHelpGateway.QueryHelpRequests(uprn, postcode, address, firstName, lastName, isMaster);
        }
    }
}
