using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using CV19INeedHelp.Models.V1;
using CV19INeedHelp.Data.V1;
using CV19INeedHelp.Helpers.V1;
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

        public List<ResidentSupportAnnex> GetHelpRequestsForUprn(string uprn, string postcode, bool isMaster)
        {
            List<ResidentSupportAnnex> response = new List<ResidentSupportAnnex>();
            if(uprn != null)
            {   
                response = _dbContext.ResidentSupportAnnex
                    .Where(x => x.Uprn == uprn).ToList();   
            }
            else if(postcode != null)
            {
                if (!string.IsNullOrEmpty(postcode.Trim()))
                {
                    response = _dbContext.ResidentSupportAnnex
                        .Where(x => x.Postcode.ToUpper().Contains(postcode.ToUpper())).ToList();   
                }
            }
            else
            {
                response = _dbContext.ResidentSupportAnnex.ToList();
            }
            if (isMaster == true)
            {
                response = response
                    .Where(x => x.RecordStatus == "MASTER").ToList();
            }
            return response;
        }
        
        public ResidentSupportAnnex GetSingleHelpRequest(int id)
        {
            var response = _dbContext.ResidentSupportAnnex.SingleOrDefault(x => x.Id == id);
            LambdaLogger.Log("Got record " + id + " with: " + JsonConvert.SerializeObject(response));
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
            rec.DeliveryNotes = data.DeliveryNotes;
            rec.CaseNotes = data.CaseNotes;
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
            LambdaLogger.Log("Updating record " + id + " with: " + JsonConvert.SerializeObject(dataItems));
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
            if (dataItems.FirstName != null)
            {
                rec.FirstName = dataItems.FirstName;
            }
            if (dataItems.LastName != null)
            {
                rec.LastName = dataItems.LastName;
            }
            if (dataItems.DeliveryNotes != null)
            {
                rec.DeliveryNotes = dataItems.DeliveryNotes;
            }
            if (dataItems.CaseNotes != null)
            {
                rec.CaseNotes = dataItems.CaseNotes;
            }
            _dbContext.SaveChanges();
        }

        public List<ResidentSupportAnnex> GetRequestExceptions()
        {
            List<ResidentSupportAnnex> response = new List<ResidentSupportAnnex>();
            response = _dbContext.ResidentSupportAnnex
                .Where(x => x.RecordStatus.ToUpper() == "EXCEPTION")
                .OrderBy(x => x.Uprn).ToList();
            return response;
        }

        public List<DeliveryReportItem> CreateDeliverySchedule(int limit, string spreadsheet)
        {
            var helper = new UtilityHelper();
            var deliveryDate = helper.GetNextWorkingDay();
            var deliveryData = new List<DeliveryReportItem>();
            var data = GetData(limit);
            var batch = new DeliveryBatch
            {
                DeliveryDate = deliveryDate,
                DeliveryPackages = data.Count(),
                ReportFileId = spreadsheet
            };
            _dbContext.DeliveryBatch.Add(batch);
            _dbContext.SaveChanges();
            foreach (var record in data)
            {
                var saveRecord = new DeliveryReportItem()
                {
                    AnnexId = record.Id,
                    NumberOfPackages = data.Count(),
                    AnyFoodHouseholdCannotEat = record.AnyFoodHouseholdCannotEat,
                    BatchId = batch.Id,
                    FullName = $"{record.FirstName} {record.LastName}",
                    FullAddress = $"{record.AddressFirstLine} {record.AddressSecondLine} {record.AddressThirdLine}",
                    Postcode = record.Postcode,
                    Uprn = record.Uprn,
                    TelephoneNumber = record.ContactTelephoneNumber,
                    MobileNumber = record.ContactMobileNumber,
                    DeliveryDate = deliveryDate,
                    LastConfirmedDeliveryDate = record.LastConfirmedFoodDelivery,
                    DeliveryNotes = record.DeliveryNotes
                };
                _dbContext.DeliveryReportData.Add(saveRecord);
                _dbContext.SaveChanges();
                deliveryData.Add(saveRecord);
            }
            return deliveryData;
        }

        public List<ResidentSupportAnnex> CreateTemporaryDeliveryData(int limit)
        {
            return GetData(limit);
        }

        private List<ResidentSupportAnnex> GetData(int limit)
        {
            var response = _dbContext.ResidentSupportAnnex
                .Where(x => x.RecordStatus.ToUpper() == "MASTER"
                            && x.IsDuplicate.ToUpper() == "FALSE"
                            && x.OngoingFoodNeed == true
                            && (x.LastConfirmedFoodDelivery == null))
                .OrderByDescending(x => x.Id)
                .Take(limit).ToList();
            if (response.Count() == limit)
            {
                LambdaLogger.Log($"First priority returned {response.Count()} records against a limit of {limit}.  Capacity reached");
                return response;
            }
            LambdaLogger.Log($"First priority returned {response.Count()} records against a limit of {limit}.  Capacity not reached. Adding next priority.");
            var remainingCapacity = limit - response.Count();
            var output = _dbContext.ResidentSupportAnnex
                .Where(x => x.RecordStatus.ToUpper() == "MASTER"
                            && x.IsDuplicate.ToUpper() == "FALSE"
                            && x.OngoingFoodNeed == true
                            && (x.LastConfirmedFoodDelivery <= DateTime.Now.AddDays(-6)))
                .OrderByDescending(x => x.Id)
                .Take(remainingCapacity).ToList();
            if (output.Count() == limit)
            {
                LambdaLogger.Log($"Second priority returned {response.Count()} records against a limit of {limit}.  Capacity reached");
                response.AddRange(output);
                return response;
            }
            
            LambdaLogger.Log($"Second priority returned {response.Count()} records against a limit of {limit}.  Capacity not reached. Adding next priority.");
            remainingCapacity = limit - response.Count();
           output = _dbContext.ResidentSupportAnnex
                .Where(x => x.RecordStatus.ToUpper() == "MASTER"
                            && x.IsDuplicate.ToUpper() == "FALSE"
                            && x.OngoingFoodNeed == true
                            && (x.LastConfirmedFoodDelivery > DateTime.Now.AddDays(-6) && x.LastConfirmedFoodDelivery <= DateTime.Now.AddDays(-4)))
                .OrderByDescending(x => x.Id)
                .Take(remainingCapacity).ToList();
            LambdaLogger.Log($"Final priority returned {response.Count()} records against a limit of {limit}.");
            response.AddRange(output);
            return response;
        }
    }
}