using System;
using System.Collections.Generic;

namespace CV19INeedHelp.Models.V1
{
    public partial class ResidentSupportAnnexPatch
    {
        public string IsDuplicate { get; set; }
        public bool? OngoingFoodNeed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DobDay { get; set; }
        public string DobMonth { get; set; }
        public string DobYear { get; set; }
        public string Postcode { get; set; }
        public string Uprn { get; set; }
        public string AddressFirstLine { get; set; }
        public string AddressSecondLine { get; set; }
        public string AddressThirdLine { get; set; }
        public string EmailAddress { get; set; }
        public string ContactTelephoneNumber { get; set; }
        public string ContactMobileNumber { get; set; }
        public string NumberOfPeopleInHouse { get; set; }
        public DateTime? LastConfirmedFoodDelivery { get; set; } 
        public string RecordStatus { get; set; }
        public string DeliveryNotes { get; set; }
        public string CaseNotes { get; set; }

    }
}
