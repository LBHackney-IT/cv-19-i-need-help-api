using System.Collections.Generic;

namespace CV19INeedHelp.UseCases.V1
{
    public interface IDeliveryScheduleUseCase
    {
        int CreateDeliverySchedule(int limit, bool confirmed);
    }
}