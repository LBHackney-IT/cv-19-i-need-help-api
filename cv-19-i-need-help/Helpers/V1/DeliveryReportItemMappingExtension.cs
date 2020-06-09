using System.Collections.Generic;
using System.Linq;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Helpers.V1
{
    public static class DeliveryReportItemMappingExtension
    {
        public static DeliveryReportItemResponse ToResponse(this DeliveryReportItem item)
        {
            return new DeliveryReportItemResponse()
            {
                Id = item.Id,
                AnnexId = item.AnnexId,
                NumberOfPackages = item.NumberOfPackages,
                FullName = item.FullName,
                TelephoneNumber = item.TelephoneNumber,
                MobileNumber = item.MobileNumber,
                FullAddress = item.FullAddress,
                Postcode = item.Postcode,
                Uprn = item.Uprn,
                AnyFoodHouseholdCannotEat = item.AnyFoodHouseholdCannotEat,
                DeliveryNotes = item.DeliveryNotes,
                DeliveryDate = item.DeliveryDate,
                LastConfirmedDeliveryDate = item.LastConfirmedDeliveryDate,
                BatchId = item.BatchId
            };
        }

        public static List<DeliveryReportItemResponse> ToResponse(this IEnumerable<DeliveryReportItem> deliveryReportItems)
        {
            return deliveryReportItems.Select(res => res.ToResponse()).ToList();
        }
    }
}