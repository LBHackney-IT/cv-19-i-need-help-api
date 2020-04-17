using System.Collections.Generic;
using CV19INeedHelp.Models.V1;
namespace CV19INeedHelp.Gateways.V1
{
    public interface IINeedHelpGateway
    {
        List<ResidentSupportAnnex> GetAllHelpRequests();
        ResidentSupportAnnex GetSingleHelpRequest(int id);

        void UpdateHelpRequest(ResidentSupportAnnex data);
    }
}