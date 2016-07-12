using System.Data.Entity.ModelConfiguration.Conventions;
using UMPG.USL.Models;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.Models.LicenseGenerate;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.AuditModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using UMPG.USL.Models.Reports;
using UMPG.USL.Models.Security;

namespace UMPG.USL.API.Data
{
    public class AuditContext : IdentityDbContext<IdentityUser>
    {

        public AuditContext()
            : base("AuditContext")
        {
            Database.SetInitializer<AuthContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;

        }
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("dbo");
            // Audit tables

            //Audit.License
            modelBuilder.Entity<AuditLicense>().ToTable("Audit_License");
            modelBuilder.Entity<AuditLicense>().HasKey(c => c.Id);



            //LicenseProductConfiguration
            modelBuilder.Entity<AuditLicenseProductConfiguration>().ToTable("Audit_LicenseProductConfiguration");
            modelBuilder.Entity<AuditLicenseProductConfiguration>().HasKey(c => c.Id);

            //Audit.LicenseAttachment
            modelBuilder.Entity<AuditLicenseAttachment>().ToTable("Audit_LicenseAttachment");
            modelBuilder.Entity<AuditLicenseAttachment>().HasKey(c => c.Id);

            ////Audit.Licensee
            modelBuilder.Entity<AuditLicensee>().ToTable("Audit_Licensee");
            modelBuilder.Entity<AuditLicensee>().HasKey(c => c.Id);

            ////Audit.LicenseeLabelGroup
            //modelBuilder.Entity<AuditLicenseeLabelGroup>().ToTable("Audit.LicenseeLabelGroup", "Audit");
            //modelBuilder.Entity<AuditLicenseeLabelGroup>().HasKey(c => c.LicenseeLabelGroupId);

            //////Audit.LicenseeLabelGroup
            //modelBuilder.Entity<AuditLicenseeLabelGroupLink>().ToTable("Audit.LicenseeLabelGroupLink", "Audit");
            //modelBuilder.Entity<AuditLicenseeLabelGroupLink>().HasKey(c => c.LicenseeLabelGroupLinkId);

            //Audit.LicenseNote
            modelBuilder.Entity<AuditLicenseNote>().ToTable("Audit_LicenseNote");
            modelBuilder.Entity<AuditLicenseNote>().HasKey(c => c.Id);

            //Audit.LicenseProduct
            modelBuilder.Entity<AuditLicenseProduct>().ToTable("Audit_LicenseProduct");
            modelBuilder.Entity<AuditLicenseProduct>().HasKey(c => c.Id);


            //Audit.LicenseProductRecording
            modelBuilder.Entity<AuditLicenseProductRecording>().ToTable("Audit_LicenseProductRecording");
            modelBuilder.Entity<AuditLicenseProductRecording>().HasKey(c => c.Id);

            //Audit.LicenseProductRecordingWriter
            modelBuilder.Entity<AuditLicenseProductRecordingWriter>().ToTable("Audit_LicenseProductRecordingWriter");
            modelBuilder.Entity<AuditLicenseProductRecordingWriter>().HasKey(c => c.Id);

            //Audit.LicenseProductRecordingWriterNote
            modelBuilder.Entity<AuditLicenseProductRecordingWriterNote>().ToTable("Audit_LicenseProductRecordingWriterNote");
            modelBuilder.Entity<AuditLicenseProductRecordingWriterNote>().HasKey(c => c.Id);

            //Audit.LicenseProductRecordingWriterRate
            modelBuilder.Entity<AuditLicenseProductRecordingWriterRate>().ToTable("Audit_LicenseProductRecordingWriterRate");
            modelBuilder.Entity<AuditLicenseProductRecordingWriterRate>().HasKey(c => c.Id);


            //Audit.LicenseProductRecordingWriterRateStatus
            modelBuilder.Entity<AuditLicenseProductRecordingWriterRateStatus>().ToTable("Audit_LicenseProductRecordingWriterRateStatus");
            modelBuilder.Entity<AuditLicenseProductRecordingWriterRateStatus>().HasKey(c => c.Id);
            //modelBuilder.Entity<AuditLicenseProductRecordingWriterRateStatus>().Ignore(c => c.LU_SpecialStatuses);

            //Audit.RecordingMedley
            modelBuilder.Entity<AuditRecordingMedley>().ToTable("Audit_LicenseRecordingMedley");
            modelBuilder.Entity<AuditRecordingMedley>().HasKey(c => c.Id);


        }
    }
}
