using System.Data.Entity.ModelConfiguration.Conventions;
using UMPG.USL.Models;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.Reports;
using UMPG.USL.Models.Security;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.AuditModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Migrations;


namespace UMPG.USL.API.Data
{
    using UMPG.USL.Models.LicenseGenerate;

    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext(): base("AuthContext")
        {
            Database.SetInitializer<AuthContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;

        }
        
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactDefault> ContactDefaults { get; set; }

        public DbSet<ContactGeneratedLicenseQueue> ContactGeneratedLicenseQueue { get; set; }

        public DbSet<ContactEmail> ContactEmails { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Licensee> Licensees { get; set; }

        public DbSet<License> Licenses { get; set; }

        public DbSet<LicenseNote> LicenseNotes { get; set; }

        public DbSet<LicenseProductRecording> LicenseProductRecordings { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Action> Actions { get; set; }

        public DbSet<LicenseProductRecordingWriter> LicenseProductRecordingWriters { get; set; }

        public DbSet<LicenseProductRecordingWriterNote> LicenseProductRecordingWriterNotes { get; set; }

        public DbSet<LicenseProductRecordingWriterRateStatus> LicenseProductRecordingWriterRateStatuses { get; set; }

        public DbSet<LicenseProductRecordingWriterRate> LicenseProductRecordingWriterRates { get; set; }

        //public DbSet<LicenseProductRecordingWriterRateNote> LicenseProductRecordingWriterRateNotes { get; set; }
        
        public DbSet<LU_LicenseType> LU_LicenseTypes { get; set; }

        public DbSet<LU_LicenseMethod> LU_LicenseMethods { get; set; }

        public DbSet<LU_Priority> LU_Priorities { get; set; }

        public DbSet<LU_LicenseStatus> LU_LicenseStatuses { get; set; }

        public DbSet<LU_RateType> LU_RateTypes { get; set; }

        public DbSet<LU_WritersConsentType> LU_WritersConsentTypes { get; set; }

        public DbSet<LU_PaidQuarterType> LU_PaidQuarterTypes { get; set; }

        public DbSet<LU_WritersIncludeExcludeType> LU_WritersIncludeExcludeTypes { get; set; }

        public DbSet<LU_SpecialStatus> LU_SpecialStatuses { get; set; }

        public DbSet<LU_TrackType> Lu_TrackTypes { get; set; }

        public DbSet<LU_NoteType> LU_NoteTypes { get; set; }

        public DbSet<LU_PaidQuarter> LU_PaidQuarters { get; set; }
        public DbSet<LU_Schedule> LU_Schedules { get; set; }

        public DbSet<LicenseProduct> LicenseProducts { get; set; }

        public DbSet<LicenseProductConfiguration> LicenseProductConfigurations { get; set; }

        public DbSet<LicenseSequence> LicenseSequence { get; set; }

        //public DbSet<ProductRecordingLink> ProductRecordingLink { get; set; }
        public DbSet<LicenseRecordingMedley> LicenseRecordingMedleys { get; set; }
       
        public DbSet<Phone> Phones { get; set; }

        public DbSet<LicenseeLabelGroup> LicenseeLabelGroups { get; set; }

        public DbSet<LicenseeLabelGroupLink> LicenseeLabelGroupLinks { get; set; }

        public DbSet<SendLicenseInfo> SendIssueLicenses { get; set; }
        public DbSet<SendLicenseContact> SendIssueLicenseContacts { get; set; }

        public DbSet<LicenseAttachment> LicenseAttachments { get; set; }

        public DbSet<GenerateLicenseQueue> GenerateLicenseQueue { get; set; }
        public DbSet<GenerateLicenseAttachment> GenerateLicenseAttachment { get; set; }

        public DbSet<Audit> Audits { get; set; }

        public DbSet<AgreementStatutoryRate> AgreementStatutoryRate { get; set; }
        public DbSet<StatRateDate> StatRateDate { get; set; }
        public DbSet<StatRateTime> StatRateTime { get; set; }
        public DbSet<StatRate> StatRate { get; set; }

        public DbSet<RecordingMedley> RecordingMedleys { get; set; }
        public DbSet<SolrIndexQueueItem> SolrIndexQueues { get; set; }
        public DbSet<SolrSynchronizationJob> SolrSynchronizationJobs { get; set; }


        //Audit Tables
        public DbSet<AuditLicense> AuditLicenses { get; set; }

        public DbSet<AuditLicenseProductConfiguration> AuditLicenseProductConfigurations { get; set; }
        public DbSet<AuditLicenseAttachment> AuditLicenseAttachments { get; set; }
        public DbSet<AuditLicensee> AuditLicensees { get; set; }
        //public DbSet<AuditLicenseeLabelGroup> AuditLicenseeLabelGroups { get; set; }
        //public DbSet<AuditLicenseeLabelGroupLink> AuditLicenseeLabelGroupLink { get; set; }
        public DbSet<AuditLicenseNote> AuditLicenseNotes { get; set; }
        public DbSet<AuditLicenseProduct> AuditLicenseProducts { get; set; }
        public DbSet<AuditLicenseProductRecording> AuditLicenseProductRecordings { get; set; }
        public DbSet<AuditLicenseProductRecordingWriter> AuditLicenseProductRecordingWriters { get; set; }
        public DbSet<AuditLicenseProductRecordingWriterNote> AuditLicenseProductRecordingWriterNotes { get; set; }
        public DbSet<AuditLicenseProductRecordingWriterRate> AuditLicenseProductRecordingWriterRates { get; set; }
        public DbSet<AuditLicenseProductRecordingWriterRateStatus> AuditLicenseProductRecordingWriterRateStatuses { get; set; }
        //public DbSet<AuditLicenseRecordingMedley> AuditLicenseRecordingMedleys { get; set; }
        //public DbSet<AuditRecordingMedley> AuditRecordingMedleys { get; set; }
        //Reports
        public DbSet<ReportQueue> ReportQueues { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("dbo");

            // Contacts
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<Contact>().HasKey(c => c.ContactId);
            modelBuilder.Entity<Contact>().HasRequired(c => c.Role).WithMany().HasForeignKey(c => c.RoleId);
            modelBuilder.Entity<Contact>().HasMany(c => c.Address).WithOptional().HasForeignKey(c => c.ContactId);
            modelBuilder.Entity<Contact>().HasMany(c => c.Phone).WithOptional().HasForeignKey(c => c.ContactId);
            modelBuilder.Entity<Contact>().HasMany(c => c.Email).WithOptional().HasForeignKey(c => c.ContactId);

            //Phone
            modelBuilder.Entity<Phone>().ToTable("Phone");
            modelBuilder.Entity<Phone>().HasKey(c => c.PhoneId);

            // Address
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Address>().HasKey(c => c.AddressId);

            // ContactDefaults
            modelBuilder.Entity<ContactDefault>().ToTable("ContactDefault");
            modelBuilder.Entity<ContactDefault>().HasKey(c => c.ContactDefaultId);

            //ContactGeneratedLicenseQueue
            modelBuilder.Entity<ContactGeneratedLicenseQueue>().ToTable("GenerateLicenseQueueContact");
            modelBuilder.Entity<ContactGeneratedLicenseQueue>().HasKey(c => c.GenerateLicenseQueueContactId);

            //GenerateLicenseQueue
            modelBuilder.Entity<GenerateLicenseQueue>().ToTable("GenerateLicenseQueue");
            modelBuilder.Entity<GenerateLicenseQueue>().HasKey(c => c.GenerateLicenseQueueId);

            // ContactEmails
            modelBuilder.Entity<ContactEmail>().ToTable("ContactEmail");
            modelBuilder.Entity<ContactEmail>().HasKey(c => c.ContactEmailId);

            // Licenses
            modelBuilder.Entity<License>().ToTable("License","dbo");
            modelBuilder.Entity<License>().HasKey(c => c.LicenseId);
            modelBuilder.Entity<License>().HasRequired(c => c.LicenseType).WithMany().HasForeignKey(c => c.LicenseTypeId);
            modelBuilder.Entity<License>().HasRequired(c => c.LicensePriority).WithMany().HasForeignKey(c => c.PriorityId);
            modelBuilder.Entity<License>().HasRequired(c => c.Licensee).WithMany().HasForeignKey(c => c.LicenseeId);
            modelBuilder.Entity<License>().HasRequired(c => c.LicenseStatus).WithMany().HasForeignKey(c => c.LicenseStatusId);
            modelBuilder.Entity<License>().HasRequired(c => c.LicenseMethod).WithMany().HasForeignKey(c => c.LicenseMethodId);
            modelBuilder.Entity<License>().HasRequired(c => c.Contact).WithMany().HasForeignKey(c => c.AssignedToId);
            modelBuilder.Entity<License>().HasRequired(c => c.Contact2).WithMany().HasForeignKey(c => c.CreatedBy);
            modelBuilder.Entity<License>().HasRequired(c => c.LicenseeContact).WithMany().HasForeignKey(c => c.ContactId);
            modelBuilder.Entity<License>().Ignore(c => c.ProductsNo);
            modelBuilder.Entity<License>().Ignore(c => c.Label);
            modelBuilder.Entity<License>().Ignore(c => c.LicenseSpecialStatusList);
            modelBuilder.Entity<License>().Ignore(c => c.ClaimException);
            modelBuilder.Entity<License>().Ignore(c => c.StatusesRollup);


            //LicenseProduct
            modelBuilder.Entity<LicenseProduct>().ToTable("LicenseProduct");
            modelBuilder.Entity<LicenseProduct>().HasKey(c => c.LicenseProductId);
            modelBuilder.Entity<LicenseProduct>().HasRequired(c => c.Schedule).WithMany().HasForeignKey(c => c.ScheduleId);
            modelBuilder.Entity<LicenseProduct>().Ignore(c => c.LicensePRecordingsNo);
            modelBuilder.Entity<LicenseProduct>().Ignore(c => c.ProductConfigurations);
            modelBuilder.Entity<LicenseProduct>().Ignore(c => c.title);
            modelBuilder.Entity<LicenseProduct>().Ignore(c => c.ProductHeader);
            modelBuilder.Entity<LicenseProduct>().Ignore(c => c.Recordings);
            modelBuilder.Entity<LicenseProduct>().Ignore(c => c.RelatedLicensesNo);
            modelBuilder.Entity<LicenseProduct>().Ignore(c => c.LicenseClaimException);
            modelBuilder.Entity<LicenseProduct>().Ignore(c => c.TotalLicenseConfigAmount);
            modelBuilder.Entity<LicenseProduct>().Ignore(c => c.LicenseProductConfigurations);
            modelBuilder.Entity<LicenseProduct>().Ignore(c => c.LicenseProductRecordings);

            //LicenseProductConfiguration
            modelBuilder.Entity<LicenseProductConfiguration>().ToTable("LicenseProductConfiguration");
            modelBuilder.Entity<LicenseProductConfiguration>().HasKey(c => c.LicenseProductConfigurationId);
            modelBuilder.Entity<LicenseProductConfiguration>().HasRequired(c => c.LicenseProduct).WithMany().HasForeignKey(c => c.LicenseProductId);
            modelBuilder.Entity<LicenseProductConfiguration>().Ignore(c => c.RecsProductConfiguration);
            modelBuilder.Entity<LicenseProductConfiguration>().Ignore(c => c.ConfigurationType);
            modelBuilder.Entity<LicenseProductConfiguration>().Ignore(c => c.TotalAmount);
            modelBuilder.Entity<LicenseProductConfiguration>().Ignore(c => c.LicensedAmount);
            modelBuilder.Entity<LicenseProductConfiguration>().Ignore(c => c.NotLicensedAmount);
            modelBuilder.Entity<LicenseProductConfiguration>().Ignore(c => c.upc_code);
            // modelBuilder.Entity<LicenseProductConfiguration>().Ignore(c => c.product_configuration_id);


            //LicenseProductRecording
            modelBuilder.Entity<LicenseProductRecording>().ToTable("LicenseRecording");
            modelBuilder.Entity<LicenseProductRecording>().HasKey(c => c.LicenseRecordingId);
            modelBuilder.Entity<LicenseProductRecording>().Ignore(c => c.RecsRecording);
            modelBuilder.Entity<LicenseProductRecording>().Ignore(c => c.LicensePRWriterNo);
            modelBuilder.Entity<LicenseProductRecording>().Ignore(c => c.LicensePRLicensedWriterNo);
            modelBuilder.Entity<LicenseProductRecording>().Ignore(c => c.LicensePRUnLicensedWriterNo);
            modelBuilder.Entity<LicenseProductRecording>().Ignore(c => c.CDVolume);
            modelBuilder.Entity<LicenseProductRecording>().Ignore(c => c.CDTrackNumber);
            modelBuilder.Entity<LicenseProductRecording>().Ignore(c => c.WorkCode);
            modelBuilder.Entity<LicenseProductRecording>().Ignore(c => c.LicensePRWriters);
            modelBuilder.Entity<LicenseProductRecording>().Ignore(c => c.StatusRollup);


            // Role
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Role>().HasMany(r => r.Actions).WithMany().Map(m => { m.ToTable("RoleAction"); m.MapLeftKey("RoleId"); m.MapRightKey("ActionId"); });
            
            // Action
            modelBuilder.Entity<Action>().ToTable("Action");
            modelBuilder.Entity<Action>().HasKey(c => c.ActionId);
            modelBuilder.Entity<Action>().Property(a => a.Name).HasColumnName("Action");
            
            //LicenseProductRecordingWriter
            modelBuilder.Entity<LicenseProductRecordingWriter>().ToTable("LicenseWriter");
            modelBuilder.Entity<LicenseProductRecordingWriter>().HasKey(c => c.LicenseWriterId);
            //modelBuilder.Entity<LicenseProductRecordingWriter>().Ignore(c => c.SpecialStatusList);
            modelBuilder.Entity<LicenseProductRecordingWriter>().Ignore(c => c.RateList);
            modelBuilder.Entity<LicenseProductRecordingWriter>().Ignore(c => c.Publisher);
            modelBuilder.Entity<LicenseProductRecordingWriter>().Ignore(c => c.MostRecentNote);
            modelBuilder.Entity<LicenseProductRecordingWriter>().Ignore(c => c.WriterNoteCount);
            modelBuilder.Entity<LicenseProductRecordingWriter>().Property(c => c.ExecutedSplit).HasPrecision(9, 6);
            modelBuilder.Entity<LicenseProductRecordingWriter>().Property(c => c.SplitOverride).HasPrecision(9, 6);
            modelBuilder.Entity<LicenseProductRecordingWriter>().Property(c => c.ClaimExceptionOverride).HasPrecision(9, 6);

            //LicenseProductRecordingWriterNote
            modelBuilder.Entity<LicenseProductRecordingWriterNote>().ToTable("LicenseWriterNote");
            modelBuilder.Entity<LicenseProductRecordingWriterNote>().HasKey(c => c.LicenseWriterNoteId);
            
            //LicenseProductRecordingWriterRate
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().ToTable("LicenseWriterRate");
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().HasKey(c => c.LicenseWriterRateId);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().HasRequired(c => c.RateType).WithMany().HasForeignKey(c => c.RateTypeId);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().HasRequired(c => c.WritersConsentType).WithMany().HasForeignKey(c => c.writersConsentTypeId);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Ignore(c => c.RateNoteCount);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Ignore(c => c.MostRecentNote);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Ignore(c => c.configuration_type);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Property(c => c.Rate).HasPrecision(7,6);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Property(c => c.PerSongRate).HasPrecision(7, 6);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Property(c => c.ProRataRate).HasPrecision(7, 6);
         

            //LicenseProductRecordingWriterRateNote
            //modelBuilder.Entity<LicenseProductRecordingWriterRateNote>().ToTable("LicenseWriterRateNote");
            //modelBuilder.Entity<LicenseProductRecordingWriterRateNote>().HasKey(c => c.LicenseWriterRateNoteId);
            
            
            //LicenseProductRecordingWriterRateStatus
            modelBuilder.Entity<LicenseProductRecordingWriterRateStatus>().ToTable("LicenseWriterRateStatus");
            modelBuilder.Entity<LicenseProductRecordingWriterRateStatus>().HasKey(c => c.LicenseWriterRateStatusId);
            modelBuilder.Entity<LicenseProductRecordingWriterRateStatus>().Ignore(c => c.LU_SpecialStatuses);
            //modelBuilder.Entity<LicenseProductRecordingWriterRateStatus>().HasRequired(c => c.LU_SpecialStatuses).WithMany().HasForeignKey(c => c.SpecialStatusId);
            
            // LU_LicenseTypes      
            modelBuilder.Entity<LU_LicenseType>().ToTable("LU_LicenseType");
            modelBuilder.Entity<LU_LicenseType>().HasKey(c => c.LicenseTypeId);
            modelBuilder.Entity<LU_LicenseType>().Property(a => a.LicenseType).HasColumnName("LicenseType");


            // LU_Priorities
            modelBuilder.Entity<LU_Priority>().ToTable("LU_Priority");
            modelBuilder.Entity<LU_Priority>().HasKey(c => c.PriorityId);
            modelBuilder.Entity<LU_Priority>().Property(a => a.Priority).HasColumnName("Priority");

            // LU_LicenseStatuses
            modelBuilder.Entity<LU_LicenseStatus>().ToTable("LU_LicenseStatus");
            modelBuilder.Entity<LU_LicenseStatus>().HasKey(c => c.LicenseStatusId);

            // LU_PaidQuarters
            modelBuilder.Entity<LU_PaidQuarter>().ToTable("LU_PaidQuarter");
            modelBuilder.Entity<LU_PaidQuarter>().HasKey(c => c.lU_PaidQuarterId);

            // LU_Schedules
            modelBuilder.Entity<LU_Schedule>().ToTable("LU_Schedule");
            modelBuilder.Entity<LU_Schedule>().HasKey(c => c.ScheduleId);

            // Licensee
            modelBuilder.Entity<Licensee>().ToTable("Licensee");
            modelBuilder.Entity<Licensee>().HasKey(c => c.LicenseeId);
        //    modelBuilder.Entity<Licensee>().HasRequired(c => c.Contact).WithMany().HasForeignKey(c => c.ContactId);
            modelBuilder.Entity<Licensee>().HasMany(r => r.LicenseeLabelGroup).WithOptional().HasForeignKey(c => c.LicenseeId);
            modelBuilder.Entity<Licensee>().HasMany(r => r.LicenseeContacts).WithOptional().HasForeignKey(c => c.LicenseeId);
            //modelBuilder.Entity<Licensee>().HasMany(c => c.Address).WithOptional().HasForeignKey(c => c.LicenseeId);
            //modelBuilder.Entity<Licensee>().HasMany(s => s.LicenseeLabelGroup).WithRequired(s=>s.Licensee).HasForeignKey(s => s.LicenseeId);
            modelBuilder.Entity<Licensee>().Ignore(c => c.LicenseeLabelGroupFiltered);
            modelBuilder.Entity<Licensee>().Ignore(c => c.LicenseeContactsFiltered);
        
            // LicenseeLabelGroupLink
            modelBuilder.Entity<LicenseeLabelGroupLink>().ToTable("LicenseeLabelGroupLink");
            modelBuilder.Entity<LicenseeLabelGroupLink>().HasKey(c => c.LicenseeLabelGroupLinkId);
            //modelBuilder.Entity<LicenseeLabelGroupLink>().HasRequired(p => p.LicenseeLabelGroup).WithMany().HasForeignKey(p => p.LicenseeLabelGroupId);
            modelBuilder.Entity<LicenseeLabelGroupLink>().HasRequired(c => c.Contact).WithMany().HasForeignKey(c => c.ContactId);
            modelBuilder.Entity<LicenseeLabelGroupLink>().Ignore(c => c.FullName);

            // LicenseeLabelGroup
            modelBuilder.Entity<LicenseeLabelGroup>().ToTable("LicenseeLabelGroup");
            modelBuilder.Entity<LicenseeLabelGroup>().HasKey(c => c.LicenseeLabelGroupId);
            //modelBuilder.Entity<LicenseeLabelGroup>().HasRequired(p => p.Licensee).WithMany().HasForeignKey(p => p.LicenseeId);
            modelBuilder.Entity<LicenseeLabelGroup>().HasMany(r => r.LabelGroupLinks).WithOptional().HasForeignKey(c => c.LicenseeLabelGroupId);
           // modelBuilder.Entity<LicenseeLabelGroup>().HasRequired(c => c.Licensee).WithMany(c=>c.LicenseeLabelGroup).HasForeignKey(r => r.LicenseeId);
            modelBuilder.Entity<LicenseeLabelGroup>().Ignore(c => c.LabelGroupLinksFiltered);
            
            

            // LicenseAttachments
            modelBuilder.Entity<LicenseAttachment>().ToTable("LicenseAttachment");
            modelBuilder.Entity<LicenseAttachment>().HasKey(c => c.licenseAttachmentId);
            modelBuilder.Entity<LicenseAttachment>().HasRequired(c => c.Contact).WithMany().HasForeignKey(c => c.CreatedBy);

            // LicenseNote
            modelBuilder.Entity<LicenseNote>().ToTable("LicenseNote");
            modelBuilder.Entity<LicenseNote>().HasKey(c => c.licenseNoteId);
            modelBuilder.Entity<LicenseNote>().HasRequired(c => c.NoteType).WithMany().HasForeignKey(c => c.NoteTypeId);
            modelBuilder.Entity<LicenseNote>().HasRequired(c => c.Contact).WithMany().HasForeignKey(c => c.CreatedBy);

            // LU_LicenseMethods
            modelBuilder.Entity<LU_LicenseMethod>().ToTable("LU_LicenseMethod");
            modelBuilder.Entity<LU_LicenseMethod>().HasKey(c => c.LicenseMethodId);

            // LU_RateTypes
            modelBuilder.Entity<LU_RateType>().ToTable("LU_RateType");
            modelBuilder.Entity<LU_RateType>().HasKey(c => c.RateTypeId);

            // LU_WritersConsentTypes
            modelBuilder.Entity<LU_WritersConsentType>().ToTable("LU_WritersConsentType");
            modelBuilder.Entity<LU_WritersConsentType>().HasKey(c => c.WritersConsentTypeId);

            // LU_PaidQuarterTypes
            modelBuilder.Entity<LU_PaidQuarterType>().ToTable("LU_PaidQuarterType");
            modelBuilder.Entity<LU_PaidQuarterType>().HasKey(c => c.WritersConsentTypeId);

            // LU_WritersIncludeExcludeTypes
            modelBuilder.Entity<LU_WritersIncludeExcludeType>().ToTable("LU_WritersIncludeExcludeType");
            modelBuilder.Entity<LU_WritersIncludeExcludeType>().HasKey(c => c.WritersConsentTypeId);

            // LU_SpecialStatus
            modelBuilder.Entity<LU_SpecialStatus>().ToTable("LU_SpecialStatus");
            modelBuilder.Entity<LU_SpecialStatus>().HasKey(c => c.SpecialStatusId);

            // LU_NoteType
            modelBuilder.Entity<LU_NoteType>().ToTable("LU_NoteType");
            modelBuilder.Entity<LU_NoteType>().HasKey(c => c.NoteTypeId);

            //LicenseSequence
            modelBuilder.Entity<LicenseSequence>().ToTable("LicenseSequence");
            modelBuilder.Entity<LicenseSequence>().HasKey(c => c.LicenseSequenceId);

            //LicenseSent
            modelBuilder.Entity<SendLicenseInfo>().ToTable("LicenseSent");
            modelBuilder.Entity<SendLicenseInfo>().HasKey(c => c.LicenseSentId);

            //LicenseSentContact
            modelBuilder.Entity<SendLicenseContact>().ToTable("LicenseSentContact");
            modelBuilder.Entity<SendLicenseContact>().HasKey(c => c.LicenseSentContactId);
            modelBuilder.Entity<SendLicenseContact>().Ignore(c => c.Action);

            //AgreementStatutoryRate
            modelBuilder.Entity<AgreementStatutoryRate>().ToTable("AgreementStatutoryRate");
            modelBuilder.Entity<AgreementStatutoryRate>().HasKey(c => c.Year);

            //StatRateDate
            modelBuilder.Entity<StatRateDate>().ToTable("StatRateDate");
            modelBuilder.Entity<StatRateDate>().HasKey(c => c.DateId);

            //StatRateTime
            modelBuilder.Entity<StatRateTime>().ToTable("StatRateTime");
            modelBuilder.Entity<StatRateTime>().HasKey(c => c.TimeId);

            //StatRate
            modelBuilder.Entity<StatRate>().ToTable("StatRate");
            modelBuilder.Entity<StatRate>().HasKey(c => c.id);
            modelBuilder.Entity<StatRate>().HasRequired(c => c.StatRateDate).WithMany().HasForeignKey(c => c.DateId);
            modelBuilder.Entity<StatRate>().HasRequired(c => c.StatRateTime).WithMany().HasForeignKey(c => c.TimeId);

            // LU_TrackType
            modelBuilder.Entity<LU_TrackType>().ToTable("LU_TrackType");
            modelBuilder.Entity<LU_TrackType>().HasKey(c => c.TrackTypeid);

            // LicenseRecordingMedley
            modelBuilder.Entity<LicenseRecordingMedley>().ToTable("LicenseRecordingMedley");
            modelBuilder.Entity<LicenseRecordingMedley>().HasKey(c => c.LicenseRecordingMedleyId);
            modelBuilder.Entity<LicenseRecordingMedley>().HasRequired(c => c.TrackType).WithMany().HasForeignKey(c => c.TrackTypeId);

            // RecordingMedley
            modelBuilder.Entity<RecordingMedley>().ToTable("RecordingMedley");
            modelBuilder.Entity<RecordingMedley>().HasKey(c => c.RecordingMedleyId);
            modelBuilder.Entity<RecordingMedley>().HasRequired(c => c.LicenseProductRecording).WithMany().HasForeignKey(c => c.LicenseRecordingId);
            modelBuilder.Entity<RecordingMedley>().HasRequired(c => c.LicenseRecordingMedley).WithMany().HasForeignKey(c => c.LicenseRecordingMedleyId);

            // SolrIndexQueue
            modelBuilder.Entity<SolrIndexQueueItem>().ToTable("SolrIndexQueue");
            modelBuilder.Entity<SolrIndexQueueItem>().HasKey(c => c.SolrIndexQueueId);

            // SolrIndexQueue
            modelBuilder.Entity<SolrSynchronizationJob>().ToTable("SolrSynchronizationJobs");
            modelBuilder.Entity<SolrSynchronizationJob>().HasKey(c => c.JobId);

            // Audit tables

            //Audit.License
            modelBuilder.Entity<AuditLicense>().ToTable("License","Audit");
            modelBuilder.Entity<AuditLicense>().HasKey(c => c.LicenseId);
            


            //LicenseProductConfiguration
            modelBuilder.Entity<AuditLicenseProductConfiguration>().ToTable("LicenseProductConfiguration", "Audit");
            modelBuilder.Entity<AuditLicenseProductConfiguration>().HasKey(c => c.LicenseProductConfigurationId);

            //Audit.LicenseAttachment
            modelBuilder.Entity<AuditLicenseAttachment>().ToTable("LicenseAttachment", "Audit");
            modelBuilder.Entity<AuditLicenseAttachment>().HasKey(c => c.licenseAttachmentId);

            ////Audit.Licensee
            modelBuilder.Entity<AuditLicensee>().ToTable("Licensee", "Audit");
            modelBuilder.Entity<AuditLicensee>().HasKey(c => c.LicenseeId);

            ////Audit.LicenseeLabelGroup
            //modelBuilder.Entity<AuditLicenseeLabelGroup>().ToTable("Audit.LicenseeLabelGroup", "Audit");
            //modelBuilder.Entity<AuditLicenseeLabelGroup>().HasKey(c => c.LicenseeLabelGroupId);

            //////Audit.LicenseeLabelGroup
            //modelBuilder.Entity<AuditLicenseeLabelGroupLink>().ToTable("Audit.LicenseeLabelGroupLink", "Audit");
            //modelBuilder.Entity<AuditLicenseeLabelGroupLink>().HasKey(c => c.LicenseeLabelGroupLinkId);

            //Audit.LicenseNote
            modelBuilder.Entity<AuditLicenseNote>().ToTable("Audit.LicenseNote", "Audit");
            modelBuilder.Entity<AuditLicenseNote>().HasKey(c => c.licenseNoteId);

            //Audit.LicenseProduct
            modelBuilder.Entity<AuditLicenseProduct>().ToTable("Audit.LicenseProduct", "Audit");
            modelBuilder.Entity<AuditLicenseProduct>().HasKey(c => c.LicenseProductId);


            //Audit.LicenseProductRecording
            modelBuilder.Entity<AuditLicenseProductRecording>().ToTable("Audit.LicenseRecording", "Audit");
            modelBuilder.Entity<AuditLicenseProductRecording>().HasKey(c => c.LicenseRecordingId);

            //Audit.LicenseProductRecordingWriter
            modelBuilder.Entity<AuditLicenseProductRecordingWriter>().ToTable("Audit.LicenseWriter", "Audit");
            modelBuilder.Entity<AuditLicenseProductRecordingWriter>().HasKey(c => c.LicenseWriterId);

            //Audit.LicenseProductRecordingWriterNote
            modelBuilder.Entity<AuditLicenseProductRecordingWriterNote>().ToTable("Audit.LicenseWriterNote", "Audit");
            modelBuilder.Entity<AuditLicenseProductRecordingWriterNote>().HasKey(c => c.LicenseWriterNoteId);

            //Audit.LicenseProductRecordingWriterRate
            modelBuilder.Entity<AuditLicenseProductRecordingWriterRate>().ToTable("Audit.LicenseWriterRate", "Audit");
            modelBuilder.Entity<AuditLicenseProductRecordingWriterRate>().HasKey(c => c.LicenseWriterRateId);


            //Audit.LicenseProductRecordingWriterRateStatus
            modelBuilder.Entity<AuditLicenseProductRecordingWriterRateStatus>().ToTable("Audit.LicenseWriterRateStatus", "Audit");
            modelBuilder.Entity<AuditLicenseProductRecordingWriterRateStatus>().HasKey(c => c.LicenseWriterRateStatusId);
            modelBuilder.Entity<AuditLicenseProductRecordingWriterRateStatus>().Ignore(c => c.LU_SpecialStatuses);

            ////Audit.LicenseRecordingMedley
            //modelBuilder.Entity<AuditLicenseRecordingMedley>().ToTable("Audit.LicenseRecordingMedley", "Audit");
            //modelBuilder.Entity<AuditLicenseRecordingMedley>().HasKey(c => c.LicenseRecordingMedleyId);

            //Audit.RecordingMedley
            //modelBuilder.Entity<AuditRecordingMedley>().ToTable("Audit.RecordingMedley");
            //modelBuilder.Entity<AuditRecordingMedley>().HasKey(c => c.LicenseRecordingMedleyId);

            //Repostrs
            modelBuilder.Entity<ReportQueue>().ToTable("ReportQueue");
            modelBuilder.Entity<ReportQueue>().HasKey(c => c.ReportQueueId);
            modelBuilder.Entity<ReportQueue>().HasRequired(c => c.ReportType).WithMany().HasForeignKey(c => c.ReportTypeId);

            modelBuilder.Entity<ReportType>().ToTable("ReportType");
            modelBuilder.Entity<ReportType>().HasKey(c => c.ReportTypeId);
            modelBuilder.Entity<ReportType>().Property(a => a.ReportTypeName).HasColumnName("ReportType");

        }
        
    }

}