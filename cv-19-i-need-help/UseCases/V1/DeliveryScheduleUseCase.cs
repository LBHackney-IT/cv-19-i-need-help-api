using System.Linq;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Helpers.V1;
using CV19INeedHelp.Models.V1;

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
                var spreadsheet =
                    _driveHelper.CreateSpreadsheet($"Delivery Report - {helper.GetNextWorkingDay():dd-MM-yyyy}");
                var data = _iFoodDeliveriesGateway.CreateDeliverySchedule(limit, spreadsheet);
                _driveHelper.PopulateSpreadsheet(spreadsheet, data);
                var responseDetails = data.FirstOrDefault();
                // TODO:  Enable this to update the annex with next set of delivery dates.
                // foreach (var item in data)
                // {
                //     var annexPatch = new ResidentSupportAnnexPatch();
                //     annexPatch.LastConfirmedFoodDelivery = item.DeliveryDate;
                //     _iFoodDeliveriesGateway.PatchHelpRequest(item.AnnexId, annexPatch);   
                // }
                return new DeliveryBatch()
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
    }
}