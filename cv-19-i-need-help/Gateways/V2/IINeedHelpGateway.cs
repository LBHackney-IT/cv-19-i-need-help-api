using System.Collections.Generic;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Gateways.V2
{
    public interface IINeedHelpGateway
    {
        List<ResidentSupportAnnex> QueryHelpRequests(string uprn, string postcode, string address, string firstName, string lastName, bool isMaster);
    }
}