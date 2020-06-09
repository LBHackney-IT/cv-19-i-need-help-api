using System;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Boundary.V1.Responses
{
    public class DeliveryReportItemResponse
    {
        public int Id { get; set; }
        public int AnnexId { get; set; }
        public int NumberOfPackages { get; set; }
        public string FullName { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string FullAddress { get; set; }
        public string Postcode { get; set; }
        public string Uprn { get; set; }
        public string AnyFoodHouseholdCannotEat { get; set; }
        public string DeliveryNotes { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime? LastConfirmedDeliveryDate { get; set; }
        public int BatchId { get; set; }

        public DeliveryReportItem ToModel()
        {
            return new DeliveryReportItem()
            {
                Id = Id,
                AnnexId = AnnexId,
                NumberOfPackages = NumberOfPackages,
                FullName = FullName,
                TelephoneNumber = TelephoneNumber,
                MobileNumber = MobileNumber,
                FullAddress = FullAddress,
                Postcode = Postcode,
                Uprn = Uprn,
                AnyFoodHouseholdCannotEat = AnyFoodHouseholdCannotEat,
                DeliveryNotes = DeliveryNotes,
                DeliveryDate = DeliveryDate,
                LastConfirmedDeliveryDate = LastConfirmedDeliveryDate,
                BatchId = BatchId
            };
        }
    }
}
