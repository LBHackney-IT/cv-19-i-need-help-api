using System.Collections.Generic;
using System.Text.RegularExpressions;
using CV19INeedHelp.Boundary.Exceptions;
using CV19INeedHelp.Gateways.V2;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.UseCases.V2
{
    public class GetHelpRequestsUseCase : IGetHelpRequestsUseCase
    {
        private readonly IINeedHelpGateway _iNeedHelpGateway;

        private const string PostcodeRegex =
            "^((([A-PR-UWYZ][A-HK-Y]?[0-9][0-9]?)|(([A-PR-UWYZ][0-9][A-HJKSTUW])|([A-PR-UWYZ][A-HK-Y][0-9][ABEHMNPRV-Y]))) {0,}[0-9][ABD-HJLNP-UW-Z]{2})$";

        public GetHelpRequestsUseCase(IINeedHelpGateway iNeedHelpGateway)
        {
            _iNeedHelpGateway = iNeedHelpGateway;
        }

        public List<ResidentSupportAnnex> GetHelpRequests(string uprn, string postcode, string address, string firstName, string lastName, bool isMaster)
        {
            var uprnValid = uprn == null || Regex.IsMatch(uprn, "\\d+$");
            if (!uprnValid)
                throw new InvalidQueryParameter("The UPRN given is invalid. UPRN must only have numeric characters");

            var postcodeValid = postcode == null || Regex.IsMatch(postcode.ToUpper(), PostcodeRegex);
            if (!postcodeValid)
                throw new InvalidQueryParameter("The Postcode given does not have a valid format");

            return _iNeedHelpGateway.QueryHelpRequests(uprn, postcode, address, firstName, lastName, isMaster);
        }
    }
}
