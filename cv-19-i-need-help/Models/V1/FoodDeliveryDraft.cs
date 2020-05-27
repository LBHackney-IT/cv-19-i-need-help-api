using System;

namespace CV19INeedHelp.Models.V1
{
    public class FoodDeliveryDraft
    {
        public int AnnexId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string Uprn { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}