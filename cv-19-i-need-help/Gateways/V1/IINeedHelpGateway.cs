using System.Collections.Generic;
using CV19INeedHelp.Models.V1;
namespace CV19INeedHelp.Gateways.V1
{
    public interface IINeedHelpGateway
    {
        List<ResidentSupportAnnex> GetHelpRequestsForUprn(string uprn, string postcode, bool isMaster);
        ResidentSupportAnnex GetSingleHelpRequest(int id);
        void UpdateHelpRequest(ResidentSupportAnnex data);
        List<FoodDelivery> GetFoodDeliveriesForForm(int id);

        int CreateFoodDelivery(FoodDelivery data);
        void UpdateFoodDelivery(FoodDelivery data);
        void PatchHelpRequest(int id, ResidentSupportAnnexPatch dataItems);
        List<ResidentSupportAnnex> GetRequestExceptions();
        int CreateDeliverySchedule(int limit);
        int CreateTemporaryDeliveryData(int limit);
    }
}