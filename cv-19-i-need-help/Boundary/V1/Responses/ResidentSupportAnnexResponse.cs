﻿using System;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Boundary.V1.Responses
{
    public class ResidentSupportAnnexResponse
    {
        public int Id { get; set; }
        public string IsDuplicate { get; set; }
        public bool? OngoingFoodNeed { get; set; }
        public bool? OngoingPrescriptionNeed { get; set; }
        public string FormId { get; set; }
        public string FormVersion { get; set; }
        public DateTime? DateTimeRecorded { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DobMonth { get; set; }
        public string DobYear { get; set; }
        public string DobDay { get; set; }
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
        public string NumberOfPeopleInHouse { get; set; }
        public string DaysWorthOfFood { get; set; }
        public string AnyFoodHouseholdCannotEat { get; set; }
        public bool? StrugglingToPayForFood { get; set; }
        public bool? IsPharmacistAbleToDeliver { get; set; }
        public string NameAddressPharmacist { get; set; }
        public bool? IsPackageOfCareAsc { get; set; }
        public bool? IsUrgentFoodRequired { get; set; }
        public string DaysWorthOfMedicine { get; set; }
        public bool? IsUrgentMedicineRequired { get; set; }
        public bool? IsAddressConfirmed { get; set; }
        public bool? IsHouseholdHelpAvailable { get; set; }
        public bool? IsUrgentFood { get; set; }
        public bool? IsUrgentPrescription { get; set; }
        public bool? AnyHelpAvailable { get; set; }
        public bool? IsAnyAgedUnder15 { get; set; }
        public DateTime? LastConfirmedFoodDelivery { get; set; }
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
                FormId = FormId,
                FormVersion = FormVersion,
                DateTimeRecorded = DateTimeRecorded,
                FirstName = FirstName,
                LastName = LastName,
                DobMonth = DobMonth,
                DobYear = DobYear,
                DobDay = DobDay,
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
                NumberOfPeopleInHouse = NumberOfPeopleInHouse,
                DaysWorthOfFood = DaysWorthOfFood,
                AnyFoodHouseholdCannotEat = AnyFoodHouseholdCannotEat,
                StrugglingToPayForFood = StrugglingToPayForFood,
                IsPharmacistAbleToDeliver = IsPharmacistAbleToDeliver,
                NameAddressPharmacist = NameAddressPharmacist,
                IsPackageOfCareAsc = IsPackageOfCareAsc,
                IsUrgentFoodRequired = IsUrgentFoodRequired,
                DaysWorthOfMedicine = DaysWorthOfMedicine,
                IsUrgentMedicineRequired = IsUrgentMedicineRequired,
                IsAddressConfirmed = IsAddressConfirmed,
                IsHouseholdHelpAvailable = IsHouseholdHelpAvailable,
                IsUrgentFood = IsUrgentFood,
                IsUrgentPrescription = IsUrgentPrescription,
                AnyHelpAvailable = AnyHelpAvailable,
                IsAnyAgedUnder15 = IsAnyAgedUnder15,
                LastConfirmedFoodDelivery = LastConfirmedFoodDelivery,
                RecordStatus = RecordStatus,
                DeliveryNotes = DeliveryNotes,
                CaseNotes = CaseNotes,
            };
        }
    }
}
