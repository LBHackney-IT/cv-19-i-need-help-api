using System;
using System.Collections.Generic;
using System.Linq;
using CV19INeedHelp.Boundary.V2.Responses;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Helpers.V2
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
                FormId = ParseNullableInt(resident.FormId),
                FormVersion = resident.FormVersion,
                DateTimeRecorded = resident.DateTimeRecorded,
                FirstName = resident.FirstName,
                LastName = resident.LastName,
                DateOfBirth = ParseDate(resident.DobYear, resident.DobMonth, resident.DobDay),
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
                NumberOfPeopleInHouse = ParseNullableInt(resident.NumberOfPeopleInHouse),
                DaysWorthOfFood = ParseNullableInt(resident.DaysWorthOfFood),
                AnyFoodHouseholdCannotEat = resident.AnyFoodHouseholdCannotEat,
                StrugglingToPayForFood = resident.StrugglingToPayForFood,
                IsPharmacistAbleToDeliver = resident.IsPharmacistAbleToDeliver,
                NameAddressPharmacist = resident.NameAddressPharmacist,
                IsPackageOfCareAsc = resident.IsPackageOfCareAsc,
                IsUrgentFoodRequired = resident.IsUrgentFoodRequired,
                DaysWorthOfMedicine = ParseNullableInt(resident.DaysWorthOfMedicine),
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

        private static int? ParseNullableInt(string number)
        {
            return number != null ? int.Parse(number) : (int?) null;
        }

        private static DateTime? ParseDate(string year, string month, string day)
        {
            try
            {
                return new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}