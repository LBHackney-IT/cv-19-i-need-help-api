using System.Collections.Generic;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Usecases.V1
{
    public interface ICreateFoodDeliveryUseCase
    {
        int CreateFoodDelivery(FoodDelivery data);
    }
}