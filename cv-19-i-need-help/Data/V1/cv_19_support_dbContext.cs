using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.Data.V1
{
    public partial class Cv19SupportDbContext : DbContext
    {
        private readonly string _connectionString;
        public Cv19SupportDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Cv19SupportDbContext(DbContextOptions<Cv19SupportDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ResidentSupportAnnex> ResidentSupportAnnex { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=cv-19-res-support-dev.cxgopqaj01vy.eu-west-2.rds.amazonaws.com;Database=cv_19_support_db;Username=postgres;Password=f&g^xsa3Du");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ResidentSupportAnnex>(entity =>
            {
                entity.ToTable("resident_support_annex");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressFirstLine)
                    .HasColumnName("address_first_line")
                    .HasColumnType("character varying");

                entity.Property(e => e.AddressSecondLine)
                    .HasColumnName("address_second_line")
                    .HasColumnType("character varying");

                entity.Property(e => e.AddressThirdLine)
                    .HasColumnName("address_third_line")
                    .HasColumnType("character varying");

                entity.Property(e => e.AnyFoodHouseholdCannotEat)
                    .HasColumnName("any_food_household_cannot_eat")
                    .HasColumnType("character varying");

                entity.Property(e => e.AnyHelpAvailable).HasColumnName("any_help_available");

                entity.Property(e => e.AnythingElse)
                    .HasColumnName("anything_else")
                    .HasColumnType("character varying");

                entity.Property(e => e.ContactMobileNumber)
                    .HasColumnName("contact_mobile_number")
                    .HasColumnType("character varying");

                entity.Property(e => e.ContactTelephoneNumber)
                    .HasColumnName("contact_telephone_number")
                    .HasColumnType("character varying");

                entity.Property(e => e.DateTimeRecorded).HasColumnName("date_time_recorded");

                entity.Property(e => e.DaysWorthOfFood)
                    .HasColumnName("days_worth_of_food")
                    .HasColumnType("character varying");

                entity.Property(e => e.DaysWorthOfMedicine)
                    .HasColumnName("days_worth_of_medicine")
                    .HasColumnType("character varying");

                entity.Property(e => e.DobDay)
                    .HasColumnName("dob_day")
                    .HasColumnType("character varying");

                entity.Property(e => e.DobMonth)
                    .HasColumnName("dob_month")
                    .HasColumnType("character varying");

                entity.Property(e => e.DobYear)
                    .HasColumnName("dob_year")
                    .HasColumnType("character varying");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("email_address")
                    .HasColumnType("character varying");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.FoodNeed).HasColumnName("food_need");

                entity.Property(e => e.FormId)
                    .HasColumnName("form_id")
                    .HasColumnType("character varying");

                entity.Property(e => e.FormVersion)
                    .HasColumnName("form_version")
                    .HasColumnType("character varying");

                entity.Property(e => e.GpSurgeryDetails)
                    .HasColumnName("gp_surgery_details")
                    .HasColumnType("character varying");

                entity.Property(e => e.IsAddressConfirmed).HasColumnName("is_address_confirmed");

                entity.Property(e => e.IsAnyAgedUnder15).HasColumnName("is_any_aged_under_15");

                entity.Property(e => e.IsDuplicate).HasColumnName("is_duplicate");

                entity.Property(e => e.IsHouseholdHelpAvailable).HasColumnName("is_household_help_available");

                entity.Property(e => e.IsOnBehalf).HasColumnName("is_on_behalf");

                entity.Property(e => e.IsPackageOfCareAsc).HasColumnName("is_package_of_care_asc");

                entity.Property(e => e.IsPharmacistAbleToDeliver).HasColumnName("is_pharmacist_able_to_deliver");

                entity.Property(e => e.IsUrgentFood).HasColumnName("is_urgent_food");

                entity.Property(e => e.IsUrgentFoodRequired).HasColumnName("is_urgent_food_required");

                entity.Property(e => e.IsUrgentMedicineRequired).HasColumnName("is_urgent_medicine_required");

                entity.Property(e => e.IsUrgentPrescription).HasColumnName("is_urgent_prescription");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.NameAddressPharmacist)
                    .HasColumnName("name_address_pharmacist")
                    .HasColumnType("character varying");

                entity.Property(e => e.NumberOfPeopleInHouse)
                    .HasColumnName("number_of_people_in_house")
                    .HasColumnType("character varying");

                entity.Property(e => e.OnBehalfContactNumber)
                    .HasColumnName("on_behalf_contact_number")
                    .HasColumnType("character varying");

                entity.Property(e => e.OnBehalfEmailAddress)
                    .HasColumnName("on_behalf_email_address")
                    .HasColumnType("character varying");

                entity.Property(e => e.OnBehalfFirstName)
                    .HasColumnName("on_behalf_first_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.OnBehalfLastName)
                    .HasColumnName("on_behalf_last_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.OngoingFoodNeed).HasColumnName("ongoing_food_need");

                entity.Property(e => e.OngoingPrescriptionNeed).HasColumnName("ongoing_prescription_need");

                entity.Property(e => e.Postcode)
                    .HasColumnName("postcode")
                    .HasColumnType("character varying");

                entity.Property(e => e.RelationshipWithResident)
                    .HasColumnName("relationship_with_resident")
                    .HasColumnType("character varying");

                entity.Property(e => e.StrugglingToPayForFood).HasColumnName("struggling_to_pay_for_food");

                entity.Property(e => e.Uprn)
                    .HasColumnName("uprn")
                    .HasColumnType("character varying");

                entity.Property(e => e.Ward)
                    .HasColumnName("ward")
                    .HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
