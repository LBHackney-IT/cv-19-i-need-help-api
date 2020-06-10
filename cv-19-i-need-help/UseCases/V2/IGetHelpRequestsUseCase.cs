using System.Collections.Generic;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.UseCases.V2
{
    public interface IGetHelpRequestsUseCase
    {
        List<ResidentSupportAnnex> GetHelpRequests(string uprn, string postcode, string address, string firstName, string lastName, bool isMaster);
    }
}