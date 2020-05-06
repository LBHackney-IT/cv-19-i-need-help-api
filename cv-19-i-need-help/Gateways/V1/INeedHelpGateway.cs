using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using CV19INeedHelp.Models.V1;
using CV19INeedHelp.Data.V1;
using Newtonsoft.Json;

namespace CV19INeedHelp.Gateways.V1
{
    public class INeedHelpGateway : IINeedHelpGateway
    {
        private readonly Cv19SupportDbContext _dbContext;
        public INeedHelpGateway(Cv19SupportDbContext context)
        {
            _dbContext = context;
        }

        public List<ResidentSupportAnnex> GetHelpRequestsForUprn(string uprn, bool isMaster)
        {
            List<ResidentSupportAnnex> response = new List<ResidentSupportAnnex>();
            if (isMaster == true)
            {
                response = _dbContext.ResidentSupportAnnex
                    .Where(x => x.Uprn == uprn && x.RecordStatus.ToUpper() == "MASTER").ToList();
            }
            else
            {   
                response = _dbContext.ResidentSupportAnnex
                    .Where(x => x.Uprn == uprn).ToList();   
            }
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
            rec.LastConfirmedFoodDelivery = data.LastConfirmedFoodDelivery;
            rec.RecordStatus = data.RecordStatus;
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
        
        public void UpdateFoodDelivery(FoodDelivery data)
        {
            var rec = _dbContext.FoodDeliveries.SingleOrDefault(x => x.Id == data.Id);
            if (rec.ScheduledDeliveryDate != null)
            {
                rec.ScheduledDeliveryDate = data.ScheduledDeliveryDate;
            }
            rec.DeliveryConfirmed = data.DeliveryConfirmed;
            rec.ReasonForNonDelivery = data.ReasonForNonDelivery;
            rec.IsThisFirstDelivery = data.IsThisFirstDelivery;
            rec.RepeatDelivery = data.RepeatDelivery;
            rec.HouseholdSize = data.HouseholdSize;
            rec.FoodPackages = data.FoodPackages;
            _dbContext.SaveChanges();
        }

        public void PatchHelpRequest(int id, ResidentSupportAnnexPatch dataItems)
        {
            LambdaLogger.Log("Updating: " + JsonConvert.SerializeObject(dataItems));
            var rec = _dbContext.ResidentSupportAnnex.SingleOrDefault(x => x.Id == id);
            if (dataItems.OngoingFoodNeed != null)
            {
                rec.OngoingFoodNeed = dataItems.OngoingFoodNeed;
            }

            if (dataItems.NumberOfPeopleInHouse != null)
            {
                rec.NumberOfPeopleInHouse = dataItems.NumberOfPeopleInHouse;
            }

            if (dataItems.LastConfirmedFoodDelivery != null)
            {
                rec.LastConfirmedFoodDelivery = dataItems.LastConfirmedFoodDelivery;
            }
            
            if (dataItems.IsDuplicate != null)
            {
                rec.IsDuplicate = dataItems.IsDuplicate;
            }

            if (dataItems.DobDay != null)
            {
                rec.DobDay = dataItems.DobDay;
            }

            if (dataItems.DobMonth != null)
            {
                rec.DobMonth = dataItems.DobMonth;
            }

            if (dataItems.DobYear != null)
            {
                rec.DobYear = dataItems.DobYear;
            }

            if (dataItems.ContactTelephoneNumber != null)
            {
                rec.ContactTelephoneNumber = dataItems.ContactTelephoneNumber;
            }

            if (dataItems.ContactMobileNumber != null)
            {
                rec.ContactMobileNumber = dataItems.ContactMobileNumber;
            }

            if (dataItems.RecordStatus != null)
            {
                rec.RecordStatus = dataItems.RecordStatus;
            }


            _dbContext.SaveChanges();
        }

        public List<ResidentSupportAnnex> GetRequestExceptions()
        {
            List<ResidentSupportAnnex> response = new List<ResidentSupportAnnex>();
            response = _dbContext.ResidentSupportAnnex
                .Where(x => x.RecordStatus.ToUpper() == "EXCEPTION").ToList();
            return response;
        }
    }
}