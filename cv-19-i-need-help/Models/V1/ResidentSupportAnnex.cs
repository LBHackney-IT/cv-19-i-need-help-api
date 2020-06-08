using System;
using System.ComponentModel.DataAnnotations.Schema;
using CV19INeedHelp.Boundary.V1.Responses;

namespace CV19INeedHelp.Models.V1
{
    [Table("resident_support_annex")]
    public class ResidentSupportAnnex
    {
        [Column("id")]
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
    }
}