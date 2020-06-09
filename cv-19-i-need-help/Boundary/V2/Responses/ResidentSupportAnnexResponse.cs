using System;
using System.Collections.Generic;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Boundary.V2.Responses
{

    public class ResidentSupportAnnexResponseList
    {
        public List<ResidentSupportAnnexResponse> HelpRequests { get; set; }
    }
    public class ResidentSupportAnnexResponse
    {
        public int Id { get; set; }
        public string IsDuplicate { get; set; }
        public bool? OngoingFoodNeed { get; set; }
        public bool? OngoingPrescriptionNeed { get; set; }
        public int? FormId { get; set; }
        public string FormVersion { get; set; }
        public string DateTimeRecorded { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Postcode { get; set; }
        public string Uprn { get; set; }
        public string Ward { get; set; }
        public string AddressFirstLine { get; set; }
        public string AddressSecondLine { get; set; }
        public string AddressThirdLine { get; set; }
        public string ContactTelephoneNumber { get; set; }
        public string ContactMobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsOnBehalf { get; set; }
        public string OnBehalfFirstName { get; set; }
        public string OnBehalfLastName { get; set; }
        public string OnBehalfEmailAddress { get; set; }
        public string OnBehalfContactNumber { get; set; }
        public string RelationshipWithResident { get; set; }
        public string AnythingElse { get; set; }
        public string GpSurgeryDetails { get; set; }
        public bool? FoodNeed { get; set; }
        public int? NumberOfPeopleInHouse { get; set; }
        public int? DaysWorthOfFood { get; set; }
        public string AnyFoodHouseholdCannotEat { get; set; }
        public bool? StrugglingToPayForFood { get; set; }
        public bool? IsPharmacistAbleToDeliver { get; set; }
        public string NameAddressPharmacist { get; set; }
        public bool? IsPackageOfCareAsc { get; set; }
        public bool? IsUrgentFoodRequired { get; set; }
        public int? DaysWorthOfMedicine { get; set; }
        public bool? IsUrgentMedicineRequired { get; set; }
        public bool? IsAddressConfirmed { get; set; }
        public bool? IsHouseholdHelpAvailable { get; set; }
        public bool? IsUrgentFood { get; set; }
        public bool? IsUrgentPrescription { get; set; }
        public bool? AnyHelpAvailable { get; set; }
        public bool? IsAnyAgedUnder15 { get; set; }
        public string LastConfirmedFoodDelivery { get; set; }
        public string RecordStatus { get; set; }
        public string DeliveryNotes { get; set; }
        public string CaseNotes { get; set; }

        public ResidentSupportAnnex ToModel()
        {
            return new ResidentSupportAnnex
            {
                Id = Id,
                IsDuplicate = IsDuplicate,
                OngoingFoodNeed = OngoingFoodNeed,
                OngoingPrescriptionNeed = OngoingPrescriptionNeed,
                FormId = FormId.ToString(),
                FormVersion = FormVersion,
                DateTimeRecorded = ParseNullableDateTime(DateTimeRecorded),
                FirstName = FirstName,
                LastName = LastName,
                DobMonth = ParseNullableDateTime(DateOfBirth)?.Month.ToString(),
                DobYear = ParseNullableDateTime(DateOfBirth)?.Year.ToString(),
                DobDay = ParseNullableDateTime(DateOfBirth)?.Day.ToString(),
                Postcode = Postcode,
                Uprn = Uprn,
                Ward = Ward,
                AddressFirstLine = AddressFirstLine,
                AddressSecondLine = AddressSecondLine,
                AddressThirdLine = AddressThirdLine,
                ContactTelephoneNumber = ContactTelephoneNumber,
                ContactMobileNumber = ContactMobileNumber,
                EmailAddress = EmailAddress,
                IsOnBehalf = IsOnBehalf,
                OnBehalfFirstName = OnBehalfFirstName,
                OnBehalfLastName = OnBehalfLastName,
                OnBehalfEmailAddress = OnBehalfEmailAddress,
                OnBehalfContactNumber = OnBehalfContactNumber,
                RelationshipWithResident = RelationshipWithResident,
                AnythingElse = AnythingElse,
                GpSurgeryDetails = GpSurgeryDetails,
                FoodNeed = FoodNeed,
                NumberOfPeopleInHouse = NumberOfPeopleInHouse.ToString(),
                DaysWorthOfFood = DaysWorthOfFood.ToString(),
                AnyFoodHouseholdCannotEat = AnyFoodHouseholdCannotEat,
                StrugglingToPayForFood = StrugglingToPayForFood,
                IsPharmacistAbleToDeliver = IsPharmacistAbleToDeliver,
                NameAddressPharmacist = NameAddressPharmacist,
                IsPackageOfCareAsc = IsPackageOfCareAsc,
                IsUrgentFoodRequired = IsUrgentFoodRequired,
                DaysWorthOfMedicine = DaysWorthOfMedicine.ToString(),
                IsUrgentMedicineRequired = IsUrgentMedicineRequired,
                IsAddressConfirmed = IsAddressConfirmed,
                IsHouseholdHelpAvailable = IsHouseholdHelpAvailable,
                IsUrgentFood = IsUrgentFood,
                IsUrgentPrescription = IsUrgentPrescription,
                AnyHelpAvailable = AnyHelpAvailable,
                IsAnyAgedUnder15 = IsAnyAgedUnder15,
                LastConfirmedFoodDelivery = ParseNullableDateTime(LastConfirmedFoodDelivery),
                RecordStatus = RecordStatus,
                DeliveryNotes = DeliveryNotes,
                CaseNotes = CaseNotes,
            };
        }

        private DateTime? ParseNullableDateTime(string date)
        {
            return date != null ? DateTime.Parse(date) : (DateTime?) null;
        }
    }
}
