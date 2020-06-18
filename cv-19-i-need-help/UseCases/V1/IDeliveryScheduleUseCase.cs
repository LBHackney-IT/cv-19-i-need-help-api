using System;
using System.Collections.Generic;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.UseCases.V1
{
    public interface IDeliveryScheduleUseCase
    {
        object CreateDeliverySchedule(int limit, bool confirmed, DateTime date);
        DeliveryBatchResponse GetDeliveryBatch(DateTime date);
        void DeleteDeliveryBatch(int id);
    }
}