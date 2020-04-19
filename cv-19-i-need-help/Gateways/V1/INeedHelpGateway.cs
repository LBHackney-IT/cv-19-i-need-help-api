using System;
using System.Collections.Generic;
using System.Linq;
using CV19INeedHelp.Models.V1;
using CV19INeedHelp.Data.V1;

namespace CV19INeedHelp.Gateways.V1
{
    public class INeedHelpGateway : IINeedHelpGateway
    {
        private readonly string _connectionString;
        private readonly Cv19SupportDbContext _dbContext;
        public INeedHelpGateway(string connectionString)
        {
            _connectionString = connectionString;
            _dbContext = new Cv19SupportDbContext(_connectionString);
        }

        public List<ResidentSupportAnnex> GetAllHelpRequests()
        {
            var response = _dbContext.ResidentSupportAnnex.ToList();
            return response;
        }
        
        public ResidentSupportAnnex GetSingleHelpRequest(int id)
        {
            var response = _dbContext.ResidentSupportAnnex.SingleOrDefault(x => x.Id == id);
            return response;
        }
        
        public void UpdateHelpRequest(ResidentSupportAnnex data)
        {
            var rec = _dbContext.ResidentSupportAnnex.SingleOrDefault(x => x.Id == data.Id);
            rec.IsDuplicate = data.IsDuplicate;
            rec.OngoingFoodNeed = data.OngoingFoodNeed;
            rec.OngoingPrescriptionNeed = data.OngoingPrescriptionNeed;
            rec.FirstName = data.FirstName;
            rec.LastName = data.LastName;
            rec.DobMonth = data.DobMonth;
            rec.DobYear = data.DobYear;
            rec.DobDay = data.DobDay;
            rec.Postcode = data.Postcode;
            rec.Uprn = data.Uprn;
            rec.Ward = data.Ward;
            rec.AddressFirstLine = data.AddressFirstLine;
            rec.AddressSecondLine = data.AddressSecondLine;
            rec.AddressThirdLine = data.AddressThirdLine;
            rec.ContactTelephoneNumber = data.ContactTelephoneNumber;
            rec.ContactMobileNumber = data.ContactMobileNumber;
            rec.EmailAddress = data.EmailAddress;
            rec.IsOnBehalf = data.IsOnBehalf;
            rec.OnBehalfFirstName = data.OnBehalfFirstName;
            rec.OnBehalfLastName = data.OnBehalfLastName;
            rec.OnBehalfEmailAddress = data.OnBehalfEmailAddress;
            rec.OnBehalfContactNumber = data.OnBehalfContactNumber;
            rec.RelationshipWithResident = data.RelationshipWithResident;
            rec.AnythingElse = data.AnythingElse;
            rec.GpSurgeryDetails = data.GpSurgeryDetails;
            rec.FoodNeed = data.FoodNeed;
            rec.NumberOfPeopleInHouse = data.NumberOfPeopleInHouse;
            rec.DaysWorthOfFood = data.DaysWorthOfFood;
            rec.AnyFoodHouseholdCannotEat = data.AnyFoodHouseholdCannotEat;
            rec.StrugglingToPayForFood = data.StrugglingToPayForFood;
            rec.IsPharmacistAbleToDeliver = data.IsPharmacistAbleToDeliver;
            rec.NameAddressPharmacist = data.NameAddressPharmacist;
            rec.IsPackageOfCareAsc = data.IsPackageOfCareAsc;
            rec.IsUrgentFoodRequired = data.IsUrgentFoodRequired;
            rec.DaysWorthOfMedicine = data.DaysWorthOfMedicine;
            rec.IsUrgentMedicineRequired = data.IsUrgentMedicineRequired;
            rec.IsAddressConfirmed = data.IsAddressConfirmed;
            rec.IsHouseholdHelpAvailable = data.IsHouseholdHelpAvailable;
            rec.IsUrgentFood = data.IsUrgentFood;
            rec.IsUrgentPrescription = data.IsUrgentPrescription;
            rec.AnyHelpAvailable = data.AnyHelpAvailable;
            rec.IsAnyAgedUnder15 = data.IsAnyAgedUnder15;
            _dbContext.SaveChanges();
        }
        
        public List<FoodDelivery> GetFoodDeliveriesForForm(int id)
        {
            var response = _dbContext.FoodDeliveries.Where(x => x.AnnexId == id).ToList();
            return response;
        }

        public int CreateFoodDelivery(FoodDelivery data)
        {
            _dbContext.FoodDeliveries.Add(data);
            _dbContext.SaveChanges();
            return data.Id;
        }

    }
}