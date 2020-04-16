using System.Collections.Generic;
using CV19INeedHelp.Models.V1;
namespace CV19INeedHelp.Gateways.V1
{
    public interface IOrganisationVolunteerGateway
    {
        List<ResidentSupportAnnex> GetAllHelpRequests();
    }
}