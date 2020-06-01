using System;

namespace CV19INeedHelp.Models.V1
{
    public class DeliveryBatch
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryPackages { get; set; }
        public string ReportFileId { get; set; }
    }
}