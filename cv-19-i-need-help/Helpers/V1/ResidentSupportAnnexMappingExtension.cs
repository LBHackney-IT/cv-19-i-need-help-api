using System.Collections.Generic;
using System.Linq;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Helpers.V1
{
    public static class ResidentSupportAnnexMappingExtension
    {
        public static ResidentSupportAnnexResponse ToResponse(this ResidentSupportAnnex resident)
        {
            return new ResidentSupportAnnexResponse
            {
                Id = resident.Id,
                IsDuplicate = resident.IsDuplicate,
                OngoingFoodNeed = resident.OngoingFoodNeed,
                OngoingPrescriptionNeed = resident.OngoingPrescriptionNeed,
                FormId = resident.FormId,
                FormVersion = resident.FormVersion,
                DateTimeRecorded = resident.DateTimeRecorded,
                FirstName = resident.FirstName,
                LastName = resident.LastName,
                DobMonth = resident.DobMonth,
                DobYear = resident.DobYear,
                DobDay = resident.DobDay,
                Postcode = resident.Postcode,
                Uprn = resident.Uprn,
                Ward = resident.Ward,
                AddressFirstLine = resident.AddressFirstLine,
                AddressSecondLine = resident.AddressSecondLine,
                AddressThirdLine = resident.AddressThirdLine,
                ContactTelephoneNumber = resident.ContactTelephoneNumber,
                ContactMobileNumber = resident.ContactMobileNumber,
                EmailAddress = resident.EmailAddress,
                IsOnBehalf = resident.IsOnBehalf,
                OnBehalfFirstName = resident.OnBehalfFirstName,
                OnBehalfLastName = resident.OnBehalfLastName,
                OnBehalfEmailAddress = resident.OnBehalfEmailAddress,
                OnBehalfContactNumber = resident.OnBehalfContactNumber,
                RelationshipWithResident = resident.RelationshipWithResident,
                AnythingElse = resident.AnythingElse,
                GpSurgeryDetails = resident.GpSurgeryDetails,
                FoodNeed = resident.FoodNeed,
                NumberOfPeopleInHouse = resident.NumberOfPeopleInHouse,
                DaysWorthOfFood = resident.DaysWorthOfFood,
                AnyFoodHouseholdCannotEat = resident.AnyFoodHouseholdCannotEat,
                StrugglingToPayForFood = resident.StrugglingToPayForFood,
                IsPharmacistAbleToDeliver = resident.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = resident.NameAddressPharmacist,
                IsPackageOfCareAsc = resident.IsPackageOfCareAsc,
                IsUrgentFoodRequired = resident.IsUrgentFoodRequired,
                DaysWorthOfMedicine = resident.DaysWorthOfMedicine,
                IsUrgentMedicineRequired = resident.IsUrgentMedicineRequired,
                IsAddressConfirmed = resident.IsAddressConfirmed,
                IsHouseholdHelpAvailable = resident.IsHouseholdHelpAvailable,
                IsUrgentFood = resident.IsUrgentFood,
                IsUrgentPrescription = resident.IsUrgentPrescription,
                AnyHelpAvailable = resident.AnyHelpAvailable,
                IsAnyAgedUnder15 = resident.IsAnyAgedUnder15,
                LastConfirmedFoodDelivery = resident.LastConfirmedFoodDelivery,
                RecordStatus = resident.RecordStatus,
                DeliveryNotes = resident.DeliveryNotes,
                CaseNotes = resident.CaseNotes,
            };
        }

        public static List<ResidentSupportAnnexResponse> ToResponse(this IEnumerable<ResidentSupportAnnex> residents)
        {
            return residents.Select(res => res.ToResponse()).ToList();
        }
    }
}