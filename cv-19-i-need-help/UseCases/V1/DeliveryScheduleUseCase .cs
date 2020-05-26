using System;
using System.Collections.Generic;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.UseCases.V1
{
    public class DeliveryScheduleUseCase : IDeliveryScheduleUseCase
    {
        private readonly IINeedHelpGateway _iFoodDeliveriesGateway;

        public DeliveryScheduleUseCase(IINeedHelpGateway iFoodDeliveriesGateway)
        {
            _iFoodDeliveriesGateway = iFoodDeliveriesGateway;
        }
        
        public int CreateDeliverySchedule(int limit, bool confirmed)
        {
            if (confirmed)
            {
                return _iFoodDeliveriesGateway.CreateDeliverySchedule(limit);
            }
            else
            {
                return _iFoodDeliveriesGateway.CreateTemporaryDeliveryData(limit);
            }
        }
    }
}