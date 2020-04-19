using System;
using System.Collections.Generic;
using CV19INeedHelp.Gateways.V1;
using CV19INeedHelp.Models;
using CV19INeedHelp.Models.V1;
namespace CV19INeedHelp.UseCases.V1
{
    public class GetFoodDeliveriesForFormUseCase
    {
        private readonly IINeedHelpGateway _iFoodDeliveriesGateway;

        public GetFoodDeliveriesForFormUseCase(IINeedHelpGateway iFoodDeliveriesGateway)
        {
            _iFoodDeliveriesGateway = iFoodDeliveriesGateway;
        }
        
        public List<FoodDelivery> GetFoodDeliveriesForForm(int id)
        {
            return _iFoodDeliveriesGateway.GetFoodDeliveriesForForm(id);
        }
    }
}