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
        public DeliveryScheduleUseCase(IINeedHelpGateway iFoodDeliveriesGateway)
        {
            _iFoodDeliveriesGateway = iFoodDeliveriesGateway;
            _formatHelper = new FormatHelper();
        }
        
        public object CreateDeliverySchedule(int limit, bool confirmed)
        {
            if (confirmed)
            {
                return _iFoodDeliveriesGateway.CreateDeliverySchedule(limit);
            }
            return _formatHelper.FormatDraftOutput(_iFoodDeliveriesGateway.CreateTemporaryDeliveryData(limit));
        }
    }
}