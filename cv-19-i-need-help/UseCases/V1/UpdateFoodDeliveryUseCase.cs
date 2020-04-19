using System;
using System.Collections.Generic;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.UseCases.V1
{
    public class UpdateFoodDeliveryUseCase
    {
        private readonly IINeedHelpGateway _iFoodDeliveriesGateway;

        public UpdateFoodDeliveryUseCase(IINeedHelpGateway iFoodDeliveriesGateway)
        {
            _iFoodDeliveriesGateway = iFoodDeliveriesGateway;
        }
        
        public void UpdateFoodDelivery(FoodDelivery data)
        {
            _iFoodDeliveriesGateway.UpdateFoodDelivery(data);
        }
    }
}