using System.Linq;
using Amazon.Lambda.Core;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Helpers.V1;
using CV19INeedHelp.Models.V1;
using Newtonsoft.Json;

namespace CV19INeedHelp.UseCases.V1
{
    public class DeliveryScheduleUseCase : IDeliveryScheduleUseCase
    {
        private readonly IINeedHelpGateway _iFoodDeliveriesGateway;
        private readonly IFormatHelper _formatHelper;
        private IDriveHelper _driveHelper;
        public DeliveryScheduleUseCase(IINeedHelpGateway iFoodDeliveriesGateway, IDriveHelper driveHelper)
        {
            _iFoodDeliveriesGateway = iFoodDeliveriesGateway;
            _formatHelper = new FormatHelper();
            _driveHelper = driveHelper;
        }
        
        public object CreateDeliverySchedule(int limit, bool confirmed)
        {
            if (confirmed)
            {
                UtilityHelper helper = new UtilityHelper();
                var deliveryDay = helper.GetNextWorkingDay();
                var spreadsheet =
                    _driveHelper.CreateSpreadsheet($"Delivery Report - {deliveryDay:dd-MM-yyyy}");
                var data = _iFoodDeliveriesGateway.CreateDeliverySchedule(limit, spreadsheet);
                _driveHelper.PopulateSpreadsheet(spreadsheet, data);
                var responseDetails = data.FirstOrDefault();
                _iFoodDeliveriesGateway.UpdateAnnexWithDeliveryDates(data);
                return new DeliveryBatchResponse()
                {
                    DeliveryDate = responseDetails.DeliveryDate,
                    DeliveryPackages = data.Count(),
                    Id = responseDetails.BatchId,
                    ReportFileId = "https://docs.google.com/spreadsheets/d/" + spreadsheet
                };
            }
            var getHelpRequests = _iFoodDeliveriesGateway.CreateTemporaryDeliveryData(limit).ToList();
            return _formatHelper.FormatDraftOutput(getHelpRequests);
        }

        public DeliveryBatchResponse GetDeliveryBatch()
        {
            UtilityHelper helper = new UtilityHelper();
            var deliveryDay = helper.GetNextWorkingDay();
            var alreadyGenerated = _iFoodDeliveriesGateway.FindExistingBatchForDate(deliveryDay);
            if (alreadyGenerated != null)
            {
                return new DeliveryBatchResponse()
                {
                    DeliveryDate = alreadyGenerated.DeliveryDate,
                    DeliveryPackages = alreadyGenerated.DeliveryPackages,
                    Id = alreadyGenerated.Id,
                    ReportFileId = "https://docs.google.com/spreadsheets/d/"+alreadyGenerated.ReportFileId
                };
            }
            return null;
        }

        public void DeleteDeliveryBatch(int id)
        {
            var batch = _iFoodDeliveriesGateway.GetBatchById(id);
            if (batch != null)
            {
                LambdaLogger.Log($"Executing delete spreadsheet method for {batch.ReportFileId}.");
                _driveHelper.DeleteSpreadsheet(batch.ReportFileId);
                LambdaLogger.Log($"Executing delete batch method for batch {batch.Id}.");
                _iFoodDeliveriesGateway.DeleteBatch(batch.Id);
                LambdaLogger.Log($"Batch deletion completed.");
            }
        }
    }
}