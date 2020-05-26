using System.Collections.Generic;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.UseCases.V1
{
    public interface IDeliveryScheduleUseCase
    {
        List<ResidentSupportAnnex> CreateDeliverySchedule(int limit, bool confirmed);
    }
}