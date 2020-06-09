using System.Collections.Generic;
using System.Linq;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Helpers.V1
{
    public static class DeliveryBatchMappingExtension
    {
        public static DeliveryBatchResponse ToResponse(this DeliveryBatch batch)
        {
            return new DeliveryBatchResponse
            {
                Id = batch.Id,
                DeliveryDate = batch.DeliveryDate,
                DeliveryPackages = batch.DeliveryPackages,
                ReportFileId = batch.ReportFileId
            };
        }
    }
}