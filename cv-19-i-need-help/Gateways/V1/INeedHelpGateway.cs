using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using CV19INeedHelp.Boundary.V1.Responses;
using CV19INeedHelp.Models.V1;
using CV19INeedHelp.Data.V1;
using CV19INeedHelp.Helpers.V1;
using CV19INeedHelp.Models;
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
            IEnumerable<ResidentSupportAnnex> response = new List<ResidentSupportAnnex>();
            if(uprn != null)
            {
                response = _dbContext.ResidentSupportAnnex
                    .Where(x => x.Uprn == uprn);
            }
            else if(postcode != null)
            {
                if (!string.IsNullOrEmpty(postcode.Trim()))
                {
                    response = _dbContext.ResidentSupportAnnex
                        .Where(x => x.Postcode.ToUpper().Contains(postcode.ToUpper()));   
                }
            }
            else
            {
                response = _dbContext.ResidentSupportAnnex;
            }
            if (isMaster)
            {
                response = response
                    .Where(x => x.RecordStatus == "MASTER");
            }
            return response.ToList();
        }

        public ResidentSupportAnnex GetSingleHelpRequest(int id)
        {
            var response = _dbContext.ResidentSupportAnnex.SingleOrDefault(x => x.Id == id);
            LambdaLogger.Log("Got record " + id + " with: " + JsonConvert.SerializeObject(response));
            return response;
        }
        
        public void UpdateHelpRequest(ResidentSupportAnnex data)
        {
            var rec = _dbContext.ResidentSupportAnnex.Find(data.Id);
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
            if (dataItems.Postcode != null)
            {
                rec.Postcode = dataItems.Postcode;
            }
            if (dataItems.Uprn != null)
            {
                rec.Uprn = dataItems.Uprn;
            }
            if (dataItems.AddressFirstLine != null)
            {
                rec.AddressFirstLine = dataItems.AddressFirstLine;
            }
            if (dataItems.AddressSecondLine != null)
            {
                rec.AddressSecondLine = dataItems.AddressSecondLine;
            }
            if (dataItems.AddressThirdLine != null)
            {
                rec.AddressThirdLine = dataItems.AddressThirdLine;
            }
            if (dataItems.EmailAddress != null)
            {
                rec.EmailAddress = dataItems.EmailAddress;
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
            return _dbContext.ResidentSupportAnnex
                .Where(x => x.RecordStatus.ToUpper() == "EXCEPTION")
                .OrderBy(x => x.Uprn)
                .ToList();
        }

        public List<DeliveryReportItem> CreateDeliverySchedule(int limit, string spreadsheet, DateTime deliveryDate)
        {
            var deliveryData = new List<DeliveryReportItem>();
            var data = GetData(limit, deliveryDate);
            var batch = new DeliveryBatch
            {
                DeliveryDate = deliveryDate,
                DeliveryPackages = data.Count,
                ReportFileId = spreadsheet
            };
            _dbContext.DeliveryBatch.Add(batch);
            _dbContext.SaveChanges();
            foreach (var record in data)
            {
                var saveRecord = new DeliveryReportItem()
                {
                    AnnexId = record.Id,
                    NumberOfPackages = 1,
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
                deliveryData.Add(saveRecord);
            }
            _dbContext.SaveChanges();
            return deliveryData;
        }

        public List<ResidentSupportAnnex> CreateTemporaryDeliveryData(int limit, DateTime deliveryDate)
        {
            return GetData(limit, deliveryDate);
        }

        public void UpdateAnnexWithDeliveryDates(List<DeliveryReportItem> data)
        {
            foreach (var item in data)
            {
                var annexRecord = _dbContext.ResidentSupportAnnex.Find(item.AnnexId);
                annexRecord.LastConfirmedFoodDelivery = item.DeliveryDate;
            }
            _dbContext.SaveChanges();
        }

        public void DeleteBatch(int id)
        {
            var batchRecord = _dbContext.DeliveryBatch.Find(id);
            if (batchRecord == null) return;
            var data = _dbContext.DeliveryReportData.Where(x => x.BatchId == batchRecord.Id);
            RevertAnnexDeliveryDates(data.ToList());
            _dbContext.DeliveryReportData.RemoveRange(data);
            _dbContext.SaveChanges();
            _dbContext.DeliveryBatch.Remove(batchRecord);
            _dbContext.SaveChanges();
        }

        private void RevertAnnexDeliveryDates(List<DeliveryReportItem> data)
        {
            foreach (var item in data)
            {
                var annexRecord = _dbContext.ResidentSupportAnnex.Find(item.AnnexId);
                annexRecord.LastConfirmedFoodDelivery = item.LastConfirmedDeliveryDate;
            }
            _dbContext.SaveChanges();
        }

        public DeliveryBatch FindExistingBatchForDate(DateTime deliveryDay)
        {
            LambdaLogger.Log($"Searching for an existing delivery batch with date {deliveryDay.Date}");
            var batchSearch = _dbContext.DeliveryBatch.FirstOrDefault(x => x.DeliveryDate == deliveryDay.Date);
            return batchSearch;
        }
        
        public DeliveryBatch GetBatchById(int id)
        {
            var batchRecord = _dbContext.DeliveryBatch.Find(id);
            return batchRecord;
        }

        public AnnexSummaryResponse GetHelpRequestsSummary()
        {
            var res = _dbContext.ResidentSupportAnnex
                .Count(x => x.IsDuplicate.ToUpper() == "FALSE"
                            && x.RecordStatus.ToUpper() == "MASTER"
                            && x.OngoingFoodNeed == true);
            return new AnnexSummaryResponse
            {
                ActiveCases = res
            };
        }

        private List<ResidentSupportAnnex> GetData(int limit, DateTime nextWorkingDay)
        {
            var response = _dbContext.ResidentSupportAnnex
                .Where(x => x.RecordStatus.ToUpper() == "MASTER"
                            && x.IsDuplicate.ToUpper() == "FALSE"
                            && x.OngoingFoodNeed == true
                            && (x.LastConfirmedFoodDelivery == null))
                .OrderBy(x => x.Id)
                .Take(limit).ToList();
            if (response.Count >= limit)
            {
                LambdaLogger.Log($"First priority returned {response.Count} records against a limit of {limit}.  Capacity reached");
                return response
                    .OrderBy(x => x.Id)
                    .ToList();
            }
            LambdaLogger.Log($"First priority returned {response.Count} records against a limit of {limit}.  Capacity not reached. Adding next priority.");
            var remainingCapacity = limit - response.Count;
            var output = _dbContext.ResidentSupportAnnex
                .Where(x => x.RecordStatus.ToUpper() == "MASTER"
                            && x.IsDuplicate.ToUpper() == "FALSE"
                            && x.OngoingFoodNeed == true
                            && (x.LastConfirmedFoodDelivery <= nextWorkingDay.AddDays(-7)))
                .OrderBy(x => x.Id)
                .Take(remainingCapacity).ToList();
            response.AddRange(output);
            if (response.Count >= limit)
            {
                LambdaLogger.Log($"Second priority returned {response.Count + output.Count} records against a limit of {limit}.  Capacity reached");
                return response
                    .OrderBy(x => x.Id)
                    .ToList();
            }
            remainingCapacity = limit - response.Count;
            LambdaLogger.Log($"Second priority returned {response.Count + output.Count} records against a limit of {limit}.  Capacity not reached. Adding next priority.");
            output = _dbContext.ResidentSupportAnnex
                .Where(x => x.RecordStatus.ToUpper() == "MASTER"
                            && x.IsDuplicate.ToUpper() == "FALSE"
                            && x.OngoingFoodNeed == true
                            && (x.LastConfirmedFoodDelivery > nextWorkingDay.AddDays(-7) && x.LastConfirmedFoodDelivery <= nextWorkingDay.AddDays(-6)))
                .OrderBy(x => x.Id)
                .Take(remainingCapacity).ToList();
            LambdaLogger.Log($"Final priority returned {response.Count + output.Count} records against a limit of {limit}.");
            response.AddRange(output);
            
            if (response.Count >= limit)
            {
                LambdaLogger.Log($"Second priority returned {response.Count + output.Count} records against a limit of {limit}.  Capacity reached");
                return response.OrderBy(x => x.Id).ToList();
            }
            remainingCapacity = limit - response.Count;
            LambdaLogger.Log($"Second priority returned {response.Count + output.Count} records against a limit of {limit}.  Capacity not reached. Adding next priority.");
            output = _dbContext.ResidentSupportAnnex
                .Where(x => x.RecordStatus.ToUpper() == "MASTER"
                            && x.IsDuplicate.ToUpper() == "FALSE"
                            && x.OngoingFoodNeed == true
                            && (x.LastConfirmedFoodDelivery > nextWorkingDay.AddDays(-6) && x.LastConfirmedFoodDelivery <= nextWorkingDay.AddDays(-5)))
                .OrderBy(x => x.Id)
                .Take(remainingCapacity).ToList();
            LambdaLogger.Log($"Final priority returned {response.Count + output.Count} records against a limit of {limit}.");
            response.AddRange(output);
            
            return response.OrderBy(x => x.Id).ToList();
        }

        public BankHoliday GetNextBankHoliday(DateTime date)
        {
            var response = _dbContext.BankHolidays
                .OrderBy(x => x.Date)
                .FirstOrDefault(x => x.Date >= date);
            return response;
        }
    }
}