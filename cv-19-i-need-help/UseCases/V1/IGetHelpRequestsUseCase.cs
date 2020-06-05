using System.Collections.Generic;
using CV19INeedHelp.Models.V1;
namespace CV19INeedHelp.UseCases.V1
{
    public interface IGetHelpRequestsUseCase
    {
        List<ResidentSupportAnnexResponse> GetHelpRequests(string uprn, string postcode, bool isMaster);
        List<ResidentSupportAnnexResponse> GetHelpRequestExceptions();
    }
}