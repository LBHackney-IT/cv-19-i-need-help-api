using System;
using System.Collections.Generic;
using System.Linq;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Helpers.V1
{
    public class FormatHelper : IFormatHelper
    {
        public List<FoodDeliveryDraft> FormatDraftOutput(List<ResidentSupportAnnex> data)
        {
            return data.Select(a => new FoodDeliveryDraft()
                {
                  AnnexId  = a.Id,
                  Name = $"{a.FirstName} {a.LastName}",
                  Address = $"{a.AddressFirstLine} {a.AddressSecondLine} {a.AddressThirdLine}",
                  Postcode = a.Postcode,
                  Uprn = a.Uprn,
                  DeliveryDate = DateTime.Now.AddDays(1)
                }
            ).ToList();
        }
    }
}