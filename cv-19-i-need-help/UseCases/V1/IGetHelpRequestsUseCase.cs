using System.Collections.Generic;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Models.V1;
namespace CV19INeedHelp.UseCases.V1
{
    public interface IGetHelpRequestsUseCase
    {
        List<ResidentSupportAnnex> GetHelpRequests(string uprn, string postcode, bool isMaster);
        List<ResidentSupportAnnex> GetHelpRequestExceptions();
        AnnexSummaryResponse GetHelpRequestsSummary();
    }
}