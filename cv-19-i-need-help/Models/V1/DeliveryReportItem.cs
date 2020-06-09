using System;

namespace CV19INeedHelp.Models.V1
{
    public class DeliveryReportItem
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
    }
}