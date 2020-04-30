using System;
using System.Collections.Generic;

namespace CV19INeedHelp.Models.V1
{
    public partial class ResidentSupportAnnexPatch
    {
        public bool? OngoingFoodNeed { get; set; }
        public string NumberOfPeopleInHouse { get; set; }
        public DateTime? LastConfirmedFoodDelivery { get; set; }        
    }
}
