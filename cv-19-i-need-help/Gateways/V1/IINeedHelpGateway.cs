using System;
using System.Collections.Generic;
using CV19INeedHelp.Models;
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
        List<DeliveryReportItem> CreateDeliverySchedule(int limit, string spreadsheet);
        List<ResidentSupportAnnex> CreateTemporaryDeliveryData(int limit);
        void UpdateAnnexWithDeliveryDates(List<DeliveryReportItem> data);
        DeliveryBatch FindExistingBatchForDate(DateTime deliveryDay);
        void DeleteBatch(int id);
        void RevertAnnexDeliveryDates(List<DeliveryReportItem> data);
    }
}