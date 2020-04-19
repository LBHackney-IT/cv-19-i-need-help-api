using System;
using System.Collections.Generic;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.UseCases.V1
{
    public class CreateFoodDeliveryUseCase
    {
        private readonly IINeedHelpGateway _iFoodDeliveriesGateway;

        public CreateFoodDeliveryUseCase(IINeedHelpGateway iFoodDeliveriesGateway)
        {
            _iFoodDeliveriesGateway = iFoodDeliveriesGateway;
        }
        
        public int CreateFoodDelivery(FoodDelivery data)
        {
            return _iFoodDeliveriesGateway.CreateFoodDelivery(data);
        }
    }
}