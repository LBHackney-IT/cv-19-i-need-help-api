using System;
using System.Collections.Generic;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Helpers.V1;
using CV19INeedHelp.Models;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.UseCases.V1
{
    public class DeliveryScheduleUseCase : IDeliveryScheduleUseCase
    {
        private readonly IINeedHelpGateway _iFoodDeliveriesGateway;
        private readonly IFormatHelper _formatHelper;
        private DriveHelper _driveHelper;
        public DeliveryScheduleUseCase(IINeedHelpGateway iFoodDeliveriesGateway)
        {
            _iFoodDeliveriesGateway = iFoodDeliveriesGateway;
            _formatHelper = new FormatHelper();
        }
        
        public object CreateDeliverySchedule(int limit, bool confirmed)
        {
            if (confirmed)
            {
                UtilityHelper helper = new UtilityHelper();
                _driveHelper = new DriveHelper("I Need Help API", "1KpsBDkH3A7ugMPZSb9SCHBeiM7evCqQg");
                var spreadsheet =
                    _driveHelper.CreateSpreadsheet($"Delivery Report - {helper.GetNextWorkingDay():dd-MM-yyyy}");
                var data = _iFoodDeliveriesGateway.CreateDeliverySchedule(limit, spreadsheet);
                _driveHelper.PopulateSpreadsheet(spreadsheet, data);
                return "https://docs.google.com/spreadsheets/d/" + spreadsheet;
            }
            return _formatHelper.FormatDraftOutput(_iFoodDeliveriesGateway.CreateTemporaryDeliveryData(limit));
        }
    }
}