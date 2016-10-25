using BusinessServices;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UMPG.USL.API.Business.Contacts;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.API.Business.LookUps;
using UMPG.USL.API.Business.Lookups;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Business.Audits;
using UMPG.USL.API.Business.DataHarmonization;
using UMPG.USL.API.Business.Reports;
using UMPG.USL.API.Business.Token;


namespace UMPG.USL.API.Business.Installer
{
    public class CoreComponentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IInterceptor>().ImplementedBy<LicenseSequenceIntercepror>().LifestyleSingleton());
            container.Register(Component.For<IInterceptor>().ImplementedBy<LicenseUpdateIntercepror>().LifestyleSingleton());
            
            container.Register(Component.For<IAuthManager>().ImplementedBy<AuthManager>());
            container.Register(Component.For<ILicenseManager>().ImplementedBy<LicenseManager>().Interceptors<LicenseSequenceIntercepror,LicenseUpdateIntercepror>());
            container.Register(Component.For<ILicenseNoteManager>().ImplementedBy<LicenseNoteManager>());
            container.Register(Component.For<ILicenseProductWriterNoteManager>().ImplementedBy<LicenseProductWriterNoteManager>());
            container.Register(Component.For<ILicenseeManager>().ImplementedBy<LicenseeManager>());
            container.Register(Component.For<ILicenseAttachmentManager>().ImplementedBy<LicenseAttachmentManager>());
            container.Register(Component.For<IContactManager>().ImplementedBy<ContactManager>());
            container.Register(Component.For<IContactDefaultManager>().ImplementedBy<ContactDefaultManager>());
            container.Register(Component.For<IContactGenerateLicenseQueueManager>().ImplementedBy<ContactGenerateLicenseQueueManager>());
            container.Register(Component.For<IGenerateLicenseManager>().ImplementedBy<GenerateLicenseManager>());
            container.Register(Component.For<ILicenseMethodManager>().ImplementedBy<LicenseMethodManager>());
            container.Register(Component.For<IPriorityManager>().ImplementedBy<PriorityManager>());
            container.Register(Component.For<IRateTypeManager>().ImplementedBy<RateTypeManager>());
            container.Register(Component.For<IScheduleManager>().ImplementedBy<ScheduleManager>());
            container.Register(Component.For<IWritersConsentTypeManager>().ImplementedBy<WritersConsentTypeManager>());
            container.Register(Component.For<IPaidQuarterManager>().ImplementedBy<PaidQuarterManager>());
            container.Register(Component.For<ISpecialStatusManager>().ImplementedBy<SpecialStatusManager>());
            container.Register(Component.For<ILicenseStatusManager>().ImplementedBy<LicenseStatusManager>());
            container.Register(Component.For<ILicenseProductManager>().ImplementedBy<LicenseProductManager>().Interceptors<LicenseUpdateIntercepror>());
            container.Register(Component.For<ILicensePRWriterRateManager>().ImplementedBy<LicensePRWriterRateManager>());
            container.Register(Component.For<ILicenseTypeManager>().ImplementedBy<LicenseTypeManager>());
            container.Register(Component.For<ILicenseProductConfigurationManager>().ImplementedBy<LicenseProductConfigurationManager>().Interceptors<LicenseUpdateIntercepror>());
            container.Register(Component.For<IConfigurationManager>().ImplementedBy<ConfigurationManager>());
            container.Register(Component.For<IProductManager>().ImplementedBy<ProductManager>().Interceptors<LicenseUpdateIntercepror>());
            container.Register(Component.For<IAuditManager>().ImplementedBy<AuditManager>());
            container.Register(Component.For<IAutosuggestManager>().ImplementedBy<AutosuggestManager>());
            container.Register(Component.For<ILabelManager>().ImplementedBy<LabelManager>());
            container.Register(Component.For<ILicenseSolrManager>().ImplementedBy<LicenseProductSolrManager>().LifestyleTransient());
            container.Register(Component.For<ITrackTypeManager>().ImplementedBy<TrackTypeManager>());
            container.Register(Component.For<ILicenseRecordingMedleyManager>().ImplementedBy<LicenseRecordingMedleyManager>());
            container.Register(Component.For<IReportQueueManager>().ImplementedBy<ReportQueueManager>());
            container.Register(Component.For<ITokenServices>().ImplementedBy<TokenServices>());
            container.Register(Component.For<IAttachmentTypeManager>().ImplementedBy<AttachmentTypeManager>());



            //Data Harmonization Managers
            container.Register(Component.For<ISnapshotArtistRecsManager>().ImplementedBy<SnapshotArtistRecsManager>());
            container.Register(Component.For<ISnapshotConfigurationManager>().ImplementedBy<SnapshotConfigurationManager>());
            container.Register(Component.For<ISnapshotContactManger>().ImplementedBy<SnapshotContactManger>());
            container.Register(Component.For<ISnapshotLabelGroupManager>().ImplementedBy<SnapshotLabelGroupManager>());
            container.Register(Component.For<ISnapshotLicenseProductManager>().ImplementedBy<SnapshotLicenseProductManager>());
            container.Register(Component.For<ISnapshotLicenseProductConfigurationManager>().ImplementedBy<SnapshotLicenseProductConfigurationManager>());
            container.Register(Component.For<ISnapshotProductHeaderManager>().ImplementedBy<SnapshotProductHeaderManager>());
            container.Register(Component.For<ISnapshotRecsConfigurationManager>().ImplementedBy<SnapshotRecsConfigurationManager>());
            container.Register(Component.For<ISnapshotRoleManager>().ImplementedBy<SnapshotRoleManager>());
            container.Register(Component.For<ISnapshotWorksRecordingManager>().ImplementedBy<SnapshotWorksRecordingManager>());
            container.Register(Component.For<ISnapshotLicenseManager>().ImplementedBy<SnapshotLicenseManager>());
            container.Register(Component.For<ISnapshotLicenseNoteManager>().ImplementedBy<SnapshotLicenseNoteManager>());
            container.Register(Component.For<IDataHarmonizationManager>().ImplementedBy<DataHarmonizationManager>());

            //container.Register(
            //    Component.For<IDataHarmonizationManager>()
            //        .ImplementedBy<DataHarmonizationManager>()
            //        .DependsOn(Dependency.OnComponent(
            //            typeof(ISnapshotLicenseManager),
            //            "SnapshotLicenseManager")));

        }
    }
}

