﻿using System;
using System.Collections.Generic;

namespace CV19INeedHelp.Models.V1
{
    public partial class ResidentSupportAnnexPatch
    {
        public string IsDuplicate { get; set; }
        public bool? OngoingFoodNeed { get; set; }
        public string DobDay { get; set; }
        public string DobMonth { get; set; }
        public string DobYear { get; set; }
        public string ContactTelephoneNumber { get; set; }
        public string ContactMobileNumber { get; set; }

        public string NumberOfPeopleInHouse { get; set; }
        public DateTime? LastConfirmedFoodDelivery { get; set; } 
        public string RecordStatus { get; set; }

    }
}