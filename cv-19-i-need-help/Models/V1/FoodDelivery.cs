using System;

namespace CV19INeedHelp.Models.V1
{
    public class FoodDelivery
    {
        public int Id { get; set; }
        public int AnnexId { get; set; }
        public DateTime ScheduledDeliveryDate { get; set; }
        public bool? DeliveryConfirmed { get; set; }
        public string ReasonForNonDelivery { get; set; }
        public string UPRN { get; set; }
        public bool? IsThisFirstDelivery { get; set; }
        public int RepeatDelivery { get; set; }
        public int HouseholdSize { get; set; }
        public int FoodPackages { get; set; }
    }
}