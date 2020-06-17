using System;
using System.Collections.Generic;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Helpers.V1
{
    public interface IFormatHelper
    {
        List<FoodDeliveryDraft> FormatDraftOutput(List<ResidentSupportAnnex> data, DateTime deliveryDate);
    }
}