﻿using Microsoft.AspNet.Identity.EntityFramework;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.Authorization;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.Reports;
using UMPG.USL.Models.Security;
using Action = UMPG.USL.Models.Security.Action;

namespace UMPG.USL.API.Data
{
    using UMPG.USL.Models.LicenseGenerate;

    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AuthContext() : base("AuthContext")
        {
            try
            {
                Database.SetInitializer<AuthContext>(null);
                Configuration.ProxyCreationEnabled = false;
                Configuration.AutoDetectChangesEnabled = false;
            }
            catch (Exception e)
            {
                Logger.Debug("_____CRTIICAL ERROR_____________");
                Logger.Debug("An Error occured trying to connect to the database " + e.ToString());
            }
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

        public DbSet<LU_AttachmentType> LU_AttachmentTypes { get; set; }

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

        public DbSet<Models.Authorization.TokenEntity> Tokens { get; set; }

        //Data Harmonizaiton Tables

        public DbSet<Snapshot_License> Snapshot_Licenses { get; set; }
        public DbSet<Snapshot_LicenseProduct> Snapshot_LicenseProducts { get; set; }
        public DbSet<Snapshot_Licensee> Snapshot_Licensees { get; set; }

        public DbSet<Snapshot_LicenseNote> Snapshot_LicenseNotes { get; set; }

        public DbSet<Snapshot_LabelGroup> Snapshot_LabelGroups { get; set; }

        public DbSet<Snapshot_Configuration> Snapshot_Configurations { get; set; }

        public DbSet<Snapshot_Contact> Snapshot_Contacts { get; set; }
        public DbSet<Snapshot_Label> Snapshot_Labels { get; set; }
        public DbSet<Snapshot_LicenseProductConfiguration> Snapshot_LicenseProductConfigurations { get; set; }

        public DbSet<Snapshot_ProductHeader> Snapshot_ProductHeaders { get; set; }
        public DbSet<Snapshot_RecsConfiguration> Snapshot_RecsConfigurations { get; set; }
        public DbSet<Snapshot_Role> Snapshot_Roles { get; set; }
        public DbSet<Snapshot_WorksRecording> Snapshot_WorksRecordings { get; set; }
        public DbSet<Snapshot_ArtistRecs> Snapshot_ArtistRecs { get; set; }

        public DbSet<Snapshot_Address> Snapshot_Addresses { get; set; }
        public DbSet<Snapshot_ContactEmail> Snapshot_ContactEmails { get; set; }
        public DbSet<Snapshot_Phone> Snapshot_Phones { get; set; }

        public DbSet<Snapshot_KnownAs> Snapshot_KnownAs { get; set; }

        public DbSet<Snapshot_LicenseeLabelGroup> Snapshot_LicenseeLabelGroups { get; set; }

        public DbSet<Snapshot_WorksTrack> Snapshot_Tracks { get; set; }

        public DbSet<Snapshot_LicenseProductRecording> Snapshot_LicenseRecordings { get; set; }

        public DbSet<Snapshot_WorksWriter> Snapshot_WorksWriters { get; set; }

        public DbSet<Snapshot_Affiliation> Snapshot_Affiliations { get; set; }

        public DbSet<Snapshot_OriginalPublisher> Snapshot_OriginalPublishers { get; set; }
        public DbSet<Snapshot_RecsCopyright> Snapshot_RecsCopyrights { get; set; }
        public DbSet<Snapshot_LocalClientCopyright> Snapshot_LocalClientCopyrights { get; set; }
        public DbSet<Snapshot_AquisitionLocationCode> Snapshot_AquisitionLocationCodes { get; set; }

        public DbSet<Snapshot_AdminAffiliation> Snapshot_AdminAffiliations { get; set; }
        public DbSet<Snapshot_AdminAffiliationBase> Snapshot_AdminAffiliationBases { get; set; }
        public DbSet<Snapshot_Administrator> Snapshot_Administrators { get; set; }

        public DbSet<Snapshot_OriginalPublisherAffiliation> Snapshot_OriginalPublisherAffiliations { get; set; }
        public DbSet<Snapshot_OriginalPubAffiliationBase> Snapshot_OriginalPublisherAffiliationBases { get; set; }
        public DbSet<Snapshot_AdminKnownAs> Snapshot_AdminKnownAs { get; set; }

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

            //Tokens
            modelBuilder.Entity<TokenEntity>().ToTable("Tokens");
            modelBuilder.Entity<TokenEntity>().HasKey(t => t.Id);
            modelBuilder.Entity<TokenEntity>().HasRequired(t => t.User).WithMany().HasForeignKey(t => t.UserId);

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

            //Snapshot_License
            modelBuilder.Entity<Snapshot_License>().ToTable("Snapshot_License");
            modelBuilder.Entity<Snapshot_License>().HasKey(x => x.SnapshotLicenseId);
            modelBuilder.Entity<Snapshot_License>().HasMany(r => r.LicenseProducts).WithOptional().HasForeignKey(_ => _.LicenseId);
            modelBuilder.Entity<Snapshot_License>().HasMany(r => r.LicenseNoteList).WithOptional().HasForeignKey(c => c.LicenseId);
            modelBuilder.Entity<Snapshot_License>().HasRequired(c => c.LicenseType).WithMany().HasForeignKey(c => c.LicenseTypeId);
            modelBuilder.Entity<Snapshot_License>().HasRequired(c => c.LicensePriority).WithMany().HasForeignKey(c => c.PriorityId);
            //   modelBuilder.Entity<Snapshot_License>().HasRequired(c => c.Licensee).WithMany().HasForeignKey(c => c.LicenseeId);
            modelBuilder.Entity<Snapshot_License>().HasRequired(c => c.LicenseStatus).WithMany().HasForeignKey(c => c.LicenseStatusId);
            modelBuilder.Entity<Snapshot_License>().HasRequired(c => c.LicenseMethod).WithMany().HasForeignKey(c => c.LicenseMethodId);
            modelBuilder.Entity<Snapshot_License>().HasRequired(c => c.Contact).WithMany().HasForeignKey(c => c.AssignedToId);
            modelBuilder.Entity<Snapshot_License>().HasRequired(c => c.Contact2).WithMany().HasForeignKey(c => c.CreatedBy);
            modelBuilder.Entity<Snapshot_License>().HasRequired(c => c.LicenseeContact).WithMany().HasForeignKey(c => c.ContactId);
            modelBuilder.Entity<Snapshot_License>().HasRequired(c => c.LicenseeLabelGroup).WithMany().HasForeignKey(c => c.LicenseeId);
            modelBuilder.Entity<Snapshot_License>().Ignore(c => c.ProductsNo);
            modelBuilder.Entity<Snapshot_License>().Ignore(c => c.Label);
            modelBuilder.Entity<Snapshot_License>().Ignore(c => c.ClaimException);
            modelBuilder.Entity<Snapshot_License>().Ignore(c => c.StatusesRollup);

            //Snapshot_LicenseProduct
            modelBuilder.Entity<Snapshot_LicenseProduct>().ToTable("Snapshot_LicenseProduct");
            modelBuilder.Entity<Snapshot_LicenseProduct>().HasKey(x => x.SnapshotLicenseProductId);
            modelBuilder.Entity<Snapshot_LicenseProduct>().HasRequired(c => c.Schedule).WithMany().HasForeignKey(c => c.ScheduleId);
            modelBuilder.Entity<Snapshot_LicenseProduct>().HasRequired(c => c.ProductHeader).WithMany().HasForeignKey(c => c.ProductHeaderId);
            modelBuilder.Entity<Snapshot_LicenseProduct>().Ignore(c => c.LicensePRecordingsNo);
            modelBuilder.Entity<Snapshot_LicenseProduct>().HasMany(_ => _.ProductConfigurations).WithOptional().HasForeignKey(x => x.LicenseProductId); //Not needed?
            modelBuilder.Entity<Snapshot_LicenseProduct>().HasMany(_ => _.Recordings).WithOptional().HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Snapshot_LicenseProduct>().Ignore(c => c.title);
            modelBuilder.Entity<Snapshot_LicenseProduct>().Ignore(c => c.RelatedLicensesNo);
            modelBuilder.Entity<Snapshot_LicenseProduct>().Ignore(c => c.LicenseClaimException);

            //Snapshot_LicenseNotes
            modelBuilder.Entity<Snapshot_LicenseNote>().ToTable("Snapshot_LicenseNote");
            modelBuilder.Entity<Snapshot_LicenseNote>().HasKey(x => x.SnapshotLicenseNoteId);
            modelBuilder.Entity<Snapshot_LicenseNote>().HasRequired(c => c.NoteType).WithMany().HasForeignKey(c => c.NoteTypeId);
            modelBuilder.Entity<Snapshot_LicenseNote>().HasRequired(c => c.Contact).WithMany().HasForeignKey(c => c.CreatedBy);

            //Snapshot_Role
            modelBuilder.Entity<Snapshot_Role>().ToTable("Snapshot_Role");
            modelBuilder.Entity<Snapshot_Role>().HasKey(x => x.SnapshotRoleId);

            //Snapshot_recsConfiguration
            modelBuilder.Entity<Snapshot_RecsConfiguration>().ToTable("Snapshot_RecsConfiguration");
            modelBuilder.Entity<Snapshot_RecsConfiguration>().HasKey(x => x.SnapshotRecsConfigurationId);
            modelBuilder.Entity<Snapshot_RecsConfiguration>().HasRequired(c => c.Configuration).WithMany().HasForeignKey(c => c.ConfigurationId);
            modelBuilder.Entity<Snapshot_RecsConfiguration>().HasRequired(c => c.LicenseProductConfiguration).WithMany().HasForeignKey(c => c.LicenseProductConfigurationId);

            //Snapshot_ProductHeader
            modelBuilder.Entity<Snapshot_ProductHeader>().ToTable("Snapshot_ProductHeader");
            modelBuilder.Entity<Snapshot_ProductHeader>().HasKey(x => x.SnapshotProductHeaderId);
            modelBuilder.Entity<Snapshot_ProductHeader>().HasRequired(c => c.Artist).WithMany().HasForeignKey(c => c.ArtistRecsId);
            modelBuilder.Entity<Snapshot_ProductHeader>().HasRequired(c => c.Label).WithMany().HasForeignKey(c => c.LabelId);
            modelBuilder.Entity<Snapshot_ProductHeader>().HasMany(x => x.Configurations).WithOptional().HasForeignKey(_ => _.ProductHeaderId);

            //Shanpshot_LicenseProductConfiguration
            modelBuilder.Entity<Snapshot_LicenseProductConfiguration>().ToTable("Snapshot_LicenseProductConfiguration");
            modelBuilder.Entity<Snapshot_LicenseProductConfiguration>().HasKey(_ => _.LicenseProductConfigurationId);

            //Snapshot_Label
            modelBuilder.Entity<Snapshot_Label>().ToTable("Snapshot_Label");
            modelBuilder.Entity<Snapshot_Label>().HasKey(_ => _.SnapshotLabelId);
            modelBuilder.Entity<Snapshot_Label>().HasMany(_ => _.RecordLabelGroups).WithOptional().HasForeignKey(_ => _.CloneLabelId);

            //Snapshot_LabelGroup
            modelBuilder.Entity<Snapshot_LabelGroup>().ToTable("Snapshot_LabelGroup");
            modelBuilder.Entity<Snapshot_LabelGroup>().HasKey(_ => _.SnapshotLabelGroupId);

            //Snapshot_Contact
            modelBuilder.Entity<Snapshot_Contact>().ToTable("Snapshot_Contact");
            modelBuilder.Entity<Snapshot_Contact>().HasKey(_ => _.SnapshotContactId);
            modelBuilder.Entity<Snapshot_Contact>().HasRequired(_ => _.Role).WithMany().HasForeignKey(_ => _.RoleId);
            modelBuilder.Entity<Snapshot_Contact>().HasMany(_ => _.Address).WithOptional().HasForeignKey(_ => _.ContactId);
            modelBuilder.Entity<Snapshot_Contact>().HasMany(_ => _.Phone).WithOptional().HasForeignKey(_ => _.ContactId);
            modelBuilder.Entity<Snapshot_Contact>().HasMany(_ => _.Email).WithOptional().HasForeignKey(_ => _.ContactId);

            //Snapshot_Configuration
            modelBuilder.Entity<Snapshot_Configuration>().ToTable("Snapshot_Configuration");
            modelBuilder.Entity<Snapshot_Configuration>().HasKey(_ => _.SnapshotConfigId);

            //Snapshot_ArtistRecs
            modelBuilder.Entity<Snapshot_ArtistRecs>().ToTable("Snapshot_ArtistRecs");
            modelBuilder.Entity<Snapshot_ArtistRecs>().HasKey(_ => _.SnapshotArtistRecsId);

            modelBuilder.Entity<Snapshot_WorksRecording>().ToTable("Snapshot_WorksRecording");
            modelBuilder.Entity<Snapshot_WorksRecording>().HasKey(_ => _.SnapshotWorksRecodingId);
            modelBuilder.Entity<Snapshot_WorksRecording>().HasRequired(_ => _.Track).WithMany().HasForeignKey(_ => _.CloneTrackId);
            modelBuilder.Entity<Snapshot_WorksRecording>().HasMany(_ => _.Writers).WithOptional().HasForeignKey(_ => _.CloneWorksTrackId);
            modelBuilder.Entity<Snapshot_WorksRecording>().HasRequired(_ => _.LicenseRecording).WithMany().HasForeignKey(_ => _.CloneTrackId);

            modelBuilder.Entity<Snapshot_WorksWriter>().ToTable("Snapshot_WorksWriter");
            modelBuilder.Entity<Snapshot_WorksWriter>().HasKey(_ => _.SnapshotWorksWriter);
            modelBuilder.Entity<Snapshot_WorksWriter>().HasMany(_ => _.OriginalPublishers).WithOptional().HasForeignKey(_ => _.CloneWorksWriterCaeNumber);
            modelBuilder.Entity<Snapshot_WorksWriter>().HasMany(_ => _.Affiliation).WithOptional().HasForeignKey(_ => _.WriterCaeNumber);
            modelBuilder.Entity<Snapshot_WorksWriter>().HasMany(_ => _.KnownAs).WithOptional().HasForeignKey(_ => _.CloneWriterCaeCode);
            //modelBuilder.Entity<Snapshot_WorksWriter>().Ignore(_ => _.AffiliationsString);   || temp off ***
            // modelBuilder.Entity<Snapshot_WorksWriter>().HasRequired(_ => _.LicenseProductRecordingWriter).WithMany().HasForeignKey(_ => _.CloneCaeNumber); //? is thios correct?   || temp off

            modelBuilder.Entity<Snapshot_OriginalPublisher>().ToTable("Snapshot_OriginalPublisher");
            modelBuilder.Entity<Snapshot_OriginalPublisher>().HasKey(_ => _.SnapshotOriginalPublisherId);
            //    modelBuilder.Entity<Snapshot_OriginalPublisher>().HasMany(_ => _.Affiliation).WithOptional().HasForeignKey(_ => _.CloneWriterCaeNumber);  || TEMP OFF ***
            modelBuilder.Entity<Snapshot_OriginalPublisher>().HasMany(_ => _.KnownAs).WithOptional().HasForeignKey(_ => _.CloneWriterCaeCode);
            //modelBuilder.Entity<Snapshot_OriginalPublisher>().Ignore(_ => _.AffiliationsString); || temp off***
            //   modelBuilder.Entity<Snapshot_OriginalPublisher>().HasMany(_ => _.Administrator). WithOptional().HasForeignKey(_ => _.CloneCaeNumber);  //temp off

            //   modelBuilder.Entity<Snapshot_WriterBase>().ToTable("Snapshot_WriterBase");
            // modelBuilder.Entity<Snapshot_WriterBase>().HasKey(_ => _.SnapshotWriterBaseId);
            //modelBuilder.Entity<Snapshot_WriterBase>().HasMany(_ => _.Affiliation).WithOptional().HasForeignKey(_ => _.CloneWriterCaeNumber);
            //modelBuilder.Entity<Snapshot_WriterBase>().HasMany(_ => _.KnownAs).WithOptional().HasForeignKey(_ => _.CloneWriterCaeCode);
            //modelBuilder.Entity<Snapshot_WriterBase>().Ignore(_ => _.AffiliationsString);

            modelBuilder.Entity<Snapshot_Affiliation>().ToTable("Snapshot_Affiliation");
            modelBuilder.Entity<Snapshot_Affiliation>().HasKey(_ => _.SnapshotAffiliationId);
            //  modelBuilder.Entity<Snapshot_Affiliation>().HasMany(_ => _.Affiliations).WithOptional().HasForeignKey(_ => _.CloneWriterCaeNumber);

            modelBuilder.Entity<Snapshot_WorksTrack>().ToTable("Snapshot_WorksTrack");
            modelBuilder.Entity<Snapshot_WorksTrack>().HasKey(_ => _.SnapshotWorkTrackId);
            modelBuilder.Entity<Snapshot_WorksTrack>().HasRequired(_ => _.Artist).WithMany().HasForeignKey(_ => _.ArtistRecsId);
            modelBuilder.Entity<Snapshot_WorksTrack>().HasMany(_ => _.Copyrights).WithOptional().HasForeignKey(_ => _.CloneWorksTrackId);

            modelBuilder.Entity<Snapshot_RecsCopyright>().ToTable("Snapshot_RecsCopyright");
            modelBuilder.Entity<Snapshot_RecsCopyright>().HasKey(_ => _.SnapshotRecsCopyrightsId);
            modelBuilder.Entity<Snapshot_RecsCopyright>().HasMany(_ => _.Composers).WithOptional().HasForeignKey(_ => _.CloneWorksTrackId);
            modelBuilder.Entity<Snapshot_RecsCopyright>().HasMany(_ => _.Samples).WithOptional().HasForeignKey(_ => _.CloneWorksTrackId);
            modelBuilder.Entity<Snapshot_RecsCopyright>().HasMany(_ => _.LocalClients).WithOptional().HasForeignKey(_ => _.CloneWorksTrackId);
            modelBuilder.Entity<Snapshot_RecsCopyright>().HasMany(_ => _.AquisitionLocationCodes).WithOptional().HasForeignKey(_ => _.CloneWorksTrackId);

            modelBuilder.Entity<Snapshot_LocalClientCopyright>().ToTable("Snapshot_LocalClientCopyright");
            modelBuilder.Entity<Snapshot_LocalClientCopyright>().HasKey(_ => _.SnapshotLocalClientCopyrightId);

            modelBuilder.Entity<Snapshot_AquisitionLocationCode>().ToTable("Snapshot_AquisitionLocationCode");
            modelBuilder.Entity<Snapshot_AquisitionLocationCode>().HasKey(_ => _.SnapshotAquisitionLocationCode);

            modelBuilder.Entity<Snapshot_LicenseProductRecording>().ToTable("Snapshot_LicenseProductRecording");
            modelBuilder.Entity<Snapshot_LicenseProductRecording>().HasKey(_ => _.SnapshotLicenseProductRecordingId);
            modelBuilder.Entity<Snapshot_LicenseProductRecording>().Ignore(_ => _.StatusRollup);
            // modelBuilder.Entity<Snapshot_LicenseProductRecording>().HasMany(_ => _.LicensePRWriters).WithOptional().HasForeignKey(_ => _.LicenseRecordingId); //Returns null in GetProductsNew

            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriter>().ToTable("Snapshot_LicenseProductRecordingWriter");
            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriter>().HasKey(_ => _.SnapshotLicenseProductRecordingWriterId);
            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriter>().HasMany(_ => _.WriterNotes).WithOptional().HasForeignKey(_ => _.WriterCaeCode);
            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriter>().HasMany(_ => _.RateList).WithOptional().HasForeignKey(_ => _.CaeCode);

            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriterRate>().ToTable("Snapshot_LicenseProductRecordingWriterRate");
            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriterRate>().HasKey(_ => _.SnapshotLicenseProductRecordingWriterRateId);
            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriterRate>().HasRequired(_ => _.RateType).WithMany().HasForeignKey(_ => _.RateTypeId);
            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriterRate>().HasRequired(_ => _.WritersConsentType).WithMany().HasForeignKey(_ => _.WritersConsentTypeId);
            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriterRate>().HasMany(_ => _.SpecialStatusList).WithOptional().HasForeignKey(_ => _.LicenseWriterRateId);

            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriterRateStatus>().ToTable("Snapshot_LicenseProductRecordingWriterRateStatus");
            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriterRateStatus>().HasKey(_ => _.SnapshotLicenseProductRecordingWriterRateStatusId);
            modelBuilder.Entity<Snapshot_LicenseProductRecordingWriterRateStatus>().HasRequired(_ => _.LU_SpecialStatuses).WithMany().HasForeignKey(_ => _.SpecialStatusId);

            modelBuilder.Entity<Snapshot_LicenseeLabelGroup>().ToTable("Snapshot_LicenseeLabelGroup");
            modelBuilder.Entity<Snapshot_LicenseeLabelGroup>().HasKey(_ => _.SnapshotLicenseeLabelGroupId);
            modelBuilder.Entity<Snapshot_LicenseeLabelGroup>().HasMany(_ => _.LabelGroupLinks).WithOptional().HasForeignKey(_ => _.CloneLicenseeLabelGroupLinkId);
            modelBuilder.Entity<Snapshot_LicenseeLabelGroup>().HasMany(_ => _.LabelGroupLinksFiltered).WithOptional().HasForeignKey(_ => _.CloneLicenseeLabelGroupLinkId);

            modelBuilder.Entity<Snapshot_LicenseeLabelGroupLink>().ToTable("Snapshot_LicenseeLabelGroupLink");
            modelBuilder.Entity<Snapshot_LicenseeLabelGroupLink>().HasKey(_ => _.SnapshotCloneLicenseeLabelGroupLinkId);
            modelBuilder.Entity<Snapshot_LicenseeLabelGroupLink>().HasRequired(_ => _.Contact).WithMany().HasForeignKey(_ => _.ContactId);

            modelBuilder.Entity<Snapshot_Address>().ToTable("Snapshot_Address");
            modelBuilder.Entity<Snapshot_Address>().HasKey(_ => _.SnapshotAddressId);

            modelBuilder.Entity<Snapshot_ContactEmail>().ToTable("Snapshot_ContactEmail");
            modelBuilder.Entity<Snapshot_ContactEmail>().HasKey(_ => _.SnapshotContactEmailId);

            modelBuilder.Entity<Snapshot_Phone>().ToTable("Snapshot_Phone");
            modelBuilder.Entity<Snapshot_Phone>().HasKey(_ => _.SnapshotPhoneId);

            modelBuilder.Entity<Snapshot_Licensee>().ToTable("Snapshot_Licensee");
            modelBuilder.Entity<Snapshot_Licensee>().HasKey(_ => _.SnapshotLicenseeId);

            
            modelBuilder.Entity<Snapshot_Administrator>().ToTable("Snapshot_Administrator");
            modelBuilder.Entity<Snapshot_Administrator>().HasKey(_ => _.SnapshotAdministratorId);
            modelBuilder.Entity<Snapshot_Administrator>().Ignore(_ => _.AffiliationsString);
            modelBuilder.Entity<Snapshot_Administrator>().HasMany(_ => _.Affiliation).WithOptional().HasForeignKey(_ => _.SnapshotAdministratorId);
            modelBuilder.Entity<Snapshot_Administrator>().HasMany(_ => _.KnownAs).WithOptional().HasForeignKey(_ => _.SnapshotAdministratorId);

            modelBuilder.Entity<Snapshot_AdminAffiliationBase>().ToTable("Snapshot_AdminAffiliationBase");
            modelBuilder.Entity<Snapshot_AdminAffiliationBase>().HasKey(_ => _.SnapshotAdminAffiliationBaseId);

            modelBuilder.Entity<Snapshot_AdminAffiliation>().ToTable("Snapshot_AdminAffiliation");
            modelBuilder.Entity<Snapshot_AdminAffiliation>().HasKey(_ => _.SnapshotAdminAffiliationId);
            modelBuilder.Entity<Snapshot_AdminAffiliation>().HasMany(_ => _.Affiliations).WithOptional().HasForeignKey(_ => _.SnapshotAdminAffiliationId);

            modelBuilder.Entity<Snapshot_AdminKnownAs>().ToTable("Snapshot_AdminKnownAs");
            modelBuilder.Entity<Snapshot_AdminKnownAs>().HasKey(_ => _.SnapshotAdminKnownAsId);


            //_____End Data Harmonization

            // Licenses
            modelBuilder.Entity<License>().ToTable("License", "dbo");
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
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Ignore(c => c.upc);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Ignore(c => c.LicenseTitle);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Ignore(c => c.LicenseId);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Ignore(c => c.LicenseNumber);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Ignore(c => c.CaeCode);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Ignore(c => c.licenseRecordingId);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Ignore(c => c.trackId);
            modelBuilder.Entity<LicenseProductRecordingWriterRate>().Property(c => c.Rate).HasPrecision(7, 6);
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

            //LU_AttachmentType
            modelBuilder.Entity<LU_AttachmentType>().ToTable("LU_AttachmentType");
            modelBuilder.Entity<LU_AttachmentType>().HasKey(c => c.AttachmentTypeId);

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
            modelBuilder.Entity<LicenseAttachment>().HasRequired(c => c.AttachmentType).WithMany().HasForeignKey(c => c.AttachmentTypeId);

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
            modelBuilder.Entity<AuditLicense>().ToTable("License", "Audit");
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