using System;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Boundary.V1.Responses
{
    public class DeliveryBatchResponse
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryPackages { get; set; }
        public string ReportFileId { get; set; }

        public DeliveryBatch ToModel()
        {
            return new DeliveryBatch()
            {
                Id = Id,
                DeliveryDate = DeliveryDate,
                DeliveryPackages = DeliveryPackages,
                ReportFileId = "https://docs.google.com/spreadsheets/d/" + ReportFileId
            };
        }
    }
}
